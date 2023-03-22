using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class S_PlayerInput : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField]
    private S_PlayerModelRef playerRefrence;
    public Vector2 _mvn;
    public Vector2 _rotmvn;
    public Vector2 _rotair;

    S_HoverboardPhysic _physicsSystem;

    public Animator anim;
    public GameObject playerRef;
    int isLeaningForwardHash;
    int isLeaningRightHash;
    int isLeaningLeftHash;
    int isJumpingHash;

    private void Awake()
    {

        _physicsSystem = GetComponent<S_HoverboardPhysic>();
        playerInput = GetComponent<PlayerInput>();
        playerRefrence = GetComponentInChildren<S_PlayerModelRef>();

        playerInput.actions["Move"].performed += ctx => _mvn = ctx.ReadValue<Vector2>();
        playerInput.actions["Move"].canceled += ctx => _mvn = Vector2.zero;

        playerInput.actions["Rotate"].performed += ctx => _rotmvn = ctx.ReadValue<Vector2>();
        playerInput.actions["Rotate"].canceled += ctx => _rotmvn = Vector2.zero;

        playerInput.actions["AirRotation"].performed += ctx => _rotair = ctx.ReadValue<Vector2>();
        playerInput.actions["AirRotation"].canceled += ctx => _rotair = Vector2.zero;
    }

    private void Start()
    {
        FindObjectOfType<S_AudioManager>().Play("SnowboardA");

        anim = playerRefrence.GetComponent<Animator>();
        isLeaningForwardHash = Animator.StringToHash("IsMovingForward");
        isLeaningRightHash = Animator.StringToHash("IsMovingRight");
        isLeaningLeftHash = Animator.StringToHash("IsMovingLeft");
        isJumpingHash = Animator.StringToHash("IsJumping");
    }

    private void Update()
    {
        handleMovement();
        if (!_physicsSystem.isGrounded)
        {
            FindObjectOfType<S_AudioManager>().Pause("SnowboardA");
            
        }
    }

    void handleMovement()
    {
        if (anim != null)
        {
            bool isLeaningForward = anim.GetBool(isLeaningForwardHash);
            bool isLeaningRight = anim.GetBool(isLeaningRightHash);
            bool isLeaningLeft = anim.GetBool(isLeaningLeftHash);

            if (_mvn.magnitude > 0)
            {
                anim.SetBool("IsMovingForward", true);
            }
            else
            {
                anim.SetBool("IsMovingForward", false);
            }
            if (_rotmvn.x > 0.1f)
            {
                anim.SetBool("IsMovingRight", true);
            }
            else
            {
                anim.SetBool("IsMovingRight", false);
            }
            if (_rotmvn.x < -0.1f)
            {
                anim.SetBool("IsMovingLeft", true);
            }
            else
            {
                anim.SetBool("IsMovingLeft", false);
            }
            
        }
        
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("HasLanded", true);
            FindObjectOfType<S_AudioManager>().UnPause("SnowboardA");
           // FindObjectOfType<S_AudioManager>().Play("Snow-Landing");

        }
    }

    public void OnJump(InputValue value)
    {
        if (_physicsSystem.isGrounded)
        {
            anim.SetBool("HasLanded", false);
            anim.SetBool("IsJumping", true);
            _physicsSystem.Jump();
        }
    }














}
