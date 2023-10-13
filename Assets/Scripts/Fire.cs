using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private AudioSource pewAudioSource;
    public AudioClip[] pews = new AudioClip[3];
    public GameObject laser;
    public GameObject leftBlaster;
    public GameObject rightBlaster;
    public bool fireButtonPressed = false;
    public bool canFire = true;
    public float shootCooldown = 0f;
    public float reloadTime = 0.3f;
    public float minimumPewTimer = 0f;
    public bool isPewing = false;

    private void Awake()
    {
        pewAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
       
        fireButtonPressed = Input.GetButton("Jump");
        if (!canFire)
        {
           shootCooldown += Time.deltaTime;
        }
        if(shootCooldown > reloadTime)
        {
            canFire = true;
        }
        if (isPewing)
        {
            minimumPewTimer += Time.deltaTime;
            if (minimumPewTimer > 0.3f)
            {
                isPewing = false;
            }
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
        if(!isPewing)
        {
            int pewToPlay = Random.Range(0, pews.Length - 1);
            pewAudioSource.clip = pews[pewToPlay];
            pewAudioSource.Play();
            isPewing = true;
        }
        

        //Instantiate(projectile, leftBlaster.transform.position, Quaternion.identity);
        Instantiate(projectile, rightBlaster.transform.position, Quaternion.identity);
        shootCooldown = 0f;

    }
    
}
