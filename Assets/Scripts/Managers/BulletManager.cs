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
            bulletPool = GetComponent<BulletPool>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
