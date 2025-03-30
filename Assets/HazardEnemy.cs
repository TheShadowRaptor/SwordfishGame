using SwordfishGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SwordfishGame
{
    public class HazardEnemy : HazardObject
    {
        [SerializeField] float swimSpeed;
        [SerializeField] int scoreHeld = 5;
        [SerializeField] int health = 1;

        [SerializeField] Canvas scoreCanvas;
        [SerializeField] Animator animator;

        TextMeshProUGUI scoreTextMesh;

        private void OnEnable()
        {
            GetComponent<Collider>().enabled = true;
            scoreTextMesh = scoreCanvas.GetComponentInChildren<TextMeshProUGUI>();
            health = 1;
            scoreTextMesh.text = '$' + scoreHeld.ToString();
            scoreCanvas.enabled = false;
            activeTime = initActiveTime;
        }

        private void Update()
        {
            MasterSingleton.Instance.PlayerController.EnemyInRange(gameObject);
            if (!IsAlive())
            {
                GetComponent<Collider>().enabled = false;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) 
                {
                    animator.SetBool("Dying", false);
                    gameObject.SetActive(false);
                }
            }
        }

        protected override void HazardSpeed()
        {
            if (IsAlive()) speed = MasterSingleton.Instance.PlayerController.ShipSpeed + swimSpeed;
            else speed = MasterSingleton.Instance.PlayerController.ShipSpeed;
        }

        public void TakeDamage(int amount) 
        {
            health -= amount;
            if (health <= 0)
            {
                scoreCanvas.enabled = true;
                animator.SetBool("Dying", true);
                MasterSingleton.Instance.UIManager.Gameplay.GetComponent<GameplayHud>().AddScore(scoreHeld);
                health = 0;
            }
        }

        public bool IsAlive() 
        {
            if (health > 0) return true;
            else return false;
        }
    }
}
