using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Spawner spawner;
    private void Start()
    {
        Debug.Log("TestLe1");
        spawner.OnLevelSwitch += OnLevelUpdate;
    }

    void Update()
    {
        
    }

    public void OnLevelUpdate(Enum level)
    {
        Debug.Log("testLE2");
        Debug.Log(level);
    }
    
}
