using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject laser;
    public GameObject blaster;
    public bool mouseButtonPressed = false;
    public bool canFire = true;
    public float shootCooldown = 0f;

    private void Awake()
    {
        
    }

    private void Update()
    {
        shootCooldown += Time.deltaTime;
        mouseButtonPressed = Input.GetMouseButton(0);
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
        Instantiate(projectile, blaster.transform.position, Quaternion.identity);
        
    }
    
}
