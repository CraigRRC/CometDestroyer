using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGravityMod : MonoBehaviour
{
    public float gravity = 0f;
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (ps != null)
        {
            if(ps.gravityModifier != gravity)
            {
                ps.gravityModifier = gravity;
            }
        }
    }
}
