//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : Singleton<DataManager>
{
    public static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataManager();
            }
            return instance;
        }
    }
}


[System.Serializable]
public class GameData
{
    public ResourceData resourceData;
    public ProgressData progressData;
}
[System.Serializable]
public class ResourceData
{
    public int gold;
    public int gem;
    public List<int> pieces;
    public List<int> tickets;
    public List<int> stones;
}
[System.Serializable]
public class ProgressData
{
    public List<int[]> scoreLevel;
    public List<int> mission;
    public List<int> achievement;
    public Tuple<int, int> rcokieQuest;
}
