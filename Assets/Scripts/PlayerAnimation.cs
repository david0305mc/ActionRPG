using David;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float locomotionBlendSpeed = 0.02f;

    private PlayerLocoMotionInput playerLocomotionInput;
    private PlayerState playerState;

    private static int inputXHash = Animator.StringToHash("InputX");
    private static int inputYHash = Animator.StringToHash("InputY");
    private static int inputMagnitude = Animator.StringToHash("InputMagnitude");

    private Vector3 currentBlendInput = Vector3.zero;
    private void Awake()
    {
        playerLocomotionInput = GetComponent<PlayerLocoMotionInput>();
        playerState = GetComponent<PlayerState>();
    }

    private void Update()
    {
        UpdateAnimationState();    
    }

    private void UpdateAnimationState()
    {
        bool isSprinting = playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;

        Vector2 inputTarget = isSprinting ? playerLocomotionInput.MovementInput * 1.5f : playerLocomotionInput.MovementInput;
        currentBlendInput = Vector3.Lerp(currentBlendInput, inputTarget, locomotionBlendSpeed * Time.deltaTime);

        animator.SetFloat(inputXHash, currentBlendInput.x);
        animator.SetFloat(inputYHash, currentBlendInput.y);
        animator.SetFloat(inputMagnitude, inputTarget.magnitude);
    }
}
