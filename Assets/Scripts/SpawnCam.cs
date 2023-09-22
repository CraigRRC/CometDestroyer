using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCam : MonoBehaviour
{
    public GameObject cam;

    private void Awake()
    {
        cam = Instantiate(cam, new Vector3(0, 0, -10f), Quaternion.identity);
    }
}
