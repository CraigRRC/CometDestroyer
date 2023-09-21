using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class PlayerSpawner : MonoBehaviour
{
    public Player player;
    public float playerRespawnTimer = 1f;
    public float runningTimer = 0f;
    public Vector2 playerRespawnPos = Vector2.zero;
    public bool respawn = false;

    private void Awake()
    {
   
    }

    private void Start()
    {
       
    }

    private void OnDestroy()
    {
       
    }

    private void Update()
    {
        
    }

    public void OnPlayerDeath(Vector2 location)
    {
        Debug.Log("respawning?");
        playerRespawnPos = location;
        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(playerRespawnTimer);
        Debug.Log("hello?");
        player.gameObject.SetActive(true);
        player.transform.position = playerRespawnPos;
    }

}
