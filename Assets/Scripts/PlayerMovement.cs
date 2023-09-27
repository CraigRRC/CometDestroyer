using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 uncleanPlayerInput;
    private Vector2 playerInputDirection;
    private float cleanedMagnitude;
    public float moveSpeed = 2.7f;
    private Rigidbody2D rb;
    public GameObject mainEngine;
    public GameObject rightEngine;
    public GameObject leftEngine;
    private SpriteRenderer[] mainEngineFlames;
    private SpriteRenderer[] rightEngineFlames;
    private SpriteRenderer[] leftEngineFlames;
    public int flameCounter = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        mainEngineFlames = mainEngine.GetComponentsInChildren<SpriteRenderer>();
        leftEngineFlames = leftEngine.GetComponentsInChildren<SpriteRenderer>();
        rightEngineFlames = rightEngine.GetComponentsInChildren<SpriteRenderer>();
        TurnOffAllFlames(mainEngineFlames);
        TurnOffAllFlames(leftEngineFlames);
        TurnOffAllFlames(rightEngineFlames);


    }
    private void Update()
    {

        CleanPlayerInput();
        PlayerThrusterArtController();
    }

    private void PlayerThrusterArtController()
    {
        //consider using a random range to add some variety? possibly.
        if (uncleanPlayerInput.y > 0.1f)
        {
            mainEngine.SetActive(true);
            mainEngineFlames[2].enabled = true;

            if (uncleanPlayerInput.y > 0.5f)
            {
                mainEngineFlames[2].enabled = false;
                mainEngineFlames[0].enabled = true;
                mainEngineFlames[1].enabled = true;
                if (uncleanPlayerInput.y >= 0.9f)
                {
                    mainEngineFlames[2].enabled = true;
                }
            }
        }
        else
        {
            mainEngine.SetActive(false);
            TurnOffAllFlames(mainEngineFlames);
        }

        if (uncleanPlayerInput.y < -0.1f)
        {
            rightEngine.SetActive(true);
            leftEngine.SetActive(true);
            rightEngineFlames[2].enabled = true;
            leftEngineFlames[2].enabled = true;
            if (uncleanPlayerInput.y < -0.8f)
            {
                rightEngineFlames[1].enabled = true;
                rightEngineFlames[0].enabled = true;
                leftEngineFlames[1].enabled = true;
                leftEngineFlames[0].enabled = true;
            }
        }
        else
        {
            rightEngine.SetActive(false);
            leftEngine.SetActive(false);
            TurnOffAllFlames(rightEngineFlames);
            TurnOffAllFlames(leftEngineFlames);
        }

        if (uncleanPlayerInput.x > 0.1f)
        {
            leftEngine.SetActive(true);
            leftEngineFlames[2].enabled = true;
            if (uncleanPlayerInput.x > 0.7f)
            {
                leftEngineFlames[0].enabled = true;
                leftEngineFlames[1].enabled = true;
            }
        }
        if (uncleanPlayerInput.x < -0.1f)
        {
            rightEngine.SetActive(true);
            rightEngineFlames[2].enabled = true;
            if (uncleanPlayerInput.x < -0.7f)
            {
                rightEngineFlames[0].enabled = true;
                rightEngineFlames[1].enabled = true;
            }
        }
    }

    private void FixedUpdate()
    {
        //rb.velocity = uncleanPlayerInput.normalized * cleanedMagnitude * moveSpeed;
        rb.AddForce(uncleanPlayerInput.normalized * cleanedMagnitude * moveSpeed, ForceMode2D.Force);
    }

    private void CleanPlayerInput()
    {
        uncleanPlayerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerInputDirection = uncleanPlayerInput.normalized;
        cleanedMagnitude = Mathf.Min(uncleanPlayerInput.magnitude, 1f);
    }

    private void TurnOffAllFlames(SpriteRenderer[] flameCollection)
    {
        foreach (var flame in flameCollection)
        {
            flame.enabled = false;
        }
    }
}
