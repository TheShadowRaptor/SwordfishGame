using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class Spear : MonoBehaviour
    {
        [SerializeField] protected SpearHitBox hitBox;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void ParentSpear(GameObject gameObject)
        {
            this.gameObject.transform.parent = gameObject.transform;
            Vector3 scale = this.gameObject.transform.localScale;
            scale = new Vector3(0.02f, 0.02f, 2);
            this.gameObject.transform.localScale = scale;
        }

        public void OrintateSpear(GameObject gameObject)
        {
            this.gameObject.transform.position = gameObject.transform.position;
            this.gameObject.transform.rotation = gameObject.transform.rotation;
        }
    }
}
