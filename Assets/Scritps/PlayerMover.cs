using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float jumpSpeed;

    private CharacterController controller;
    private Animator animator;

    private Vector3 moveDir;
    private float ySpeed = 0;
    private bool walk = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        moveRoutine = StartCoroutine(MoveRoutine());
        jumpRoutine = StartCoroutine(JumpRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(moveRoutine);
        StopCoroutine(jumpRoutine);
    }

    Coroutine moveRoutine;
    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (walk)
            {
                controller.Move(transform.forward * moveDir.z * walkSpeed * Time.deltaTime);
                controller.Move(transform.right * moveDir.x * walkSpeed * Time.deltaTime);
            }
            else
            {
                controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
                controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
            }

            animator.SetFloat("XInput", moveDir.x, 0.1f, Time.deltaTime);
            animator.SetFloat("YInput", moveDir.z, 0.1f, Time.deltaTime);

            yield return null;
        }
    }

    Coroutine jumpRoutine;
    private IEnumerator JumpRoutine()
    {
        while (true)
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;

            if (GroundCheck() && ySpeed < 0)
                ySpeed = -1;

            controller.Move(Vector3.up * ySpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDir = new Vector3(input.x, 0, input.y);

        animator.SetBool("Move", input.sqrMagnitude > 0);
    }

    private void OnJump(InputValue value)
    {
        if (GroundCheck())
            ySpeed = jumpSpeed;
    }

    private void OnWalk(InputValue value)
    {
        walk = value.isPressed;

        animator.SetBool("Walk", walk);
    }

    private bool GroundCheck()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.5f, LayerMask.GetMask("Enviroment"));
    }
}
