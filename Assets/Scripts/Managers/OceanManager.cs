using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class OceanManager : MonoBehaviour
    {
        public float wavesFrequency = 0.001f;
        public float wavesSpeed = 0.001f;
        public float wavesHeight = 4;
        public GameObject ocean;

        public Material oceanMat;
        Texture2D wavesDisplacement;

        // Start is called before the first frame update
        void Start()
        {
            SetVariables();
        }

        void SetVariables()
        {
            oceanMat = ocean.GetComponent<Renderer>().sharedMaterial;
            wavesDisplacement = (Texture2D)oceanMat.GetTexture("_WavesDisplacment");

        }

        // Update is called once per frame
        void Update()
        {
            if (MasterSingleton.Instance.GameManager.gameState == GameManager.GameState.mainmenu) ocean.GetComponent<MeshRenderer>().enabled = false;
            else ocean.GetComponent<MeshRenderer>().enabled = true;
        }

        public float WaterHeightAtPosition(Vector3 position)
        {
            return ocean.transform.position.y + wavesDisplacement.GetPixelBilinear(position.x * wavesFrequency * ocean.transform.localScale.x, position.z * wavesFrequency + Time.time * wavesSpeed).g * wavesHeight;
        }

        private void OnValidate()
        {
            if (!oceanMat)
                SetVariables();

            UpdateMaterial();
        }

        void UpdateMaterial()
        {
            oceanMat.SetFloat("_WavesFrequency", wavesFrequency);
            oceanMat.SetFloat("_WavesSpeed", wavesSpeed);
            oceanMat.SetFloat("_WavesHeight", wavesHeight);
        }
    }
}
