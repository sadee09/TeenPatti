using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGame : MonoBehaviour
{
    public static SpriteGame instance;
    public Sprite[] arr_Cards;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Corrected assignment here
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}