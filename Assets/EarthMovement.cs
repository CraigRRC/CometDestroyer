using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMovement : MonoBehaviour
{

    public Spawner spawner;
    private SpriteRenderer spriteRenderer;
    public float radius = 0.2f;
    public Vector2 TargetVector = Vector2.zero;
    public float shakeSpeed = 2f;
    public float earthTravelSpeed = 0.5f;
    public bool shouldShake = false;
    public float shakeDuration = 1.5f;
    public float runningTime = 0f;
    public Vector2 shakeOrigin;
    public bool acquireNewTarget;
    public Vector2 shakeTarget;
    public bool goToOrigin;
    public bool goToTarget;
    public float shakeHeight = 0.5f;
    public Vector2 distToTarget;
    public bool stopMoving = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spawner.OnPreLevelSwitch += MoveEarthOnLevelSwitch;
    }

    private void MoveEarthOnLevelSwitch()
    {
        spriteRenderer.enabled = true;
        acquireNewTarget = true;
        runningTime = 0f;
        shakeOrigin = (Vector2)transform.position;
        goToTarget = true;
        stopMoving = false;
    }

    void Update()
    {
        if (acquireNewTarget)
        {
            shakeTarget = new Vector2(0f, transform.position.y + shakeHeight);
            acquireNewTarget = false;
        }

        if (runningTime < shakeDuration)
        {
           
            runningTime += Time.deltaTime;
            if(goToTarget) 
            {
                //get direction that we need to go towards
                distToTarget = shakeTarget - (Vector2)transform.position;
                //Debug.Log(distToTarget.magnitude);

                //move towards that direction
                transform.Translate(distToTarget.normalized * shakeSpeed * Time.deltaTime);
                if (distToTarget.magnitude < 0.1f)
                {
                    goToOrigin = true;
                    goToTarget = false;

                }
            } 
            else if (goToOrigin)
            {
                Vector2 distToOrigin = shakeOrigin - (Vector2)transform.position;
                transform.Translate(distToOrigin.normalized * shakeSpeed * Time.deltaTime);
                if (distToOrigin.magnitude < 0.1f)
                {
                    //back at origin
                    goToOrigin = false;
                    goToTarget = true;
                }
            }
        }
        else
        {
            if(!stopMoving)
            {
                //move the earth up one time.
                transform.Translate(distToTarget.normalized * earthTravelSpeed * Time.deltaTime);
                if (distToTarget.magnitude < 0.2f)
                {
                    stopMoving = true;
                }
            }
            
        }
        
    }

    private void OnDisable()
    {
        spawner.OnPreLevelSwitch -= MoveEarthOnLevelSwitch;
    }



}
