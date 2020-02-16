using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public List<GameObject> elements = new List<GameObject>();
    public bool shouldExpand;

    public ObjectPoolItem(GameObject objectToPool, bool shouldExpand = true, int amountToPool = 0)
    {
        this.objectToPool = objectToPool;
        this.shouldExpand = shouldExpand;
        this.amountToPool = amountToPool;
    }
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;

    Dictionary<string, ObjectPoolItem> pooledObjects;

    private GameObject addNewElementForPoolItem(ObjectPoolItem poolItem)
    {
        GameObject obj = (GameObject)Instantiate(poolItem.objectToPool);
        obj.SetActive(false);
        poolItem.elements.Add(obj);
        return obj;
    }

    public void initObjectToPool(GameObject objectToPool, string objectTag, bool shouldListExpand = true, int amountInPool = 0)
    {
        if (pooledObjects.ContainsKey(objectTag))
        {
            //Game object has already been initialized
            //No need to reinit it
            return;
        }
        ObjectPoolItem objectPoolItem = new ObjectPoolItem(objectToPool, shouldListExpand, amountInPool);
        pooledObjects.Add(objectTag, objectPoolItem);
        for (int i = 0; i < objectPoolItem.amountToPool; i++)
        {
            addNewElementForPoolItem(objectPoolItem);
        }
    }

    void Awake()
    {
        SharedInstance = this;
        pooledObjects = new Dictionary<string, ObjectPoolItem>();
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("shdfdfkshf");
    }

    public GameObject GetPooledObject(string objectTag)
    {
        if(pooledObjects.ContainsKey(objectTag))
        {
            var objectPoolItem = pooledObjects[objectTag];
            foreach (var gameObjectElement in objectPoolItem.elements)
            {
                if(!gameObjectElement.activeInHierarchy)
                {
                    //If object is not active then we return it
                    return gameObjectElement;
                }
            }
            //We couldn't find any object, lets see if we can create one
            if(objectPoolItem.shouldExpand)
            {
                return addNewElementForPoolItem(objectPoolItem);
            }
            else
            {
                return null;
            }
        }
        else
        {
            throw new System.ArgumentException("Invalid key, there is no pool for this object, you need to init it first", "tag");
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
