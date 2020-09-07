using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float RotationSpeed = 45;

    bool isTouchingScreen;
    bool showBlueprint;

    public Vector3 camRotation;
    Vector3 oldCamrotation;

    GameObject kontti;

    Transform camPivot;
    Transform selectedWall;
    Transform oldWall;

    Touch firstDetectedTouch;

    public Text DebugText;

    Camera cam;

    RaycastHit wallHit;

    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        SetEssentials();   
    }

    void SetEssentials()
    {
        camPivot = GameObject.Find("CameraPivot").transform;
        cam = Camera.main;
        kontti = GameObject.FindGameObjectWithTag("Kontti");
    }

    // Update is called once per frame
    void Update()
    {
        GetTouch();
        SetCameraRotation();
        CastForWall();
    }

    void GetTouch()
    {
        if(Input.touchCount > 0)
        {
            firstDetectedTouch = Input.GetTouch(0);
        }

        DebugText.text = firstDetectedTouch.deltaPosition.ToString();
    }

    void SetCameraRotation()
    {
        camRotation.x += Input.GetAxisRaw("Vertical") * RotationSpeed * Time.deltaTime;
        camRotation.y += Input.GetAxisRaw("Horizontal") * RotationSpeed * -1 * Time.deltaTime;

        if (isTouchingScreen && !showBlueprint)
        {
            camRotation.x += firstDetectedTouch.deltaPosition.y * RotationSpeed * -0.05f * Time.deltaTime;
            camRotation.y += firstDetectedTouch.deltaPosition.x * RotationSpeed * 0.05f * Time.deltaTime;
        }

        camRotation.x = Mathf.Clamp(camRotation.x, 0, 90);

        camPivot.eulerAngles = camRotation;
    }

    void CastForWall()
    {
        if(Physics.Raycast(camPivot.position, camPivot.forward * -1, out wallHit, 10, mask))
        {
            oldWall = selectedWall;
            selectedWall = wallHit.transform;

            if(oldWall != selectedWall && oldWall != null)
            {
                oldWall.transform.GetComponent<MeshRenderer>().enabled = true;
                oldWall = selectedWall;
            }
            wallHit.transform.GetComponent<MeshRenderer>().enabled = false;
        }
        /*if (Physics.Raycast(Vector3.up, Vector3.forward, out wallHit, 10))
        {
            Debug.Log(wallHit.transform);
        }*/
    }

    public void ReleaseCameraMovement(bool _canMove)
    {
        isTouchingScreen = _canMove;
    }

    public void BlueprintButton()
    {
        showBlueprint = !showBlueprint;

        if (showBlueprint)
        {
            cam.fieldOfView = 30;
            oldCamrotation = camRotation;
            camRotation = Vector3.right * 90;
            kontti.SetActive(false);
        }
        else
        {
            cam.fieldOfView = 40;
            camRotation = oldCamrotation;
            kontti.SetActive(true);
        }
    }
}
