using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledGameObject : MonoBehaviour {

    public string pool;

    void OnDisable()
    {
        GameObjectPools.instance.ReturnToPool(pool,gameObject);
    }
}
