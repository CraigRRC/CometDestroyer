using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;
    private PolygonCollider2D polygonCollider = null;
    private int respawnTimer = 0;
    private Vector2 playerSpawnPos;
    private Vector3 playerInitPos = new Vector3(0f, -7f, 0f);

    private void Awake()
    {
        playerSpawnPos = (Vector2)transform.position;
        polygonCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.magenta;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        { 
            gameObject.SetActive(false);
        }
    }

    public enum PlayerStates
    {
        Alive,
        Invul,
        Dead,
    }
}
