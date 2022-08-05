using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    private int sceneIndex = 0;
    public void LoadGame()
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        if(sceneIndex  < SceneManager.sceneCountInBuildSettings-1)
        {
            sceneIndex++;
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            sceneIndex = 0;
            SceneManager.LoadScene(sceneIndex);
        }
        
    }
}
