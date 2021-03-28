using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMan : MonoBehaviour
{
    public Rigidbody bullet;
    public Transform rifle;
    public GameObject bulletParent;

    // Start is called before the first frame update
    void Start()
    {


        InvokeRepeating("Fire", 2.0f, 0.3f);
    }

    private void Fire()
    {
        Rigidbody rb = Instantiate(bullet, rifle.position, Quaternion.identity, bulletParent.transform) as Rigidbody;
        rb.AddForce(rifle.forward * 150f);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
