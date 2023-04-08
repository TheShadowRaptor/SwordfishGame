using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class OceanManager : MonoBehaviour
    {
        public float waterHeight;
        public float waterSpeed;
        public float waterNoise;

        public GameObject ocean;
        public Material oceanMat;

        public Vector4[] vertexPositions;

        // Start is called before the first frame update
        void Start()
        {
            waterHeight = oceanMat.GetFloat("_WaterHeight");
            waterSpeed = oceanMat.GetFloat("_WaterSpeed");
            waterNoise = oceanMat.GetFloat("_WaterNoise");    
        }

        // Update is called once per frame
        void Update()
        {
            oceanMat.SetFloat("_WaterHeight", waterHeight);
            oceanMat.SetFloat("_WaterSpeed", waterSpeed);
            oceanMat.SetFloat("_WaterNoise", waterNoise);

            vertexPositions = oceanMat.GetVectorArray("_VertexPosition");
        }
    }
}
