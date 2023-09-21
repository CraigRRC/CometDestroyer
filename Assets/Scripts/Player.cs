using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class OnPlayerDeathEvent : UnityEvent<Vector2> { }
public class Player : MonoBehaviour
{
    public OnPlayerDeathEvent m_OnPlayerDeathEvent;
    private SpriteRenderer spriteRenderer = null;
    private PolygonCollider2D polygonCollider = null;
    private float playerVulnerabilityTime = 2f;
    private Vector2 playerSpawnPos;
    private Vector3 playerInitPos = new Vector3(0f, -7f, 0f);
    public PlayerStates playerStates;
    public float invulTimer = 0f;
    private bool playersFirstLife = true;

   

    private void Awake()
    {
        playerStates = PlayerStates.Alive;
        playerSpawnPos = (Vector2)transform.position;
        polygonCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.magenta;
    }

    private void Start()
    {
        
        if (m_OnPlayerDeathEvent == null)
        {
            m_OnPlayerDeathEvent = new OnPlayerDeathEvent();
        }
    }

    private void OnEnable()
    {
        if (!playersFirstLife)
        {
            playerStates = PlayerStates.Invul;
        }
       
    }

    private void Update()
    {

        if(playerStates == PlayerStates.Invul)
        {
            polygonCollider.enabled = false;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
            StartCoroutine(PlayerVulnerable());
            
           
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerStates == PlayerStates.Alive && collision.gameObject.layer == 3)
        {
            m_OnPlayerDeathEvent.Invoke(playerSpawnPos);
            Debug.Log("did we invoke?");
            playerStates = PlayerStates.Dead;
            playersFirstLife = false;
            gameObject.SetActive(false);
        }
    }

    public void SetPlayerInvul()
    {
        playerStates = PlayerStates.Invul;
    }

    public enum PlayerStates
    {
        Alive,
        Invul,
        Dead,
    }

    private IEnumerator PlayerVulnerable()
    {
        yield return new WaitForSeconds(playerVulnerabilityTime);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        playerStates = PlayerStates.Alive;
        polygonCollider.enabled = true;
    }

    public bool IsDead() { return playerStates == PlayerStates.Dead;}
    public void SetPlayerState(PlayerStates state) { playerStates = state; }
    public Vector2 GetPlayerSpawnLocation() {  return playerSpawnPos; }
}
