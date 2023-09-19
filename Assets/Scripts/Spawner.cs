using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Rigidbody2D comet;
    public float forceAmount = 4f;
    private float randomSpawnLocation;
    private float runningTime;
    public bool canWeSpawn = true;
    public float gravity = 0f;
    public float spawnFrequency = 1f;

    private void Update()
    {
        randomSpawnLocation = Random.Range(-14f, 14f);
        runningTime += Time.deltaTime;
        if (runningTime > spawnFrequency)
        {
            canWeSpawn = true;
        }
    }

    private void FixedUpdate()
    {
        if (canWeSpawn)
        {
            comet = Instantiate(comet, new Vector2(randomSpawnLocation, transform.position.y), Quaternion.identity);
            comet.gameObject.SetActive(true);
            comet.AddRelativeForce(Vector2.down * forceAmount, ForceMode2D.Impulse);
            comet.gravityScale = gravity;
            canWeSpawn = false;
            runningTime = 0f;
        }
        
    }
}
