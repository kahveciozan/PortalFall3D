using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public Image image;
    public float waitTime = 30f;
    public PlayerScript ps;

    void Start()
    {
        image = GetComponent<Image>();        
    }

    void Update()
    {
        if (ps.animator.GetBool("isTapping"))
        {
            image.fillAmount -= 1f / waitTime * Time.deltaTime;

            Debug.Log(image.fillAmount);
        }
        else
        {
            image.fillAmount = 1f;
        }

        
    }
}
