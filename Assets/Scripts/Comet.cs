using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Comet : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Spawner spawner;
    public Rigidbody2D cometChunk;
    private float spawnOffset = 1f;
    private bool doOnce = true;
    public float explodeForce = 10f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Laser>())
        {
            collision.gameObject.SetActive(false);
            //spawn an explosion tho
            if (doOnce)
            {
                gameObject.SetActive(false);
                
                //spawn mini comet to the right
                Vector2 rightOffsetVector = new Vector2(transform.position.x + spawnOffset, transform.position.y - spawnOffset);
                Rigidbody2D rightComet = Instantiate(cometChunk, rightOffsetVector, Quaternion.identity);
                rightComet.AddForce(new Vector2(Random.Range(0.1f, 1f), -1) * explodeForce, ForceMode2D.Impulse);
                //Debug.DrawRay(rightComet.transform.position, rightOffsetVector.normalized * spawner.forceAmount, Color.yellow);

                //spawn mini comet to the left
                Vector2 leftOffsetVector = new Vector2(transform.position.x - spawnOffset, transform.position.y - spawnOffset);
                Rigidbody2D leftComet = Instantiate(cometChunk, leftOffsetVector, Quaternion.identity);
                leftComet.AddForce(new Vector2(Random.Range(-0.1f, -1f), -1) * explodeForce, ForceMode2D.Impulse);

                doOnce = false;
            }





        }
    }

}
