using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyStats : MonoBehaviour
{
    [SerializeField] private GameObject _buoy;
    [SerializeField] int health = 1;
    [SerializeField] float stamina = 5;
    private float deathTimer = 1.5f;

    public int Health { get => health; }
    public float Stamina { get => stamina; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator Struggle() 
    {
        yield return new WaitForSeconds(stamina);

    }

    public IEnumerator TakeDamage(int damage) 
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            // gameObject.GetComponent<Rigidbody>().useGravity = true;

            yield return new WaitForSeconds(deathTimer);

            GameObject buoy = Instantiate(_buoy, transform.position, Quaternion.identity);
            Buoys.buoys.Add(buoy);

            gameObject.GetComponent<Rigidbody>().useGravity = false;
            this.gameObject.SetActive(false);
        }
        yield break;
    }
}
