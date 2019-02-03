using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public PlayerSettings playerSettings;

    // Use this for initialization

    public void LoadByIndex(int scenesIndex)
    {
        if (playerSettings.currentAtom != null)
            SceneManager.LoadScene(scenesIndex);
        else
        {
            GameObject popUp = GameObject.FindGameObjectWithTag("popup");
            popUp.SetActive(true);
        }
    }
}
