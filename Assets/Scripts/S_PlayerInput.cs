using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class S_PlayerInput : MonoBehaviour
{
    private PlayerInput playerInput;
    public Vector2 _mvn;
    public Vector2 _rotmvn;
    public Vector2 _rotair;

    S_HoverboardPhysic _physicsSystem;

    private void Awake()
    {
        _physicsSystem = GetComponent<S_HoverboardPhysic>();

        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Move"].performed += ctx => _mvn = ctx.ReadValue<Vector2>();
        playerInput.actions["Move"].canceled += ctx => _mvn = Vector2.zero;

        playerInput.actions["Rotate"].performed += ctx => _rotmvn = ctx.ReadValue<Vector2>();
        playerInput.actions["Rotate"].canceled += ctx => _rotmvn = Vector2.zero;

        playerInput.actions["AirRotation"].performed += ctx => _rotair = ctx.ReadValue<Vector2>();
        playerInput.actions["AirRotation"].canceled += ctx => _rotair = Vector2.zero;
    }

    public void OnJump(InputValue value)
    {
        if (_physicsSystem.isGrounded)
        {
            _physicsSystem.Jump();
        }
    }















}
