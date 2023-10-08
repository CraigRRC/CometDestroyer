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
    public OnPlayerDeathEvent OnPlayerDeathEvent;
    public delegate void ShieldUseHandler(float shieldAmount);
    public event ShieldUseHandler OnShieldUse;
    public delegate void ShieldColourHandler(bool canShield);
    public event ShieldColourHandler OnShieldColour;


    private SpriteRenderer spriteRenderer = null;
    private PolygonCollider2D polygonCollider = null;
    public CircleCollider2D shieldCollider = null;
    public SpriteRenderer shieldArt = null;
    
    public float playerVulnerabilityTime = 1.5f;
    private Vector2 playerSpawnPos;
    public PlayerStates playerStates;
    public float invulTimer = 0f;
    private bool playersFirstLife = true;

    public float availiableShield = 2f;
    public float maxShield = 2f;
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
        availiableShield = maxShield;
    }

    private void Start()
    {
        
        if (OnPlayerDeathEvent == null)
        {
            OnPlayerDeathEvent = new OnPlayerDeathEvent();
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
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.P))
            {

                //check to see if we have any shield to use
                playerStates = PlayerStates.Shielded;


            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.P))
            {
                DropShield();
            }
        }
       
        if (!canShield && playerStates != PlayerStates.Invul)
        {
            DropShield();
        }
        
        if(availiableShield < 2f)
        {
            availiableShield += Time.deltaTime / 10f;
            OnShieldUse?.Invoke(availiableShield);
        }
        if(availiableShield > 0.5f && playerStates != PlayerStates.Invul)
        {
            canShield = true;
            OnShieldColour?.Invoke(canShield);
        }
            
        
        //make into switch case.
        if(playerStates == PlayerStates.Invul)
        {
            canShield = false;
            OnShieldColour?.Invoke(canShield);
            invulTimer += Time.deltaTime;
            EnableShield();
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
            if(invulTimer > playerVulnerabilityTime)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                DropShield();
                invulTimer = 0f;
                
            }

            
        }
        else if(playerStates == PlayerStates.Shielded)
        {
            availiableShield -= Time.deltaTime;
            //OnShieldEvent.Invoke(availiableShield);
            OnShieldUse?.Invoke(availiableShield);
            if (availiableShield < 0.1f)
            {
                canShield = false;
                OnShieldColour?.Invoke(canShield);
            }
            // availiable shield descreases by time.deltatime
            EnableShield();
        }
    }

    private void EnableShield()
    {
        shieldArt.enabled = true;
        shieldCollider.enabled = true;
        polygonCollider.enabled = false;
    }

    private void DropShield()
    {
        playerStates = PlayerStates.Alive;
        shieldArt.enabled = false;
        shieldCollider.enabled = false;
        polygonCollider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerStates == PlayerStates.Alive && collision.gameObject.layer == 3)
        {
            OnPlayerDeathEvent.Invoke(playerSpawnPos);
            playerStates = PlayerStates.Dead;
            playersFirstLife = false;
            gameObject.SetActive(false);
        }
    }

    public enum PlayerStates
    {
        Alive,
        Invul,
        Shielded,
        Dead,
    }

    public bool IsDead() { return playerStates == PlayerStates.Dead;}
    public void SetPlayerState(PlayerStates state) { playerStates = state; }
    public Vector2 GetPlayerSpawnLocation() {  return playerSpawnPos; }
    public float GetPlayerShieldMax() { return maxShield; }
}
