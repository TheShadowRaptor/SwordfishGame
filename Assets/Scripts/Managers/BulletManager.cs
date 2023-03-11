using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class BulletManager : MonoBehaviour
    {
        public BulletPool bulletPool;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.gameplay) return;
            if (bulletPool == null)
            {
                bulletPool = GameObject.Find("BulletPool").GetComponent<BulletPool>();
            }
        }
    }
}
