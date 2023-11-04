using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    [RequireComponent(typeof(Rigidbody))]
    public class BuoyancyObject : MonoBehaviour
    {
        public Transform[] floaters;
        public float underWaterDrag = 3;
        public float underWaterAnglerDrag = 1;
        public float airDrag = 0;
        public float airAnglerDrag = 0.05f;
        public float floatingPower = 15;

        Rigidbody rb;
        OceanManager oceanManager;

        int floatersUnderwater;

        bool underWater;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            oceanManager = MasterSingleton.Instance.OceanManager;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.inHud) return;
            floatersUnderwater = 0;
            if (floaters.Length > 0)
            {
                for (int i = 0; i < floaters.Length; i++)
                {
                    float difference = floaters[i].transform.position.y - oceanManager.WaterHeightAtPosition(floaters[i].position);

                    if (difference < 0)
                    {
                        rb.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), floaters[i].transform.position, ForceMode.Force);
                        floatersUnderwater += 1;
                        if (!underWater)
                        {
                            underWater = true;
                            SwitchState(true);
                        }
                    }
                }

                if (underWater && floatersUnderwater == 0)
                {
                    underWater = false;
                    SwitchState(false);
                }
            }

            void SwitchState(bool isUnderWater)
            {
                if (isUnderWater)
                {
                    rb.drag = underWaterDrag;
                    rb.angularDrag = underWaterAnglerDrag;
                }
                else
                {
                    rb.drag = airDrag;
                    rb.angularDrag = airAnglerDrag;
                }
            }
        }
    }
}
