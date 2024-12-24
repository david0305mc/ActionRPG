using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace David
{
    [DefaultExecutionOrder(-2)]
    public class PlayerLocoMotionInput : MonoBehaviour, PlayerInput.IPlayerMosionActions
    {
        public PlayerInput PlayerInput { get; private set; }
        
        public Vector2 MovementInput { get; private set; }

        private void OnEnable()
        {
            PlayerInput = new PlayerInput();
            PlayerInput.Enable();

            PlayerInput.PlayerMosion.Enable();
            PlayerInput.PlayerMosion.SetCallbacks(this);
        }

        private void OnDisable()
        {
            PlayerInput.PlayerMosion.Disable();
            PlayerInput.PlayerMosion.RemoveCallbacks(this);
        }
        public void OnMovement(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
            Debug.Log($"MoveMent{MovementInput}");
        }
    }

}
 