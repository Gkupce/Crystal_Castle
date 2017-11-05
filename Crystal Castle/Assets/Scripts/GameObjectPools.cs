using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPools : MonoBehaviour {

    public GameObject[] objectsToPool;

    Dictionary<string, GameObject> objectDic = new Dictionary<string, GameObject>();
    Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();

    public static GameObjectPools Instance;
    
    private void Start()
    {
        Instance = this;
        foreach(GameObject g in objectsToPool)
        {
            objectDic.Add(g.name.ToLower(),g);
            pools.Add(g.name.ToLower(), new List<GameObject>());
        }
    }
    

    public GameObject GetPooledObject(string objectName)
    {
        GameObject g = null;
        objectName = objectName.ToLower();

        if (pools.ContainsKey(objectName))
        {
            if (pools[objectName].Count > 0)
            {
                g = pools[objectName][0];
                pools[objectName].RemoveAt(0);
            }
            else
            {
                g = Instantiate(objectDic[objectName]);
                g.AddComponent<PooledGameObject>().pool = objectName;
            }
        }
        else
        {
            Debug.LogError(objectName + " is not pooled! Check if it's in the objects to pool array.",gameObject);
        }


        return g;
    }

    public void ReturnToPool(string pool, GameObject go)
    {
        pools[pool].Add(go);
    }
}

