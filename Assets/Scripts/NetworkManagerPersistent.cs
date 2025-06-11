using UnityEngine;
using Unity.Netcode;

public class NetworkManagerPersistent : MonoBehaviour
{
    private static bool isNetworkManagerPresent = false;

    private void Awake()
    {
        if (!isNetworkManagerPresent)
        {
            isNetworkManagerPresent = true;
            DontDestroyOnLoad(gameObject); // Prevents destruction when switching scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate if it exists
        }
    }
}
