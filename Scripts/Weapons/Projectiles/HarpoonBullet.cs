using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class HarpoonBullet : MonoBehaviour
{
    public bool shot = false;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (shot)
        {
            Traveling();
        }
    }

    void Traveling() 
    {
        rb.velocity = Vector3.forward * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject swordfishPrefab = other.gameObject;
            transform.parent = swordfishPrefab.transform; 
            shot = false;
        }
    }
}
