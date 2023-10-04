using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniComet : Comet
{

    public delegate void MiniCometCameraShakeHandler();
    public event MiniCometCameraShakeHandler miniCometCameraShake;

    public bool miniDoOnce = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Laser>() || collision.gameObject.layer == 7)
        {
            if (collision.gameObject.GetComponent<Laser>())
            {
                collision.gameObject.SetActive(false);
            }

            if (miniDoOnce)
            {
                miniCometCameraShake?.Invoke();
                miniDoOnce = false;
                gameObject.SetActive(false);
                
            } 
        }
    }
}
