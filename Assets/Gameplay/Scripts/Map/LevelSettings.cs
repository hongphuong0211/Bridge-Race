using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Level")]
public class LevelSettings:ScriptableObject
{
    public List<Color> enemyColors;
}
