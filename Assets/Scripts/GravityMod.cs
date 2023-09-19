using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMod : MonoBehaviour
{
    public float gravity = 0.5f;

    private void Awake()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        rb.drag = gravity / 2f;
    }
}
