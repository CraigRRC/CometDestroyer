using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Spawner spawner;
    public GravityMod comet;
    public GravityMod miniComet;
    public StarGravityMod stars;

    public float levelOneGravity = 0f;
    public float levelTwoGravity = 0.1f;
    public float levelThreeGravity = 0.2f;
    public float levelFourGravity = 0.3f;
    public float levelFiveGravity = 0.4f;
    private void Start()
    {
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
                Debug.Log("Level 1");
                comet.gravity = levelOneGravity;
                miniComet.gravity = levelOneGravity;
                stars.gravity = levelOneGravity;
                break;
            case 1:
                Debug.Log("Level 2");
                comet.gravity = levelTwoGravity;
                miniComet.gravity = levelTwoGravity;
                stars.gravity = levelTwoGravity;
                break;
            case 2:
                Debug.Log("Level 3");
                comet.gravity = levelThreeGravity;
                miniComet.gravity = levelThreeGravity;
                stars.gravity= levelThreeGravity;
                break;
            case 3:
                Debug.Log("Level 4");
                comet.gravity = levelFourGravity;
                miniComet.gravity = levelFourGravity;
                stars.gravity = levelFourGravity;
                break;
            case 4:
                Debug.Log("Level 5");
                comet.gravity = levelFiveGravity;
                miniComet.gravity = levelFiveGravity;
                stars.gravity = levelFiveGravity;
                break;
            case 5:
                Debug.Log("End?");
                break;
            default:
                Debug.Log("do Nothing");
                break;
        }
    }

    private void OnDisable()
    {
        spawner.OnLevelSwitch -= OnLevelUpdate;
    }

}
