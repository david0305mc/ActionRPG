using David;
using System.Collections;
using System.Collections.Generic;
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
    public float drag = 0.1f;


    private PlayerLocoMotionInput locomotionInput;

    private void Awake()
    {
        locomotionInput = GetComponent<PlayerLocoMotionInput>();
    }

    private void Update()
    {
        Vector3 cameraFowardXZ = new Vector3 (playerCamera.transform.forward.x, 0.0f, playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(playerCamera.transform.right.x, 0f, playerCamera.transform.right.z).normalized;
        Vector3 movementDir = cameraRightXZ * locomotionInput.MovementInput.x + cameraFowardXZ * locomotionInput.MovementInput.y;

        Vector3 movementDelta = movementDir * runAcceleration * Time.deltaTime;
        Vector3 newVelocity = characterController.velocity + movementDelta;

        Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;
        newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
        newVelocity = Vector3.ClampMagnitude(newVelocity, runSpeed);
        characterController.Move(newVelocity * Time.deltaTime);
    }

}