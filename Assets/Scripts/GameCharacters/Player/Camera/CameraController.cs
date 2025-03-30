using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class CameraController : MonoBehaviour
    {
        GameObject player;
        public void Start()
        {
            player = GameObject.Find("Player");
        }

        private void LateUpdate()
        {
            Vector3 pos = transform.position;
            Vector3 newPos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            pos = Vector3.Lerp(pos, newPos, 0.9f);
            transform.position = pos;
        }
    }
}
