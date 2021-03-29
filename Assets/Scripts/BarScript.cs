using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarScript : MonoBehaviour
{
    public int buildIndex;

    public Image EnergyBar;
    private float waitTime = 0.8f;
    public PlayerScript ps;

    void Start()
    {
        EnergyBar = GetComponent<Image>();
        SetTheTextName();
    }

    void FixedUpdate()
    {
        SetEnergyBar();
    }

    private void SetEnergyBar()
    {
        if (Input.GetButton("Fire1") && ps.isFalling)
        {
            EnergyBar.fillAmount -= 1f / waitTime * Time.fixedDeltaTime;
        }
        else
        {
            EnergyBar.fillAmount = 1f;
        }
    }


    // Set LEvel Text Name
    private void SetTheTextName()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        Text levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Level " + (1 + buildIndex).ToString();
    }
}
