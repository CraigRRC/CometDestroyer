using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Rigidbody2D comet;
    public float forceAmount = 50f;
    private float randomSpawnLocation;
    public float spawnTimer;
    public bool canWeSpawn = true;
    public float gravity = 0f;

    private void Update()
    {
        randomSpawnLocation = Random.Range(-14f, 14f);
        spawnTimer += Time.deltaTime;
        if (spawnTimer > 3f)
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
            spawnTimer = 0f;
        }
        
    }
}
