using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float RotationSpeed = 45;

    bool isTouchingScreen;

    public Vector3 camRotation;
    public Vector3 oldCamrotation;

    public Transform camPivot;
    public Transform camTr;

    Touch firstDetectedTouch;

    public Camera cam;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        SetEssentials();   
    }

    void SetEssentials()
    {
        gameManager = GetComponent<GameManager>();
        camPivot = GameObject.Find("CameraPivot").transform;
        camTr = Camera.main.transform;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        GetTouch();
        SetCameraRotation();
    }

    void GetTouch()
    {
        if(Input.touchCount > 0)
        {
            firstDetectedTouch = Input.GetTouch(0);
        }
    }

    void SetCameraRotation()
    {
        camRotation.x += Input.GetAxisRaw("Vertical") * RotationSpeed * Time.deltaTime;
        camRotation.y += Input.GetAxisRaw("Horizontal") * RotationSpeed * -1 * Time.deltaTime;

        if (isTouchingScreen && !gameManager.showBlueprint)
        {
            camRotation.x += firstDetectedTouch.deltaPosition.y * RotationSpeed * -0.05f * Time.deltaTime;
            camRotation.y += firstDetectedTouch.deltaPosition.x * RotationSpeed * 0.05f * Time.deltaTime;
        }

        camRotation.x = Mathf.Clamp(camRotation.x, 0, 90);

        camPivot.eulerAngles = camRotation;
    }

    public void ReleaseCameraMovement(bool _canMove)
    {
        isTouchingScreen = _canMove;
    }
}
