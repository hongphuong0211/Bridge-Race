using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager instance;
    public static PoolingManager SharedInstance { get { 
            if(instance == null)
            {
                instance = FindObjectOfType<PoolingManager>();
            }
            return instance; } }
    
    public ItemInstance brickToPool;
    public int amountToPool;
    private List<ItemInstance> pooledBricks;

    void Start()
    {
        pooledBricks = new List<ItemInstance>();
        ItemInstance tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(brickToPool);
            tmp.gameObject.SetActive(false);
            pooledBricks.Add(tmp);
        }
    }
    public ItemInstance GetPooledBrick()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledBricks[i].gameObject.activeInHierarchy)
            {
                return pooledBricks[i];
            }
        }
        return null;
    }
}
