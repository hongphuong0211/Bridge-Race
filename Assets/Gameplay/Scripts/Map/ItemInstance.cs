using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemInstance : GameUnit
{
    private int typeItems;
    private MeshRenderer renderItem;
    private MapManager mapManager;
    public int TypeItems => typeItems;
    private void Awake()
    {
        renderItem = GetComponent<MeshRenderer>();
    }
    public void SetItem(int type, MapManager curStage)
    {
        typeItems = type;
        renderItem.material.color = LevelManager.Instance.Settings.enemyColors[type];
        mapManager = curStage;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        mapManager.DespawnBrick(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterInstance otherCInstance = LevelManager.Instance.GetCharacterInstance(other);
        if (otherCInstance.typeCharacter == typeItems)
        {
            otherCInstance.pointCharacter += 1;
            OnDespawn();
        }
        
    }
}
