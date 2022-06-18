using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    public List<UIInstance> uiPrefabs;
    private List<UIInstance> uiInstances;

    private void Start()
    {
        uiInstances = new List<UIInstance>();
        OpenUI(0);
    }

    public void OpenUI(int numberUI)
    {
        if (IsActive(numberUI)) return;
        for (int i = 0; i < uiInstances.Count; i++)
        {
            if ((int)uiInstances[i].numberUI == numberUI)
            {
                uiInstances[i].gameObject.SetActive(true);
                if ((int)uiInstances[i].otherNumberUI != numberUI)
                {
                    OpenUI(uiInstances[i].otherNumberUI);
                }
            }
        }
        for (int i = 0; i < uiPrefabs.Count; i++)
        {
            if ((int)uiPrefabs[i].numberUI == numberUI)
            {
                UIInstance ins = Instantiate(uiPrefabs[i], transform);
                uiInstances.Add(ins);
                OpenUI(numberUI);
                return;
            }
        }
    }
    public void OpenUI(NumberUI number)
    {
        OpenUI((int)number);
    }

    public void CloseUI(int numberUI)
    {
        if (IsOpened(numberUI))
        {
            for (int i = 0; i < uiInstances.Count; i++)
            {
                if ((int)uiInstances[i].numberUI == numberUI)
                {
                    uiInstances[i].gameObject.SetActive(false);
                    if ((int)uiInstances[i].otherNumberUI != numberUI)
                    {
                        CloseUI(uiInstances[i].otherNumberUI);
                    }
                    return;
                }
            }
        }
    }
    public void CloseUI(NumberUI number)
    {
        CloseUI((int)number);
    }

    public bool IsActive(int numberUI)
    {
        for (int i = 0; i < uiInstances.Count; i++)
        {
            if ((int)uiInstances[i].numberUI == numberUI)
            {
                return uiInstances[i].gameObject.activeSelf;
            }
        }
        return false;
    }
    public void IsActive(NumberUI number)
    {
        IsActive((int)number);
    }
    public bool IsOpened(int numberUI)
    {
        for (int i = 0; i < uiInstances.Count; i++)
        {
            if ((int)uiInstances[i].numberUI == numberUI)
            {
                return true;
            }
        }
        return false;
    }
    public void IsOpened(NumberUI number)
    {
        IsOpened((int)number);
    }
}
