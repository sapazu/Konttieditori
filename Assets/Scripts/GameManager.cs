using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public bool showBlueprint;
    public bool showInside;

    CameraController camController;

    GameObject kontti;
    GameObject menu;

    RaycastHit wallHit;
    public LayerMask mask;

    Transform selectedWall;
    Transform oldWall;

    void Start()
    {
        SetEssentials();
    }

    void SetEssentials()
    {
        camController = GetComponent<CameraController>();
        menu = GameObject.Find("Menu");
        menu.SetActive(false);
        kontti = GameObject.FindGameObjectWithTag("Kontti");
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        CastForWall();
    }

    void CastForWall()
    {
        if (Physics.Raycast(camController.camPivot.position, camController.camPivot.forward * -1, out wallHit, 10, mask))
        {
            oldWall = selectedWall;
            selectedWall = wallHit.transform;

            if (oldWall != selectedWall && oldWall != null && showInside)
            {
                oldWall.transform.GetComponent<MeshRenderer>().enabled = true;
                oldWall = selectedWall;
            }
            wallHit.transform.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void BlueprintButton()
    {
        showBlueprint = !showBlueprint;

        if (showBlueprint)
        {
            camController.cam.fieldOfView = 30;
            camController.oldCamrotation = camController.camRotation;
            camController.camRotation = Vector3.right * 90;
            kontti.SetActive(false);
        }
        else
        {
            camController.cam.fieldOfView = 40;
            camController.camRotation = camController.oldCamrotation;
            kontti.SetActive(true);
        }
    }

    public void MenuButton()
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void ActivateObject(GameObject _object)
    {
        GameObject thisToggle = EventSystem.current.currentSelectedGameObject;
        bool active = thisToggle.GetComponent<Toggle>().isOn;
        _object.SetActive(active);
    }
}
