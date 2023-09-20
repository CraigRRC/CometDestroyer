using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Rigidbody2D comet;
    public float forceAmount = 4f;
    private float randomSpawnLocation;
    public float runningTime;
    public bool canWeSpawn = true;
    public float spawnFrequency = 1f;
    public int levelOneSpawnCount = 20;
    public int levelTwoSpawnCount = 40;
    public int levelThreeSpawnCount = 60;
    public int levelFourSpawnCount = 80;
    public int levelFiveSpawnCount = 100;
    private bool newLevel = false;
    public int levelState;

    public Rigidbody2D[] levelOne;
    public Rigidbody2D[] levelTwo;
    public Rigidbody2D[] levelThree;
    public Rigidbody2D[] levelFour;
    public Rigidbody2D[] levelFive;
    public int i = 0;

    public delegate void LevelSwitchEventHandler(int level);
    public event LevelSwitchEventHandler OnLevelSwitch;

    

    private void Awake()
    {
        levelState = (Int32)LevelState.One;

        levelOne = new Rigidbody2D[levelOneSpawnCount];
        levelTwo = new Rigidbody2D[levelTwoSpawnCount];
        levelThree = new Rigidbody2D[levelThreeSpawnCount];
        levelFour = new Rigidbody2D[levelFourSpawnCount];
        levelFive = new Rigidbody2D[levelFiveSpawnCount];

        FillLevel(levelOne, levelOneSpawnCount);
        FillLevel(levelTwo, levelTwoSpawnCount);
        FillLevel(levelThree, levelThreeSpawnCount);
        FillLevel(levelFour, levelFourSpawnCount);
        FillLevel(levelFive, levelFiveSpawnCount);
    }

    private void Start()
    {
        LevelChanged(levelState);
    }

    private void Update()
    {
        randomSpawnLocation = UnityEngine.Random.Range(-14f, 14f);
        runningTime += Time.deltaTime;
        if (runningTime > spawnFrequency)
        {
            canWeSpawn = true;
        }
        if (newLevel)
        {
            i = 0;
        }

        if(runningTime > 10f)
        {
            runningTime = 0f;
            levelState++;
            LevelChanged(levelState);
        }
        
    }

    private void FixedUpdate()
    {
        if (canWeSpawn && i < levelOneSpawnCount)
        {
            
            comet = Instantiate(levelOne[i], new Vector2(randomSpawnLocation, transform.position.y), Quaternion.identity);
            levelOne[i] = null;
            comet.gameObject.SetActive(true);
            comet.AddRelativeForce(Vector2.down * forceAmount, ForceMode2D.Impulse);
            canWeSpawn = false;
            i++;
            runningTime = 0f;
        }
    }

    private void FillLevel(Rigidbody2D[] level, int count)
    {
        for (int i = 0; i < count; i++)
        {
            level[i] = comet.GetComponent<Rigidbody2D>();
        }
    }


    public void LevelChanged(int level)
    {
        switch (level)
        {
            case 1:
                Debug.Log("test1");
                OnLevelSwitch?.Invoke((Int32)LevelState.One);
                break;
            case 2:
                OnLevelSwitch?.Invoke((Int32)LevelState.Two);
                break;
            case 3:
                OnLevelSwitch?.Invoke((Int32)LevelState.Three);
                break;
            case 4:
                OnLevelSwitch?.Invoke((Int32)LevelState.Four);
                break;
            case 5:
                OnLevelSwitch?.Invoke((Int32)LevelState.Five);
                break;
        }
    }


    public enum LevelState
    {
        Change,
        One,
        Two,
        Three,
        Four,
        Five,
        EndGame,
    }
}
