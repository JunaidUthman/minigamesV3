using UnityEngine;

public class BadgeDisplayManager : MonoBehaviour
{
    public GameObject badge1;
    public GameObject badge2;
    public GameObject badge3;
    public GameObject badge4;

    void Start()
    {
        int level = PlayerGlobalData.Instance.mathLevel;


        // Activate badges depending on the mathLevel
        if (level >= 1)
            badge1.SetActive(true);
        if (level >= 3)
            badge2.SetActive(true);
        if (level >= 5)
            badge3.SetActive(true);
        if (level >= 6)
            badge4.SetActive(true);
    }
}
