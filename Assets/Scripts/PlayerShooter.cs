using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Fire()
    {
        animator.SetTrigger("Fire");
    }

    private void Reload()
    {
        animator.SetTrigger("Reload");
    }

    private void OnFire(InputValue value)
    {
        Fire();
    }

    private void OnReload(InputValue value)
    {
        Reload();
    }
}
