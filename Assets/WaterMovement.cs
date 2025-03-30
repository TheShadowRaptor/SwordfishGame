using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1; 
    [SerializeField] private Vector3 moveDir = Vector3.back;
    [SerializeField] private Vector3 resetFromPos = new Vector3(0, 0, 1900);
    [SerializeField] private Vector3 resetToPos = new Vector3(0, 0, 1900);
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    IEnumerator Move()
    {
        while (true) 
        {
            transform.Translate(moveDir * moveSpeed * Time.deltaTime);
            if (transform.position.z < resetFromPos.z) transform.Translate(resetToPos);
            yield return new WaitForFixedUpdate();
        }
    }
}
