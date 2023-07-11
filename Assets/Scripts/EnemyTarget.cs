using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    static public List<GameObject> rightTargets = new List<GameObject>();
    static public List<GameObject> leftTargets = new List<GameObject>();

    public bool isRight = false;

    private void OnEnable()
    {
        if (isRight) rightTargets.Add(this.gameObject);
        else leftTargets.Add(this.gameObject);
        if (isRight) Debug.Log($"right targets = {rightTargets.Count}");
        else Debug.Log($"left targets = {leftTargets.Count}");
    }

    private void OnDisable()
    {
        rightTargets.Clear(); leftTargets.Clear();
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
