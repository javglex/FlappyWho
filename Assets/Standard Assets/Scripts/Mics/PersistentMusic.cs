using UnityEngine;
using System.Collections;

public class PersistentMusic : MonoBehaviour
{

    private static PersistentMusic instance = null;
    public static PersistentMusic Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // any other methods you need
}