using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet 
{
    public IEnumerator FiredTo(GameObject target);
    public bool HitTarget(GameObject target);
}
