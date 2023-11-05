using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TopDownCamera : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private float distance;
    [SerializeField] private float pitch;
    [SerializeField] private float yaw;
    [SerializeField] private GameObject player;

    [Header("Camera Movement")]
    [SerializeField] AnimationCurve panSpeedByDistance;
    [SerializeField] AnimationCurve zoomSpeedCurve;

    [SerializeField] private float defaultHeight = 20.0f;
    [SerializeField] private float MaxCameraHeight = 40.0f;
    [SerializeField] private float MinCameraHeight = 5.0f;

    private Camera camComp;
    private Transform cameraStart;
    private float zoomSpeed = 2.0f;
    private float panSpeed = 0.2f;

    private void Awake() {
        camComp = GetComponent<Camera>();
    }

    void Start()
    {
        if (!camComp.orthographic)
            Debug.LogWarning("TopDownCamera is not attached to an orthographic camera");
    }
    private void OnEnable()
    {
        CenterCamera();
        ResetCamera();
    }

    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        // Scroll to zoom
        float zoomProg = (camComp.orthographicSize - MinCameraHeight) / MaxCameraHeight;
        zoomSpeed = zoomSpeedCurve.Evaluate(zoomProg);

        float zoom = -Input.mouseScrollDelta.y * zoomSpeed;
        bool canMoveCameraUp = (camComp.orthographicSize + zoom) < MaxCameraHeight;
        bool canMoveCameraDown = (camComp.orthographicSize + zoom) > MinCameraHeight;
        if (canMoveCameraUp && canMoveCameraDown)
        {
            camComp.orthographicSize += zoom;
        }

        //Translate Main Camera x/z
        panSpeed = panSpeedByDistance.Evaluate(zoomProg);
        Vector3 dir = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            dir += ToIsometric(Vector3.up);
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            dir += ToIsometric(Vector3.down);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            dir += ToIsometric(Vector3.right);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            dir += ToIsometric(Vector3.left);

        transform.Translate(dir * panSpeed, Space.World);

        //Reset Main Camera Position
        if (Input.GetKey(KeyCode.R))
            ResetCamera();
    }

    private Vector3 ToIsometric(Vector3 dir)
    {
        if (dir == Vector3.up)
            return new Vector3(1f, 0f, 1f);
        if (dir == Vector3.down)
            return new Vector3(-1f, 0f, -1f);
        if (dir == Vector3.right)
            return new Vector3(1f, 0f, -1f);
        if (dir == Vector3.left)
            return new Vector3(-1f, 0f, 1f);
        return Vector3.zero;
    }

    void ResetCamera()
    {
        camComp.orthographicSize = defaultHeight;
        transform.position = cameraStart.position;
    }

    private void CenterCamera()
    {
        //Calculate how far away to be in each dimension
        Vector3 offset = Vector3.back * distance;
        Matrix4x4 pitchTransform = Matrix4x4.Rotate(Quaternion.AngleAxis(pitch, Vector3.right));
		offset = pitchTransform.MultiplyPoint(offset);
		Matrix4x4 yawTransform = Matrix4x4.Rotate(Quaternion.AngleAxis(yaw, Vector3.up));
		offset = yawTransform.MultiplyPoint(offset);

        //Move and rotate
        Vector3 goalPos = player.gameObject.transform.position + offset;
		transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
		transform.position = goalPos;

        cameraStart = transform;
    }
}
