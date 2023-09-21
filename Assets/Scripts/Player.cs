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
    private Vector2 playerSpawnPos;
    private Vector3 playerInitPos = new Vector3(0f, -7f, 0f);
    private PlayerStates playerStates;

   

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
        playerStates = PlayerStates.Alive;
    }

    private void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerStates == PlayerStates.Alive && collision.gameObject.layer == 3)
        {
            m_OnPlayerDeathEvent.Invoke(playerSpawnPos);
            Debug.Log("did we invoke?");
            playerStates = PlayerStates.Dead;
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


    public bool IsDead() { return playerStates == PlayerStates.Dead;}
    public void SetPlayerState(PlayerStates state) { playerStates = state; }
    public Vector2 GetPlayerSpawnLocation() {  return playerSpawnPos; }
}
