using David;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera playerCamera;

    [Header("BaseMovement")]
    public float runAcceleration = 0.25f;
    public float runSpeed = 4f;
    public float sprintAcceleration = 0.5f;
    public float sprintSpeed = 10f;
    public float drag = 0.1f;
    public float movingThreshold = 0.01f;
    public float gravity = 25f;
    public float jumpSpeed = 1.0f;


    private PlayerState playerState;
    private PlayerLocoMotionInput locomotionInput;

    private void Awake()
    {
        locomotionInput = GetComponent<PlayerLocoMotionInput>();
        playerState = GetComponent<PlayerState>();
    }

    private void Update()
    {
        UpdateMovementState();
        HandleLateralMovement();
    }

    private void UpdateMovementState()
    {
        bool isMovementInput = locomotionInput.MovementInput != Vector2.zero;
        bool isMovingLaterally = IsMovingLaterally();
        bool isSprinting = locomotionInput.SprintToggledOn && isMovingLaterally;
        bool isGrounded = IsGrounded();
            
        if (isSprinting)
        {
            playerState.SetPlayerMovementState(PlayerMovementState.Sprinting);
        }
        else
        {
            PlayerMovementState lateralState = isMovingLaterally || isMovementInput ? PlayerMovementState.Running : PlayerMovementState.Idleing;
            playerState.SetPlayerMovementState(lateralState);
        }

        if (!isGrounded && characterController.velocity.y > 0f)
        {
            playerState.SetPlayerMovementState(PlayerMovementState.Jumping);
        }
        else if (!isGrounded && characterController.velocity.y < 0f)
        {
            playerState.SetPlayerMovementState(PlayerMovementState.Falling);
        }
    }

    private void HandleVerticalMovement()
    { 
        
    }

    private void HandleLateralMovement()
    {

        bool isSprinting = playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;

        float lateralAccelaration = isSprinting ? sprintAcceleration : runAcceleration;
        float clampLateralMagnitude = isSprinting ? sprintSpeed : runSpeed;

        Vector3 cameraFowardXZ = new Vector3(playerCamera.transform.forward.x, 0.0f, playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(playerCamera.transform.right.x, 0f, playerCamera.transform.right.z).normalized;
        Vector3 movementDir = cameraRightXZ * locomotionInput.MovementInput.x + cameraFowardXZ * locomotionInput.MovementInput.y;

        Vector3 movementDelta = movementDir * lateralAccelaration * Time.deltaTime;
        Vector3 newVelocity = characterController.velocity + movementDelta;

        Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;
        newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
        newVelocity = Vector3.ClampMagnitude(newVelocity, clampLateralMagnitude);
        characterController.Move(newVelocity * Time.deltaTime);
    }

    private bool IsMovingLaterally()
    {
        Vector3 lateralVelocity = new Vector3(characterController.velocity.x, 0, characterController.velocity.y);
        return lateralVelocity.magnitude > movingThreshold;
    }

    private bool IsGrounded()
    {

        return characterController.isGrounded;
        
    }
}
