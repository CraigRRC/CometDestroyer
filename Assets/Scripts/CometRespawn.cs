using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometRespawn : MonoBehaviour
{
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name != "RespawnProtection")
        {
            collision.transform.position = new Vector3(collision.transform.position.x * Random.Range(-0.8f, 0.8f), 10f, collision.transform.position.z);
        }
        
    }
}
