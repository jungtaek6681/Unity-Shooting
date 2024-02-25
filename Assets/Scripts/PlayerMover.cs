using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;

    [Header("Spec")]
    [SerializeField] float moveSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpSpeed;

    private Vector3 moveDir;
    private float ySpeed = 0;
    private bool isWalk = false;

    private void Update()
    {
        Move();
        Fall();
    }

    private void Move()
    {
        if (isWalk)
        {
            controller.Move(transform.forward * moveDir.z * walkSpeed * Time.deltaTime);
            controller.Move(transform.right * moveDir.x * walkSpeed * Time.deltaTime);

            animator.SetFloat("XSpeed", moveDir.x * walkSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("YSpeed", moveDir.z * walkSpeed, 0.1f, Time.deltaTime);
        }
        else
        {
            controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
            controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);

            animator.SetFloat("XSpeed", moveDir.x * moveSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("YSpeed", moveDir.z * moveSpeed, 0.1f, Time.deltaTime);
        }
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

    private void OnWalk(InputValue value)
    {
        isWalk = value.isPressed;
    }
}
