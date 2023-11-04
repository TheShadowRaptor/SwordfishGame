using SwordfishGame;
using System.Collections;
using UnityEngine;

public class NewEnemyController : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] private float swimSpeed = 5;
    [SerializeField] private float pullingSpeed = 2; // Adjust this value for the pulling speed

    bool beingPulled;
    float pullTimer = 5f;
    float pullTimerReset;


    public enum EnemyStates
    {
        swimming,
        attacking,
        dying
    }

    private EnemyStates state;
    public EnemyStates State { get => state; }

    private Vector3 initialPosition; // Store the initial position of the enemy

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = EnemyStates.swimming;
        initialPosition = transform.position; // Store the initial position
        pullTimerReset = pullTimer;
        StartCoroutine(EnemyRoutine());
    }

    private IEnumerator EnemyRoutine()
    {
        while (true)
        {
            switch (state)
            {
                case EnemyStates.swimming:
                    Swimming();
                    break;

                case EnemyStates.attacking:
                    break;

                case EnemyStates.dying:
                    break;
            }
            yield return null;
        }
    }

    private void Swimming()
    {
        rb.velocity += Vector3.forward * swimSpeed * Time.fixedDeltaTime;
    }

    private void Attacking()
    {

    }

    private void Dying()
    {

    }

    public IEnumerator ChangeState(EnemyStates state)
    {
        this.state = state;
        yield break;
    }
}