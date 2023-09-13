using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
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
}
