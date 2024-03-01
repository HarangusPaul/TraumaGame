using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        public float speed = 5f;
        public float gravity = -15f;
        public float crouchHeight = 0.5f;
        public float standingHeight = 2f;

        private bool isCrouching = false;

        private bool hasMovedBefore = false;
        

        Vector3 velocity;

        // Update is called once per frame
        void Update()
        {
            HandleMovement();

            // Toggle crouch on/off when the player presses the C key (you can use any key you prefer).
            if (Input.GetKeyDown(KeyCode.C))
            {
                ToggleCrouch();
            }
        }

        void HandleMovement()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * (isCrouching ? speed / 2f : speed) * Time.deltaTime);

            HandleGravity();
            
            if (!hasMovedBefore)
            {
                hasMovedBefore = true;
                GameManager.instance.updateTask(0);
            }
            
            // Apply the crouch or stand height
            controller.height = Mathf.Lerp(controller.height, isCrouching ? crouchHeight : standingHeight, Time.deltaTime * 10f);
        }

        void HandleGravity()
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        void ToggleCrouch()
        {
            isCrouching = !isCrouching;
        }
    }
}
