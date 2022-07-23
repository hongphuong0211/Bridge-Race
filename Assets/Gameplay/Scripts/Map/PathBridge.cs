using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class PathBridge : GameUnit
{
    public Collider door;
    private int typeItems = -1;
    private MeshRenderer renderItem;
    private MapManager mapManager;
    public int TypeItems => typeItems;
    private void Awake()
    {
        renderItem = GetComponent<MeshRenderer>();
    }
    public void Setup(int type)
    {
        typeItems = type;
        door.isTrigger = false;
        if (type > -1)
        {
            renderItem.enabled = true;
            renderItem.material.color = LevelManager.Instance.Settings.enemyColors[type];
        }
        else
        {
            renderItem.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CharacterInstance otherCInstance = LevelManager.Instance.GetCharacterInstance(collision.collider);
        if (otherCInstance != null && otherCInstance.typeCharacter != typeItems)
        {
            if (otherCInstance.pointCharacter > 0)
            {
                Setup(otherCInstance.typeCharacter);
                ItemInstance brick = otherCInstance.BuildBridge();
                SimplePool.Despawn(brick);
                door.isTrigger = true;
                return;
            }
        }
        door.isTrigger = false;
    }
}
