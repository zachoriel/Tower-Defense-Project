using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerMissile : MonoBehaviour
{
    ObjectPooler objectPooler;

    private void Start() 
    {
        objectPooler = ObjectPooler.Instance;
    }

    public void CasingEffect()
    {
        objectPooler.SpawnFromPool("MissileCasing", transform.position, Quaternion.identity);
    }
}
