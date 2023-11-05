using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    enum Mode {
        Aim,
        Launch
    }
    [SerializeField] private float PanSpeed = 0.5f;
    [SerializeField] AnimationCurve zoomSpeedCurve;

    [SerializeField] private float defaultHeight = 20.0f;
    [SerializeField] private float MaxCameraHeight = 40.0f;
    [SerializeField] private float MinCameraHeight = 5.0f;

    private Camera camComp;
    private Transform cameraStart;
    private float ZoomSpeed = 2.0f;

    Mode mode = Mode.Aim;

    private void Awake() {
        camComp = GetComponent<Camera>();
    }

    void Start()
    {
        cameraStart = transform;
        if (!camComp.orthographic)
            Debug.LogWarning("TopDownCamera is not attached to an orthographic camera");
    }

    void Update()
    {
        if (mode == Mode.Aim)
            ProcessAimInput();
        else
            ProcessLaunchInput();
    }

    void ProcessAimInput()
    {
        //Translate Main Camera x/z
        float xTranslate = 0.0f;
        float zTranslate = 0.0f;
        if (Input.GetKey(KeyCode.W))
            zTranslate += PanSpeed;
        if (Input.GetKey(KeyCode.S))
            zTranslate -= PanSpeed;
        if (Input.GetKey(KeyCode.D))
            xTranslate += PanSpeed;
        if (Input.GetKey(KeyCode.A))
            xTranslate -= PanSpeed;
        transform.Translate(xTranslate, 0, zTranslate, Space.World);

        //Reset Main Camera Position
        if (Input.GetKey(KeyCode.R))
            ResetCamera();

        //Change Main Camera Height (y)
        float zoomProg = (camComp.orthographicSize - MinCameraHeight) / MaxCameraHeight;
        ZoomSpeed = zoomSpeedCurve.Evaluate(zoomProg);

        float zoom = -Input.mouseScrollDelta.y * ZoomSpeed;
        bool canMoveCameraUp = (camComp.orthographicSize + zoom) < MaxCameraHeight;
        bool canMoveCameraDown = (camComp.orthographicSize + zoom) > MinCameraHeight;
        if (canMoveCameraUp && canMoveCameraDown)
        {
            camComp.orthographicSize += zoom;
        }
    }

    void ProcessLaunchInput()
    {

    }

    private Vector3 IsometricMove()
    {
        return Vector3.one;
    }

    void ResetCamera()
    {
        camComp.orthographicSize = defaultHeight;
        transform.position = cameraStart.position;
    }
}
