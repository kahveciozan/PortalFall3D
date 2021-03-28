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
    public void PlayAgain()
    {
        SceneManager.LoadScene(builtIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(builtIndex+1);
    }


    IEnumerator PopUpImege()
    {
        yield return new WaitForSeconds(2f);
        popUpImage.SetActive(true);
    }
}
