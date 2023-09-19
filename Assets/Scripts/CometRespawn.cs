using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometRespawn : MonoBehaviour
{
    public void OnTriggerExit2D(Collider2D collision)
    { 
        collision.transform.position = new Vector3(collision.transform.position.x, 9f, collision.transform.position.z);
        Debug.Log(collision.name); 
    }
}
