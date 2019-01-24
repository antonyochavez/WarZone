using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    void Awake()
    {
        musicSingleton();
    }

    private void musicSingleton()
    {
        int numberOfMusicSessions = FindObjectsOfType<Music>().Length;
        if (numberOfMusicSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
