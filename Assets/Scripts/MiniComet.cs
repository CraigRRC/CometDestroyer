using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniComet : Comet
{

    public delegate void MiniCometCameraShakeHandler();
    public event MiniCometCameraShakeHandler miniCometCameraShake;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Laser>())
        {
            //shake
            miniCometCameraShake?.Invoke();
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.layer == 7)
        {
            miniCometCameraShake?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
