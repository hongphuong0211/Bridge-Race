using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(Collider))]
public class ItemInstance : GameUnit
{
    private int typeItems;
    private bool isLooted  = false;
    public bool IsLooted => isLooted;
    private MeshRenderer renderItem;
    private MapManager mapManager;
    public Rigidbody rb;
    public Vector3 targetPos;

    public int TypeItems => typeItems;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        renderItem = GetComponent<MeshRenderer>();
    }
    public void SetItem(int type, MapManager curStage)
    {
        isLooted = false;
        rb.isKinematic = true;
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
        if (!isLooted && otherCInstance != null && otherCInstance.typeCharacter == typeItems)
        {
            isLooted = true;
            otherCInstance.pointCharacter += 1;
            OnDespawn();
            
            tf.SetParent(otherCInstance.stackBrickTF);
            if (otherCInstance.stackBrick.Count > 0) {
                ItemInstance brick = otherCInstance.stackBrick.Peek();
                targetPos = brick.targetPos + Vector3.up * 0.05f;       
            }
            else
            {
                targetPos = - Vector3.forward * 0.05f + Vector3.up * 0.2f;
            }
            Vector3[] waypoints = new Vector3[] { tf.localPosition, Vector3.one * 0.5f - Vector3.forward, targetPos };
            otherCInstance.stackBrick.Push(this);
            tf.DOLocalPath(waypoints, 0.8f, PathType.CatmullRom, PathMode.Full3D, 0);
            tf.DOLocalRotate(Vector3.zero, 0.8f);
        }
        
    }
}
