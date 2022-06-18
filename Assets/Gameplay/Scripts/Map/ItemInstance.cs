using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemInstance : MonoBehaviour
{
    public int typeItems;

    private void OnTriggerEnter(Collider other)
    {
        CharacterInstance otherCInstance = LevelManager.Instance.GetCharacterInstance(other);
        if (otherCInstance.typeCharacter == typeItems)
        {
            otherCInstance.pointCharacter += 1;
            gameObject.SetActive(false);
        }
        
    }
}
