using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField] PooledObject hitEffect;

    private void Start()
    {
        Manager.Pool.CreatePool(hitEffect, 5, 10);
    }
}
