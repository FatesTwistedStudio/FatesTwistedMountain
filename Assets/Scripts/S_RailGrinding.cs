using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;
// Credits fo to Adam Lutton https://youtu.be/Kxempc3fKz4
// Script is modified to work with Mountain but it's heavily based on Adam's code

public class S_RailGrinding : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] bool jump;
    [SerializeField] Vector3 input;     //But they're here for rail switching

    [Header("Variables")]
    public bool onRail;
    [SerializeField] float grindSpeed;
    [SerializeField] float heightOffset;
    float timeForFullSpline;
    float elapsedTime;
    [SerializeField] float lerpSpeed = 10f;
    [SerializeField] S_HoverboardPhysic physics;

    [Header("Scripts")]
    [SerializeField] S_RailController currentRailScript;
    CharacterController charController;
    private S_PlayerInput _PlayerInputScript;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
    }
    public void HandleJump(InputAction.CallbackContext context)
    {
        jump = Convert.ToBoolean(context.ReadValue<float>());
        Debug.LogWarning("jUMPINGS");
    }
    public void HandleMovement(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        input.x = rawInput.x;
    }
    private void FixedUpdate()
    {
        if (onRail)
        {
            //physics.canMove = false;
            MovePlayerAlongRail();
        }
    }
    void MovePlayerAlongRail()
    {
        if (currentRailScript != null && onRail)
        {
            //Calculate a 0 to 1 normalised time value which is the progress along the rail.
            float progress = elapsedTime / timeForFullSpline;

            //If progress is less than 0, the player's position is before the start of the rail.
            //If greater than 1, their position is after the end of the rail.
            //In either case, the player has finished their grind.
            if (progress < 0 || progress > 1)
            {
                ThrowOffRail();
                return;
            }
            float nextTimeNormalised;
            if (currentRailScript.normalDir)
                nextTimeNormalised = (elapsedTime + Time.deltaTime) / timeForFullSpline;
            else
                nextTimeNormalised = (elapsedTime - Time.deltaTime) / timeForFullSpline;

            //Calculating the local positions of the player's current position and next position
            float3 pos, tangent, up;
            float3 nextPosfloat, nextTan, nextUp;
            SplineUtility.Evaluate(currentRailScript.railSpline.Spline, progress, out pos, out tangent, out up);
            SplineUtility.Evaluate(currentRailScript.railSpline.Spline, nextTimeNormalised, out nextPosfloat, out nextTan, out nextUp);

            //Converting the local positions into world positions.
            Vector3 worldPos = currentRailScript.LocalToWorldConversion(pos);
            Vector3 nextPos = currentRailScript.LocalToWorldConversion(nextPosfloat);

            //Setting the player's position and adding a height offset so that they're sitting on top of the rail
            //instead of being in the middle of it.
            transform.position = worldPos + (transform.up * heightOffset);
            //Lerping the player's current rotation to the direction of where they are to where they're going.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(nextPos - worldPos), lerpSpeed * Time.deltaTime);
            //Lerping the player's up direction to match that of the rail, in relation to the player's current rotation.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, up) * transform.rotation, lerpSpeed * Time.deltaTime);

            //Finally incrementing or decrementing elapsed time for the next update based on direction.
            if (currentRailScript.normalDir)
                elapsedTime += Time.deltaTime;
            else
                elapsedTime -= Time.deltaTime;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Rail")
        {
            /*
                When the player hits the rail, onRail is set to true, the current rail script is set to the rail script of the rail the player hits. Then we calculate the player's position on that rail.
            */
            onRail = true;
            currentRailScript = hit.gameObject.GetComponent<S_RailController>();
            CalculateAndSetRailPosition();
        }
    }
    void CalculateAndSetRailPosition()
    {
        //Figure out the amount of time it would take for the player to cover the rail.
        timeForFullSpline = currentRailScript.totalSplineLength / grindSpeed;

        //This is going to be the world position of where the player is going to start on the rail.
        Vector3 splinePoint;

        //The 0 to 1 value of the player's position on the spline. We also get the world position of where that
        //point is.
        float normalisedTime = currentRailScript.CalculateTargetRailPoint(transform.position, out splinePoint);
        elapsedTime = timeForFullSpline * normalisedTime;
        //Multiply the full time for the spline by the normalised time to get elapsed time. This will be used in
        //the movement code.

        //Spline evaluate takes the 0 to 1 normalised time above,
        //and uses it to give you a local position, a tangent (forward), and up
        float3 pos, forward, up;
        SplineUtility.Evaluate(currentRailScript.railSpline.Spline, normalisedTime, out pos, out forward, out up);
        //Calculate the direction the player is going down the rail
        currentRailScript.CalculateDirection(transform.forward, transform.forward);
        //Set player's initial position on the rail before starting the movement code.
        transform.position = splinePoint + (transform.up * heightOffset);
    }
    public void ThrowOffRail()
    {
        //Set onRail to false, clear the rail script, and push the player off the rail.
        //It's a little sudden, there might be a better way of doing using coroutines and looping, but this will work.
        onRail = false;
        currentRailScript = null;
        transform.position += transform.forward * 2;
        transform.position += transform.up * 5;
        Debug.Log("ThrowOffRail");
    }
}