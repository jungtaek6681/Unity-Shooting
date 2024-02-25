using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;

    private Vector3 moveDir;
    private float ySpeed = 0;

    private void Update()
    {
        Move();
        Fall();
    }

    private void Move()
    {
        controller.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    private void Fall()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (controller.isGrounded && ySpeed < 0)
        {
            ySpeed = 0;
        }

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    private void Jump()
    {
        ySpeed = jumpSpeed;
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDir.x = input.x;
        moveDir.z = input.y;
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && controller.isGrounded)
        {
            Jump();
        }
    }
}
