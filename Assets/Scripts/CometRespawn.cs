using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometRespawn : MonoBehaviour
{
    public float minX = -14f;
    public float maxX = 14f;
    public void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.position = new Vector3(Random.Range(minX, maxX), 10f, collision.transform.position.z);
    }
}
