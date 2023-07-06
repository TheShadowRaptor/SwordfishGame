using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class ShipStats : CharacterStats
    {
        static private ShipStats instance;
        static public ShipStats Instance { get => instance; }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            else
            {
                Destroy(gameObject);

            }
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
