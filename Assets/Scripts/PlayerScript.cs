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
    public float arrowSpeed = 0.3f;

    /* --- Particle System Variables --- */
    public ParticleSystem particleBomb;
    public GameObject portal1, portal2;

    public Image healthBar;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {      
        StartSpeedControl();

        TapMethodAnimation();

       // MarkerControl();
    }


    private void TapMethodAnimation()
    {
        marker1.transform.position = new Vector3(marker1.transform.position.x, transform.position.y, marker1.transform.position.z);

        if (Input.GetButton("Fire1"))
        {
            if (animator.GetBool("isFalling"))
            {
                animator.SetBool("isTapping", true);
                animator.SetBool("isFalling", false);
                portal1.SetActive(true);
                portal2.SetActive(true);
            }
            else if(animator.GetBool("isTapping"))
            {
                Vector3 temp = marker1.transform.position - new Vector3(0, arrowOffsetY, 0);
                marker2.transform.position = new Vector3(temp.x, Mathf.Clamp(temp.y, 4f, 500f), temp.z);
                arrowOffsetY = arrowOffsetY + arrowSpeed;

                portal1.transform.position = new Vector3(portal1.transform.position.x, marker1.transform.position.y, portal1.transform.position.z);
                portal2.transform.position = new Vector3(portal2.transform.position.x, marker2.transform.position.y, portal2.transform.position.z);
                // TO DO (Tap Sound)
            }

        }
        else if (!Input.GetButton("Fire1") && transform.position.y > marker2.transform.position.y)                      // Portaldan gecme 
        {

            if (animator.GetBool("isTapping"))
            {
                animator.SetBool("isTapping", false);
                animator.SetBool("isFalling", true);
            }
            else if(animator.GetBool("isFalling"))
            { 
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y,marker2.transform.position.y,Time.deltaTime*5), transform.position.z);
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
        else
        {
            marker2.transform.position = marker1.transform.position;
            arrowOffsetY = 0;

            portal1.SetActive(false);
            portal2.SetActive(false);
            // TO DO (Normal Sound)
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
        }
    }

    private void MarkerControl()
    {
        marker1.transform.position = new Vector3(marker1.transform.position.x, transform.position.y, marker1.transform.position.z);

        if (Input.GetButton("Fire1") && animator.GetBool("isTapping"))
        {
            Vector3 temp = marker1.transform.position - new Vector3(0, arrowOffsetY, 0);
            marker2.transform.position = new Vector3(temp.x, Mathf.Clamp(temp.y ,4f, 500f) ,temp.z);
            arrowOffsetY = arrowOffsetY + 0.3f;
            // TO DO (Tap Sound)

        }
        else if(Input.GetButtonUp("Fire1") && animator.GetBool("isFalling"))
        {
            Vector3 temp = new Vector3(transform.position.x, marker2.transform.position.y,transform.position.z);
            transform.position = temp;
            // TO DO (Hold Sound)
        }
        else
        {
            marker2.transform.position = marker1.transform.position;
            arrowOffsetY = 0;
        }
    }

    private void StartSpeedControl()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Run"))
        {
            transform.position += startSpeed * Time.deltaTime;
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
        }
        if (other.CompareTag("Enemy"))
        {
            animator.SetTrigger("isDying2");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Win"))
        {
            Debug.Log("KAZANDINIZ");
        }
    }

} // Class
