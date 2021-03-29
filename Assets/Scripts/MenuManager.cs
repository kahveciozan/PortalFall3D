using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    // Button OnClick
    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    // Button OnClick
    public void Quit()
    {
        Application.Quit();
        Debug.Log("OYUNDAN CIKILDI");
    }

}
