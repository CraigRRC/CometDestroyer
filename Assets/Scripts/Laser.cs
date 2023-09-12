using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Rigidbody2D rb;
    public float firePower = 50f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0 , firePower);
        Destroy(this.gameObject, 5.0f);
    }
}
