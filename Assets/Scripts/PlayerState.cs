using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace David
{
    public enum PlayerMovementState
    {
        Idleing = 0,
        Walking = 1,
        Running = 2,
        Sprinting = 3,
        Jumping = 4,
        Falling = 5,
        Strafing = 6,
    }

    public class PlayerState : MonoBehaviour
    {

        [field: SerializeField] public PlayerMovementState CurrentPlayerMovementState { get; private set; } = PlayerMovementState.Idleing;

        public void SetPlayerMovementState(PlayerMovementState playerMovementState)
        {
            CurrentPlayerMovementState = playerMovementState;
              
        }

        public bool IsGroundedState()
        {
            return CurrentPlayerMovementState == PlayerMovementState.Idleing ||
                CurrentPlayerMovementState == PlayerMovementState.Walking ||
                CurrentPlayerMovementState == PlayerMovementState.Running ||
                CurrentPlayerMovementState == PlayerMovementState.Strafing;
        }
    }

}
