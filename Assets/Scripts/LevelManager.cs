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

    public void OnLevelUpdate(int level)
    {
        switch (level)
        {
            case 0:
                Debug.Log("do something");
                break;
            case 1:
                Debug.Log("Level 1");
                break;
            case 2:
                Debug.Log("Level 2");
                break;
            case 3:
                Debug.Log("Level 3");
                break;
            case 4:
                Debug.Log("Level 4");
                break;
            case 5:
                Debug.Log("Level 5");
                break;
            default:
                Debug.Log("do Nothing");
                break;
        }
    }

    private void OnDestroy()
    {
        spawner.OnLevelSwitch -= OnLevelUpdate;
    }

}
