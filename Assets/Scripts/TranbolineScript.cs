using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranbolineScript : MonoBehaviour
{
    public ParticleSystem particle;
    public PlayerScript ps;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !ps.isDead)
        {
            particle.Play();
        }
    }


}
