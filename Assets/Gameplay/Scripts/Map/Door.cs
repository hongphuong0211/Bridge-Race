using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : GameUnit
{
    public Collider cd;
    public MapManager currentMap;
    public MapManager map;
    private void OnCollisionEnter(Collision collision)
    {
        CharacterInstance otherCInstance = LevelManager.Instance.GetCharacterInstance(collision.collider);
        if (map != null && collision.transform.position.z < tf.position.z)
        {
            cd.isTrigger = true;
            if (currentMap != map)
            {
                if (otherCInstance != null)
                {
                    otherCInstance.MapManager = map;
                    map.SpawnBrickInStage(otherCInstance.typeCharacter, 225 / LevelManager.Instance.Settings.enemyColors.Count);
                }
            }
        }else 
        {
            if (otherCInstance != null && currentMap == otherCInstance.MapManager)
            {
                cd.isTrigger = true;
            }
            else
            {
                cd.isTrigger = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        cd.isTrigger = false;
    }
}
