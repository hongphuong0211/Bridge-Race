using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Dictionary<int, List<ItemInstance>> brickManage;
    public List<Vector3> allPosEmpty;
    private Queue<int> typeToSpawn;
    private void Awake()
    {
        OnInit();
    }
    public void OnInit()
    {
        brickManage = new Dictionary<int, List<ItemInstance>>();
        allPosEmpty = new List<Vector3>();
        typeToSpawn = new Queue<int>();
        for(int i = -14; i < 15; i+=2)
        {
            for(int j = -14; j < 15; j+=2)
            {
                allPosEmpty.Add(new Vector3((float)i/10f, 0.025f,(float)j/10f));
            }
        }
        InvokeRepeating("SpawnBrickAuto", 3f, 0.5f);
    }
    private void SpawnBrickAuto()
    {
        if (typeToSpawn.Count > 0)
        {
            int type = typeToSpawn.Dequeue();
            if (!brickManage.ContainsKey(type))
            {
                return;
            }
            SpawnBrickInStage(type, 1);
        }
    }


    public void SpawnBrickInStage(int type, int number)
    {
        if (!brickManage.ContainsKey(type))
        {
            brickManage.Add(type, new List<ItemInstance>());
        }
        for (int i = 0; i < number; i++)
        {
            ItemInstance item = SimplePool.Spawn<ItemInstance>(LevelManager.Instance.prefabItem);
            if (item != null)
            {
                int numberPosSpawn = Random.Range(0, allPosEmpty.Count);
                item.SetItem(type, this);
                item.transform.position = allPosEmpty[numberPosSpawn];
                allPosEmpty.RemoveAt(numberPosSpawn);
                item.OnInit();
                brickManage[type].Add(item);
            }
        }

    }
    public void DespawnBrick(int type)
    {
        for(int i = 0; i < brickManage[type].Count; i++)
        {
            brickManage[type][i].OnDespawn();
            allPosEmpty.Add(brickManage[type][i].tf.position);
        }
        brickManage.Remove(type);
    }
    public void DespawnBrick(ItemInstance item)
    {
        allPosEmpty.Add(item.tf.position);
        for(int i = 0; i < brickManage[item.TypeItems].Count; i++)
        {
            if(brickManage[item.TypeItems][i] == item)
            {
                SimplePool.Despawn(item);
                brickManage[item.TypeItems].RemoveAt(i);
                typeToSpawn.Enqueue(item.TypeItems);
                return;
            }
        }
    }
    public ItemInstance GetNearestIntance(int type, Transform charPos)
    {
        ItemInstance nearest = null;
        float minDistance = int.MaxValue;
        int countList = brickManage[type].Count;
        for (int i = 0; i < countList; i++)
        {
            //float distance = Vector3.SqrMagnitude(charPos.position - brickManage[type][i].tf.position);
            //if (distance < minDistance)
            //{
            //    minDistance = distance;
            //    nearest = brickManage[type][i];
            //}
            if (brickManage[type][i].gameObject.activeSelf)
            {
                float distance = Vector3.SqrMagnitude(charPos.position - brickManage[type][i].tf.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = brickManage[type][i];
                }
            }
            else
            {
                SimplePool.Despawn(brickManage[type][i]);
                brickManage[type].RemoveAt(i);
                --countList;
                --i;
            }
        }
        return nearest;
    }
}
