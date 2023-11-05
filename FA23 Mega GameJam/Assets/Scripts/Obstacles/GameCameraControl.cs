using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraControl : MonoBehaviour
{
    [SerializeField] private float PanSpeed = 0.05f;
    [SerializeField] private float ZoomSpeed = 2.0f;

    [SerializeField] private float MaxCameraHeight = 18.0f;
    [SerializeField] private float MinCameraHeight = 5.0f;

    [SerializeField] private Transform CameraStart;
    [SerializeField] private Transform CameraFinish;
    [SerializeField] private  GameObject CurrentLaunchPlanet;
    [SerializeField] private Transform CameraRocketPosition;

    [SerializeField] private float LaunchRotateAmount = -15.0f;


    private Transform camTransform;
    private Vector3 originalPosition;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.7f;
    private float dampingSpeed = 1.0f;



    int mode = 0;

    // Start is called before the first frame update
    void Start()
    {
        ResetCamera();
    }

    void Update()
    {
        if (mode == 0) ProcessAimInput();
        else if (mode == 1) ProcessLaunchInput();
        else ActivateFinishCamera();

        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPosition;
        }
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
        Camera.main.transform.Translate(xTranslate, 0, zTranslate, Space.World);

        //Reset Main Camera Position
        if (Input.GetKey(KeyCode.R)) ResetCamera();

        //Change Main Camera Height (y)
        float zoom = -Input.mouseScrollDelta.y * ZoomSpeed;
        bool canMoveCameraUp = (Camera.main.transform.position.y + zoom) < MaxCameraHeight;
        bool canMoveCameraDown = (Camera.main.transform.position.y + zoom) > MinCameraHeight;
        if (canMoveCameraUp && canMoveCameraDown) Camera.main.transform.Translate(0.0f, zoom, 0.0f, Space.World);
    }

    public void ShakeCamera(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    public void ActivateFinishCamera()
    {
        mode = 2;
        transform.position = CameraFinish.position;
    }

    public void ActivateLaunchCamera()
    {
        mode = 1;
        transform.Rotate(LaunchRotateAmount, 0, 0);
    }

    void ProcessLaunchInput()
    {
        transform.position = CameraRocketPosition.position;
    }

    void ResetCamera()
    {
        transform.position = CameraStart.position;
    }

    

}
