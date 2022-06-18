using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : PlayerPrefs
{
    public static DataManager instance;
    public static DataManager Instance { 
        get { 
            if(instance == null)
            {
                instance = new DataManager();
            }
            return instance; } }
}
