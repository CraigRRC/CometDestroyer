using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniComet : Comet
{
    private SpriteRenderer miniSpriteRenderer;
    private void Start()
    {
        miniSpriteRenderer = GetComponent<SpriteRenderer>();
        miniSpriteRenderer.color = spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Laser>())
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
