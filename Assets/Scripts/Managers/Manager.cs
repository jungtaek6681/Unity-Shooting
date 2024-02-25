using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance;
    public static Manager Inst { get { return instance; } }

    [SerializeField] PoolManager poolManager;

    public static PoolManager Pool { get { return instance.poolManager; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        instance = FindObjectOfType<Manager>(true);
        if (instance == null)
        {
            Debug.LogError("Manager : Can't find singleton instance");
            return;
        }
        DontDestroyOnLoad(instance);

        Pool.Init();
    }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
}
