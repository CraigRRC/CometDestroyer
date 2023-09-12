using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject laser;
    public GameObject leftBlaster;
    public GameObject rightBlaster;
    public bool mouseButtonPressed = false;
    public bool canFire = true;
    public float shootCooldown = 0f;

    private void Awake()
    {
        
    }

    private void Update()
    {
       
        mouseButtonPressed = Input.GetMouseButton(0);
        if (!canFire)
        {
            shootCooldown += Time.deltaTime;
        }
        if(shootCooldown > 0.5f)
        {
            canFire = true;
        }
    }

    private void FixedUpdate()
    {   
        if (mouseButtonPressed && canFire)
        {
            FireProjectile(laser);
            canFire = false;
        }
    }

    private void FireProjectile(GameObject projectile)
    {
        Instantiate(projectile, leftBlaster.transform.position, Quaternion.identity);
        Instantiate(projectile, rightBlaster.transform.position, Quaternion.identity);
        shootCooldown = 0f;

    }
    
}
