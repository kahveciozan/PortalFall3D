using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranbolineScript : MonoBehaviour
{
    public ParticleSystem particle;
    public PlayerScript ps;

    private void OnCollisionEnter(Collision collision)
    {
        //Confetti Effect Starts
        if (collision.collider.CompareTag("Player") && !ps.isDead)
        {
            particle.Play();
        }
    }


}
