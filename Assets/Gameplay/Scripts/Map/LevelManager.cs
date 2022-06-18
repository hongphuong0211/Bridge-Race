using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
            }
            return instance;
        }
    }
    private Dictionary<Collider, CharacterInstance> dict_col;
    private GameObject currentLevel;
    private LevelSettings currentLevelSettings;
    private MapManager[] allStages;

    public void StartLevel(int level)
    {
        StartCoroutine(LoadLevel(level));
    }

    private IEnumerator LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(Resources.Load<GameObject>("Level/Levels_" + level.ToString()), transform);
        allStages = currentLevel.GetComponentsInChildren<MapManager>();
        currentLevelSettings = Resources.Load<LevelSettings>("LevelSetting/Levels_" + level.ToString());
        yield return new WaitForSeconds(5f);
    }

    public CharacterInstance GetCharacterInstance(Collider other)
    {
        if (dict_col == null)
        {
            dict_col = new Dictionary<Collider, CharacterInstance>();
        }
        if (!dict_col.ContainsKey(other))
        {
            dict_col.Add(other, other.transform.GetComponent<CharacterInstance>());
        }
        return dict_col[other];
    }

    public void TriggerItem(ItemInstance item)
    {

    }
}
