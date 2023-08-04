using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonotDestory : MonoBehaviour
{
    private static bool isSpawned;
    // Start is called before the first frame update
    void Start()
    {
        if (isSpawned)
        {
            Destroy(this.gameObject);
            return;
        }

        isSpawned = true;
        DontDestroyOnLoad(gameObject);    
    }
    
}
