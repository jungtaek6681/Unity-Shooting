using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance;
    public static Manager Inst { get { return instance; } }

    [SerializeField] PoolManager poolManager;
    [SerializeField] ResourceManager resourceManager;

    public static PoolManager Pool { get { return instance.poolManager; } }
    public static ResourceManager Resource { get { return instance.resourceManager; } }

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
        Resource.Init();
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
