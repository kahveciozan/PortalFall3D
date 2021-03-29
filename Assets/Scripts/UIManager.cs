using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject popUpImage;
    private int builtIndex;
    public PlayerScript ps;

    private void Start()
    {
        builtIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if(ps.isDead || ps.isWin)
        {
            StartCoroutine(PopUpImege());
        }
    }

    // Open PopUp Menu 
    IEnumerator PopUpImege()
    {
        yield return new WaitForSeconds(2f);
        popUpImage.SetActive(true);
    }

    // Button OnClick
    public void PlayAgain()
    {
        SceneManager.LoadScene(builtIndex);
    }

    // Button OnClick
    public void NextLevel()
    {
        SceneManager.LoadScene(builtIndex+1);
    }


    
}
