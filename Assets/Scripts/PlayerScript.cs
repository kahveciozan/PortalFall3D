using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Animator animator;
    public Vector3 startSpeed;

    public GameObject marker1, marker2;
    private float arrowOffsetY = 0;
    public float arrowSpeed = 0.6f;
    private float markerOffset = 0.1f;

    /* --- Particle System Variables --- */
    public ParticleSystem particleBomb;
    public GameObject portal1, portal2;

    public Image healthBar;

    public bool isFalling = false, isDead = false, isTap = true, isWin = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void StartSpeedControl()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Run"))
        {
            transform.position += startSpeed * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {      
        StartSpeedControl();

        if(isFalling && !isDead)
            GamePlay();

    }


    private void GamePlay()
    {
        marker1.transform.position = new Vector3(marker1.transform.position.x, transform.position.y, marker1.transform.position.z); // Marker1 playeri takip etsin

        if (Input.GetButton("Fire1") && !(healthBar.fillAmount == 0) && isTap)
        {
            // Portallari aktif et
            portal1.SetActive(true);        
            portal2.SetActive(true);

            //Marker2 hizlansin
            Vector3 temp = marker1.transform.position - new Vector3(0, arrowOffsetY, 0);
            marker2.transform.position = new Vector3(temp.x, Mathf.Clamp(temp.y, 4f, 500f), temp.z);
            arrowOffsetY = arrowOffsetY + arrowSpeed;

            // Portallari markerlara gore duzenle
            portal1.transform.position = new Vector3(portal1.transform.position.x, marker1.transform.position.y, portal1.transform.position.z);
            portal2.transform.position = new Vector3(portal2.transform.position.x, marker2.transform.position.y, portal2.transform.position.z);
        }
        else if ((transform.position.y - markerOffset > marker2.transform.position.y) && (!Input.GetButton("Fire1") || (Input.GetButton("Fire1") && healthBar.fillAmount == 0) ))
        {
            StartTapAnimation();
            isTap = false;

            //Tap yok ve Marker2 kucuk oldugu surece isinlan
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, marker2.transform.position.y, Time.fixedDeltaTime * 5), transform.position.z);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            StartFallAnimation();
            isTap = true;
            // Tap yoksa marker2 pos marker1 olsun
            marker2.transform.position = marker1.transform.position;
            arrowOffsetY = 0;

            // Portallari pasif et
            portal1.SetActive(false);
            portal2.SetActive(false);

            gameObject.GetComponent<CapsuleCollider>().enabled = true;

        }
    }


    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FallingTrigger"))
        {
            // TO DO (Bomb explosion Sound)
            particleBomb.Play(other);

            animator.SetBool("isFalling", true);
            startSpeed = new Vector3(0, 0, 0);

            isFalling = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("isDying2");

            isFalling = false;
            isDead = true;
        }

        if (collision.collider.CompareTag("Win") )
        {
           
            isFalling = false;

            if (!isDead)
            {
                Debug.Log("KAZANDINIZ");
                WinAnimation();
                isWin = true;
            }  
            else
                Debug.Log("ÖLDÜN TEKRAR DENE");


        }

    }

    private void StartTapAnimation()
    {
        animator.SetBool("isFalling",false);
        animator.SetBool("isTapping", true);

    }

    private void StartFallAnimation()
    {
        animator.SetBool("isTapping", false);
        animator.SetBool("isFalling", true);

    }

    private void WinAnimation()
    {

        animator.SetBool("isWin", true);

    }




} // Class
