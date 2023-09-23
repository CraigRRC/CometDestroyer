using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniComet : Comet
{
    private SpriteRenderer miniSpriteRenderer;

    public delegate void MiniCometCameraShakeHandler();
    public event MiniCometCameraShakeHandler miniCometCameraShake;

    private void Start()
    {
        miniSpriteRenderer = GetComponent<SpriteRenderer>();
        miniSpriteRenderer.color = spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Laser>())
        {
            //shake
            miniCometCameraShake?.Invoke();
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
