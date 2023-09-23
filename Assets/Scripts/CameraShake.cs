using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    public Spawner spawner;
    private Comet cometSpawned;
    public Vector3 initPos = Vector3.zero;
    public Vector3[] cameraShakePositions = new Vector3[5];
    public float minShakeRange = 0.1f;
    public float maxShakeRange = 0.2f;
    public float cameraZ = -10f;

    private void Awake()
    {
        initPos = new Vector3(0f, 0f, cameraZ);
        cam = GetComponent<Camera>();
        cam.transform.position = initPos;
    }
    void Start()
    {
        spawner.CometReference += OnCometReference;
    }

    private void OnDisable()
    {
        spawner.CometReference -= OnCometReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shake()
    {
        Debug.Log("are we shaking?");
    }

    public void OnCometReference(Comet comet)
    {
        comet.CameraShake += Shake;
    }


}
