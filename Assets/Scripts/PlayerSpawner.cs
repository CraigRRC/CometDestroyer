using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Player player;
    public float playerRespawnTimer = 1f;
    public float runningTimer = 0f;
    public Vector2 playerRespawnPos = Vector2.zero;
    public bool respawn = false;

    public void OnPlayerDeath(Vector2 location)
    {
        playerRespawnPos = location;
        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(playerRespawnTimer);
        player.gameObject.SetActive(true);
        player.transform.position = playerRespawnPos;
    }

}
