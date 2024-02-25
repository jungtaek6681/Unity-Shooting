using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform muzzlePoint;
    [SerializeField] float maxDistance;
    [SerializeField] int damage;
    [SerializeField] float power;

    public void Fire()
    {
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hitInfo, maxDistance))
        {
            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hitInfo.distance, Color.yellow, 0.1f);

            IDamagable damagable = hitInfo.collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(damage);
            }

            Rigidbody rigidbody = hitInfo.collider.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddForceAtPosition(muzzlePoint.forward * power, hitInfo.point, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * maxDistance);
        }
    }
}
