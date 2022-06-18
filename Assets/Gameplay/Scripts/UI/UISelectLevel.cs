using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectLevel : MonoBehaviour
{
    public UIButtonLevel uiLevelPrefabs;
    public Transform viewContent;
    private List<UIButtonLevel> levelList;

    private void Awake()
    {
        SpawnViewLevel();
    }
    private void OnEnable()
    {
        ReloadViewLevel();
    }

    private void SpawnViewLevel()
    {
        levelList = new List<UIButtonLevel>();
        int numberLevel = Resources.LoadAll("Level/").Length;
        for (int i = 0; i < numberLevel; i++)
        {
            UIButtonLevel instance = Instantiate(uiLevelPrefabs, viewContent);
            instance.SetLevel(i + 1);
            levelList.Add(instance);
        }
    }

    private void ReloadViewLevel()
    {
        for(int i = 0; i < levelList.Count; i++)
        {
            levelList[i].UpdateView();
        }
    }
}
