using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rig aimRig;
    [SerializeField] Rig weaponHolderRig;
    [SerializeField] float reloadTime;

    private bool reloading;

    private void Fire()
    {
        animator.SetTrigger("Fire");
    }

    private void Reload()
    {
        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        animator.SetTrigger("Reload");
        reloading = true;
        aimRig.weight = 0f;
        weaponHolderRig.weight = 0f;

        yield return new WaitForSeconds(reloadTime);

        reloading = false;
        aimRig.weight = 1f;
        weaponHolderRig.weight = 1f;
    }

    private void OnFire(InputValue value)
    {
        if (reloading)
            return;

        Fire();
    }

    private void OnReload(InputValue value)
    {
        if (reloading)
            return;

        Reload();
    }
}
