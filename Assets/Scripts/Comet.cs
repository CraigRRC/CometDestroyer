using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Comet : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private Rigidbody2D comet;
    public Rigidbody2D cometChunk;
    private float spawnOffset = 1f;
    private bool doOnce = true;
    public UnityEvent cameraShake;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        comet = GetComponent<Rigidbody2D>();
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
                cameraShake.Invoke();
                //spawn mini comet to the right
                Vector2 rightOffsetVector = new Vector2(transform.position.x + spawnOffset, transform.position.y - spawnOffset);
                Rigidbody2D rightComet = Instantiate(cometChunk, rightOffsetVector, Quaternion.identity);
                rightComet.AddForce(new Vector2(Random.Range(0.1f, 1f), -1) * comet.velocity.magnitude, ForceMode2D.Impulse);
                //Debug.DrawRay(rightComet.transform.position, rightOffsetVector.normalized * spawner.forceAmount, Color.yellow);

                //spawn mini comet to the left
                Vector2 leftOffsetVector = new Vector2(transform.position.x - spawnOffset, transform.position.y - spawnOffset);
                Rigidbody2D leftComet = Instantiate(cometChunk, leftOffsetVector, Quaternion.identity);
                leftComet.AddForce(new Vector2(Random.Range(-0.1f, -1f), -1) * comet.velocity.magnitude, ForceMode2D.Impulse);
                gameObject.SetActive(false);
                doOnce = false;
            }
        }
    }
}
