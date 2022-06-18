using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Dictionary<int, int> brickManage;
    public List<Vector3> allPosEmpty;
    private IEnumerator SpawnBrick(int type, int number)
    {
        yield return new WaitForSeconds(5f);
        if (brickManage.ContainsKey(type))
        {
            brickManage[type] += number;
            for(int i = 0; i < number; i++)
            {
                ItemInstance item = PoolingManager.SharedInstance.GetPooledBrick();
                if(item != null)
                {
                    int numberPosSpawn = Random.Range(0, allPosEmpty.Count);
                    item.typeItems = type;
                    item.gameObject.SetActive(true);
                }
        }
    }
    
}
