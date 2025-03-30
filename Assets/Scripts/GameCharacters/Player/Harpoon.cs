using SwordfishGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour, IBullet
{
    [Header("Stats")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 5;

    private GameObject selectedTarget;
    public IEnumerator FiredTo(GameObject target)
    {
        selectedTarget = target;
        while (!HitTarget(selectedTarget))
        {
            if (selectedTarget.activeSelf == false) break;
            transform.LookAt(selectedTarget.transform.position);
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            yield return new WaitForFixedUpdate();
        }
        target.GetComponent<HazardEnemy>().TakeDamage(damage);
        this.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
    }

    public bool HitTarget(GameObject target)
    {
        Vector3 newTargetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        if (Vector3.Distance(transform.position, newTargetPos) < 4f) return true;
        return false;
    }
}
