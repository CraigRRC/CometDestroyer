using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject laser;
    public GameObject leftBlaster;
    public GameObject rightBlaster;
    public bool fireButtonPressed = false;
    public bool canFire = true;
    public float shootCooldown = 0f;

    private void Awake()
    {
        
    }

    private void Update()
    {
       
        fireButtonPressed = Input.GetButton("Jump");
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
        if (fireButtonPressed && canFire)
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
