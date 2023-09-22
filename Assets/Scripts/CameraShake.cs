using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera gameCamera;
    public Vector3 initPos = Vector3.zero;
    public Vector3[] cameraShakePositions = new Vector3[5];
    public float minShakeRange = 0.1f;
    public float maxShakeRange = 0.2f;
    public float cameraZ = -10f;

    private void Awake()
    {
        initPos = new Vector3(0f, 0f, cameraZ);
        
        
        
        for (int i = 0; i < cameraShakePositions.Length;  i++)
        {
            cameraShakePositions[i] = new Vector3(Random.Range(minShakeRange, maxShakeRange), Random.Range(minShakeRange, maxShakeRange), cameraZ);
        }
    }
    void Start()
    {
        // position x and y for more violent shakes.

       
      
        Instantiate<Camera>(gameCamera, initPos, Quaternion.identity);
           
       
        //gameCamera.transform.position = initPos;

        // rotation x and y for very gradual shakes.
    }

    // Update is called once per frame
    void Update()
    {
        
        //gameCamera.transform.position = initPos;
    }

    public void Shake()
    {
        Debug.Log("are we shaking?");
        for (int i = 0; i < cameraShakePositions.Length; ++i)
        {
            gameCamera.transform.Translate(cameraShakePositions[i] * Time.deltaTime);
        }
    }
}
