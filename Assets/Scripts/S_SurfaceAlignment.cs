using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class S_SurfaceAlignment : MonoBehaviour
{
    private Transform orientation;
    [SerializeField]
    private Transform playerModel;
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private AnimationCurve anim;
    [SerializeField]
    private float _time;
    [SerializeField]
    private float _modelTime;
    private void Start()
    {
        orientation = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        SurfaceAlignment();
    }

    private void SurfaceAlignment()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit info = new RaycastHit();
        Quaternion RotationRef = Quaternion.identity;
        Quaternion TerrainNormal = Quaternion.identity;

        if (Physics.Raycast(ray, out info, 1.5f, ground))
        {
            RotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, info.normal), anim.Evaluate(_time));
            TerrainNormal = Quaternion.FromToRotation(playerModel.up, info.normal);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-RotationRef.eulerAngles.x, transform.rotation.eulerAngles.y, RotationRef.eulerAngles.z), _time);
           
            //Changes the player rotation to be aligned to the slope they are standing on. 
            playerModel.rotation = Quaternion.Lerp(playerModel.rotation, Quaternion.Euler(TerrainNormal.eulerAngles.x, playerModel.rotation.y, TerrainNormal.eulerAngles.z) * playerModel.rotation, _time);

            //Changes the player rotation to be always facing forward when on the ground.
            playerModel.rotation = Quaternion.Lerp(playerModel.rotation, Quaternion.Euler(playerModel.eulerAngles.x, orientation.eulerAngles.y, playerModel.eulerAngles.z), _time);
        }
    }
}