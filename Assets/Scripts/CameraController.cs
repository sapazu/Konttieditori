using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float RotationSpeed = 45;

    public Vector3 camRotation;

    Transform camPivot;

    // Start is called before the first frame update
    void Start()
    {
        SetEssentials();   
    }

    void SetEssentials()
    {
        camPivot = GameObject.Find("CameraPivot").transform;
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraRotation();
    }

    void SetCameraRotation()
    {
        camRotation.x += Input.GetAxisRaw("Vertical") * RotationSpeed * Time.deltaTime;

        camPivot.eulerAngles = camRotation;
    }
}
