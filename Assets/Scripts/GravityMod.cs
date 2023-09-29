using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class GravityMod : MonoBehaviour
{
    public float gravity = 0f;

    private void Awake()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = gravity;
            rb.drag = gravity / 2f;
        }
    }
}
