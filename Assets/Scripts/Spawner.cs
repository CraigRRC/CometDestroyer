using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Comet comet;
    public float minForce = 4f;
    public float maxForce = 6f;
    public float forceAmount;
    private float randomSpawnLocation;
    public float runningTime;
    public bool preLevelSwitch = true;
    public bool canWeSpawn = true;
    public float spawnFrequency = 1f;
    public int levelOneSpawnCount = 10;
    public int levelTwoSpawnCount = 10;
    public int levelThreeSpawnCount = 10;
    public int levelFourSpawnCount = 10;
    public int levelFiveSpawnCount = 10;
    public int levelState;
    public Comet[] levelOne;
    public Comet[] levelTwo;
    public Comet[] levelThree;
    public Comet[] levelFour;
    public Comet[] levelFive;
    public int cometsOnScreen = 0;
    public int i = 0;
    public float minOffset = -5f;
    public float maxOffset = 5f;
    public float minSpawnDistance = -14f;
    public float maxSpawnDistance = 14f;
    public float levelSwitchTime = 4f;
    public float preLevelSwitchTime = 1f;
    public bool doOnce = true;
    public float levelSwitchTimer = 0f;


    public delegate void LevelSwitchEventHandler(int level);
    public event LevelSwitchEventHandler OnLevelSwitch;

    public delegate void CometReferenceHandler(Comet comet);
    public event CometReferenceHandler CometReference;

    public delegate void PreLevelSwitchHandler();
    public event PreLevelSwitchHandler OnPreLevelSwitch;

    

    private void Awake()
    {
        levelState = (Int32)LevelState.One;

        levelOne = new Comet[levelOneSpawnCount];
        levelTwo = new Comet[levelTwoSpawnCount];
        levelThree = new Comet[levelThreeSpawnCount];
        levelFour = new Comet[levelFourSpawnCount];
        levelFive = new Comet[levelFiveSpawnCount];

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
        forceAmount = UnityEngine.Random.Range(minForce,maxForce);

        randomSpawnLocation = UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
        runningTime += Time.deltaTime;

        if(cometsOnScreen <= 0)
        {
            levelSwitchTimer += Time.deltaTime;
        }

        if (runningTime > spawnFrequency)
        {
            canWeSpawn = true;
        }

        if(levelSwitchTimer > preLevelSwitchTime)
        {
            if (preLevelSwitch)
            {
                preLevelSwitch = false;
                OnPreLevelSwitch?.Invoke();
            }
        }

        if(levelSwitchTimer > levelSwitchTime)
        {
            runningTime = 0f;
            levelSwitchTimer = 0f;
            preLevelSwitch = true;
            levelState++;
            doOnce = true;
            LevelChanged(levelState);
            i = 0;
        }

        if (doOnce)
        {
            switch (levelState)
            {
                case 0:
                    cometsOnScreen = levelOneSpawnCount * 3;
                    doOnce = false;
                    break;
                case 1:
                    cometsOnScreen = levelTwoSpawnCount * 3;
                    doOnce = false;
                    break;
                case 2:
                    cometsOnScreen = levelThreeSpawnCount * 3;
                    doOnce = false;
                    break;
                case 3:
                    cometsOnScreen = levelFourSpawnCount * 3;
                    doOnce = false;
                    break;
                case 4:
                    cometsOnScreen = levelFiveSpawnCount * 3;
                    doOnce = false;
                    break;
             
                   
            }
        }
        
    }

    private void FixedUpdate()
    {
        switch (levelState)
        {
            case 0:
                SpawnComets(canWeSpawn, levelOne);
                break;
            case 1:
                SpawnComets(canWeSpawn, levelTwo);
                break;
            case 2:
                SpawnComets(canWeSpawn, levelThree);
               
                break;
            case 3:
                SpawnComets(canWeSpawn, levelFour);
                
                break;
            case 4:
                SpawnComets(canWeSpawn, levelFive);
                
                break;
        }
        
    }

    private void SpawnComets(bool spawn, Comet[]arrayToSpawn)
    {
        if (spawn && i < arrayToSpawn.Length)
        {

            comet = Instantiate(arrayToSpawn[i], new Vector2(randomSpawnLocation, transform.position.y), Quaternion.identity);
            //grab a reference to this comet.
            CometReference?.Invoke(comet);
            arrayToSpawn[i] = null;
            comet.gameObject.SetActive(true);
            comet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * forceAmount, ForceMode2D.Impulse);
            canWeSpawn = false;
            i++;
            runningTime = 0f;
        }
    }

    private void FillLevel(Comet[]level, int count)
    {
        for (int i = 0; i < count; i++)
        {
            level[i] = comet;
        }
    }


    public void LevelChanged(int level)
    {
        switch (level)
        {
            case 0:
                OnLevelSwitch?.Invoke((Int32)LevelState.One);
                break;
            case 1:
                OnLevelSwitch?.Invoke((Int32)LevelState.Two);
                break;
            case 2:
                OnLevelSwitch?.Invoke((Int32)LevelState.Three);
                break;
            case 3:
                OnLevelSwitch?.Invoke((Int32)LevelState.Four);
                break;
            case 4:
                OnLevelSwitch?.Invoke((Int32)LevelState.Five);
                break;
        }
    }


    public enum LevelState
    {
        One,
        Two,
        Three,
        Four,
        Five,
        EndGame,
    }
}
