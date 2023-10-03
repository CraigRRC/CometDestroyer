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
    public Vector2 cameraTargetPos = Vector3.zero;
    public Vector2 distToTarget = Vector2.zero;
    public Vector2 distToOrigin = Vector2.zero;
    public float cameraZ = -10f;
    public CameraStates cameraState;
    public float shakeSpeed = 4.2f;
    public float distToTargetMag;
    public float distToOriginMag;
    public bool doOnce = true;
    public float shakeRadius = 0.4f;

    private void Awake()
    {
        initPos = new Vector3(0f, 0f, cameraZ);
        cam = GetComponent<Camera>();
        cam.transform.position = initPos;
        cameraState = CameraStates.NeedsTarget;
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
        ShakeCamera();
    }

    private void ShakeCamera()
    {
        if (cameraState == CameraStates.NeedsTarget)
        {
            //Find target
            cameraTargetPos = Random.insideUnitCircle.normalized * shakeRadius;
            cameraState = CameraStates.HasTarget;

        }

        if (doOnce)
        {
            //Shake cam
            if (cameraState == CameraStates.MovingtoTarget)
            {
                distToTarget = cameraTargetPos - (Vector2)transform.position;
                distToTargetMag = distToTarget.magnitude;
                transform.Translate(distToTarget.normalized * Time.deltaTime * shakeSpeed);

                if (distToTargetMag < 0.1f)
                {
                    cameraState = CameraStates.ReturnToOrigin;
                }
            }

            if (cameraState == CameraStates.ReturnToOrigin)
            {
                distToOrigin = Vector2.zero - (Vector2)transform.position;
                distToOriginMag = distToOrigin.magnitude;
                transform.Translate(distToOrigin.normalized * Time.deltaTime * shakeSpeed);

                if (distToOriginMag < 0.1f)
                {
                    transform.position = initPos;
                    cameraState = CameraStates.NeedsTarget;
                    doOnce = false;
                }
            }
        }

        
    }

    public void Shake()
    {
        doOnce = true;
        cameraState = CameraStates.MovingtoTarget;
    }

    public void OnCometReference(Comet comet)
    {
        comet.CameraShake += Shake;
        comet.LeftCometReference += OnLeftCometReference;
        comet.RightCometReference += OnRightCometReference;
        
    }

    public void OnLeftCometReference(MiniComet leftcomet)
    {
        leftcomet.miniCometCameraShake += Shake;
    }

    public void OnRightCometReference(MiniComet rightComet)
    {
        rightComet.miniCometCameraShake += Shake;
    }



    public enum CameraStates
    {
        Origin,
        NeedsTarget,
        HasTarget,
        MovingtoTarget,
        ReturnToOrigin,
        MovingtoOffset,
    }

}
