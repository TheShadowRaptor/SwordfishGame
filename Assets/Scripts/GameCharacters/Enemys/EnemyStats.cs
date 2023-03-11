using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfAlive();
        if (Deactivate()) gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    bool Deactivate()
    {
        if (isAlive) return false;
        else return true;
    }
}
