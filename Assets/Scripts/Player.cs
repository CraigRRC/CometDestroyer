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
    public CircleCollider2D shieldCollider = null;
    public SpriteRenderer shieldArt = null;
    
    private float playerVulnerabilityTime = 3f;
    private Vector2 playerSpawnPos;
    public PlayerStates playerStates;
    public float invulTimer = 0f;
    private bool playersFirstLife = true;

    public float availiableShield = 2f;
    public bool canShield = true;
    public bool playerHoldingShift;

    

   

    private void Awake()
    {
        playerStates = PlayerStates.Alive;
        playerSpawnPos = (Vector2)transform.position;
        polygonCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shieldCollider.enabled = false;
        shieldArt.enabled = false;


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
        if(canShield)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {

                //check to see if we have any shield to use
                playerStates = PlayerStates.Shielded;


            }
        }
       
        if (!canShield && playerStates != PlayerStates.Invul)
        {
            playerStates = PlayerStates.Alive;
            shieldArt.enabled = false;
            shieldCollider.enabled = false;
            polygonCollider.enabled = true;
        }
        
        if(availiableShield < 2f)
        {
            availiableShield += Time.deltaTime / 10f;
        }
        if(availiableShield > 0.5f && playerStates != PlayerStates.Invul)
        {
            canShield = true;
        }
            
        
        //make into switch case.
        if(playerStates == PlayerStates.Invul)
        {
            invulTimer += Time.deltaTime;
            shieldArt.enabled = true;
            shieldCollider.enabled = true;
            polygonCollider.enabled = false;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
            if(invulTimer > playerVulnerabilityTime)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                shieldArt.enabled = false;
                shieldCollider.enabled = false;
                polygonCollider.enabled = true;
                playerStates = PlayerStates.Alive;
                invulTimer = 0f;
                
            }

            //StartCoroutine(PlayerVulnerable());
            
        }
        else if(playerStates == PlayerStates.Shielded)
        {
            availiableShield -= Time.deltaTime;
            if(availiableShield < 0.1f)
            {
                playerHoldingShift = false;
                canShield = false;
            }
            // availiable shield descreases by time.deltatime
            shieldArt.enabled = true;
            shieldCollider.enabled = true;
            polygonCollider.enabled = false;
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
        Shielded,
        Dead,
    }

    private IEnumerator PlayerVulnerable()
    {
        yield return new WaitForSeconds(playerVulnerabilityTime);
        
    }

    public bool IsDead() { return playerStates == PlayerStates.Dead;}
    public void SetPlayerState(PlayerStates state) { playerStates = state; }
    public Vector2 GetPlayerSpawnLocation() {  return playerSpawnPos; }
}
