using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 uncleanPlayerInput;
    private Vector2 playerInputDirection;
    private float cleanedMagnitude;
    public float moveSpeed = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        CleanPlayerInput();
        //rb.velocity = uncleanPlayerInput.normalized * cleanedMagnitude * moveSpeed;
        rb.AddForce(uncleanPlayerInput.normalized * cleanedMagnitude * moveSpeed, ForceMode2D.Force);
    }

    private void CleanPlayerInput()
    {
        uncleanPlayerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerInputDirection = uncleanPlayerInput.normalized;
        cleanedMagnitude = Mathf.Min(uncleanPlayerInput.magnitude, 1f);
    }
}
