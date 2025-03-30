using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class PlayerController : MonoBehaviour
    {
        [Header("State Settings")]
        [SerializeField] int health = 1;

        [Header("Movement Settings")]
        [SerializeField] float turnForce;
        [SerializeField] float moveForce;
        [SerializeField] float maxTurnAngle;

        // Player does not move but the world around it does
        [SerializeField] float shipSpeed = 150;
        float initShipSpeed;
        public float ShipSpeed { get => shipSpeed; }

        [Header("Shoot Settings")]
        [SerializeField] float reloadTime = 2;
        [SerializeField] float shootRange = 2;
        [SerializeField] List<GameObject> bulletList = new List<GameObject>();
        [SerializeField] GameObject bulletType;
        [SerializeField] GameObject BulletSpawner;

        float initReloadTime;

        [Header("Other Settings")]
        [SerializeField] LayerMask collisionMasks;

        // Singletons
        InputManager inputManager;

        // Components
        Rigidbody rb;
        Animator animator;
        Vector3 originalEulerAngle;

        // Get/Sets
        public float ShootRange { get => shootRange; }

        // Start is called before the first frame update
        void Start()
        {
            inputManager = MasterSingleton.Instance.InputManager;
            rb = gameObject.GetComponent<Rigidbody>();
            animator = gameObject.GetComponentInChildren<Animator>();

            originalEulerAngle = transform.eulerAngles;
            initReloadTime = reloadTime;
            initShipSpeed = shipSpeed;

            for (int i = 0; i < 10; i++)
            {
                GameObject bullet = Instantiate(bulletType, BulletSpawner.transform.position, bulletType.transform.rotation);
                bullet.transform.parent = MasterSingleton.Instance.LevelManager.transform;
                bullet.SetActive(false);
                bulletList.Add(bullet);
            }
        }

        private void Update()
        {
            reloadTime -= Time.deltaTime;

        }

        private void FixedUpdate()
        {
            if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.gameplay) return;
            if (!IsAlive()) return;

            if (inputManager.JoyMoveRight.Pressed)
            {
                RotateShipTip(1, -125);
                MoveShip(1);
            }
            else if (inputManager.JoyMoveLeft.Pressed) 
            {
                RotateShipTip(-1, 60);
                MoveShip(-1);
            }
            else
            {
                // Apply some damping force to slow down the ship when not moving
                rb.velocity *= 0.95f;
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, originalEulerAngle, 0.05f);
            }
        }

        void RotateShipTip(float horizontalInput, float angle)
        {
            // Calculate the rotation angle based on the input
            float rotationAngle = -horizontalInput * angle;

            Vector3 eulerRotation = transform.eulerAngles;
            eulerRotation.y = rotationAngle;
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, eulerRotation, 0.005f);
        }

        void MoveShip(float horizontalInput)
        {
            // Calculate the movement direction based on the input
            Vector3 movementDirection = new Vector3(horizontalInput, 0, 0);

            // Apply a force to the rigidbody to move it
            rb.AddForce(movementDirection * moveForce, ForceMode.Force);

            //Debug.Log(movementDirection);
            //Debug.Log(moveForce);
        }

        void SinkShip() 
        {
            shipSpeed = 0;
            rb.isKinematic = true;
            animator.SetBool("Sink", true);
            MasterSingleton.Instance.UIManager.SwitchCanvasToGameover();
        }

        void Shoot(GameObject target)
        {
            foreach (GameObject bullet in bulletList)
            {
                if (bullet.activeSelf == false) 
                {
                    bullet.transform.position = BulletSpawner.transform.position;
                    bullet.SetActive(true);
                    StartCoroutine(bullet.GetComponent<IBullet>().FiredTo(target));
                    break;
                }
            }
        }

        public bool IsReloading() 
        {
            if (reloadTime <= 0)
            {
                reloadTime = 0;
                return false;
            }
            return true;
        }

        public void EnemyInRange(GameObject enemy)
        {
            if (!IsAlive()) return;
            if (IsReloading()) return;
            Vector3 dist = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
            Vector3 enemyDist = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);
            if (Vector3.Distance(enemyDist, dist) <= shootRange && 
            enemy.transform.position.z > 20 && 
            enemy.GetComponent<HazardEnemy>())
            {
                Debug.Log("InRange");
                Shoot(enemy);
                reloadTime = initReloadTime;
            }
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            if (health < 0) health = 0;
            MasterSingleton.Instance.UIManager.Gameplay.GetComponent<GameplayHud>().UpdateShipHealth(health);
        }

        public bool IsAlive()
        {
            return health > 0;
        }

        public void ResetStats() {
            health = 3;
            rb.isKinematic = false;
            shipSpeed = initShipSpeed;
            animator.SetBool("Sink", false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Hazard")) 
            {
                Debug.Log("Hit");
                TakeDamage(other.GetComponent<HazardObject>().Damage);
                if (!IsAlive()) SinkShip();
            }
        }
    }
}
