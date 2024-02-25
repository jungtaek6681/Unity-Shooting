using UnityEngine;

public class Target : MonoBehaviour, IDamagable
{
    [SerializeField] int hp;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
