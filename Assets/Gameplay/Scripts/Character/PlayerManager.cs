using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance { 
        get { 
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerManager>();
            }
            return instance; 
        } 
    }
    
}

public struct StatusSpawn
{
    public Vector3 spawnPoint;
    public float powerSpeed;
    public float powerGravity;
    public float powerJump;
    public StateSizeBall sizeBall;
}
