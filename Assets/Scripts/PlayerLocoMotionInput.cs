using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace David
{
    [DefaultExecutionOrder(-2)]
    public class PlayerLocoMotionInput : MonoBehaviour, PlayerInput.IPlayerMosionActions
    {
        [SerializeField] private bool holdToSprint = true;
        public PlayerInput PlayerInput { get; private set; }
        public bool SprintToggledOn { get; private set; }
        
        public Vector2 MovementInput { get; private set; }

        public bool JumpPressed { get; private set; }

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

        private void LateUpdate()
        {
            JumpPressed = false;
        }

        public void OnMovement(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
            Debug.Log($"MoveMent{MovementInput}");
        }

        public void OnSprintToggle(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("context.performed");
                //SprintToggledOn = holdToSprint || !SprintToggledOn;
                SprintToggledOn = !SprintToggledOn;
            }
            //else if (context.canceled)
            //{
            //    Debug.Log("context.canceled");
            //    SprintToggledOn = !holdToSprint || SprintToggledOn;
            //}
        }

        public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            JumpPressed = true;
        }
    }

}
 