using UnityEngine;

public class operationManager : MonoBehaviour
{
    public static operationManager Instance { get; private set; }

    public bool operation1 = false;
    public bool operation2 = false;
    public bool operation3 = false;
    public bool operation4 = false;


    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
