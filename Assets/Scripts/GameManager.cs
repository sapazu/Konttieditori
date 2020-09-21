using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public bool showBlueprint;
    public bool showInside;

    Vector3 startTouchPos;
    Vector3 endTouchPos;

    CameraController camController;

    GameObject kontti;
    GameObject menu;
    GameObject popover;
    Image itemImage;
    Text itemName;
    Text itemDescription;

    RaycastHit wallHit;
    RaycastHit itemHit;
    public LayerMask wallMask;
    public LayerMask itemMask;

    Transform selectedWall;
    Transform oldWall;

    Text DebugText;

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
        DebugText = GameObject.Find("DebugText").GetComponent<Text>();
        popover = GameObject.Find("PopOver");
        itemImage = GameObject.Find("ItemImage").GetComponent<Image>();
        itemName = GameObject.Find("ItemName").GetComponent<Text>();
        itemDescription = GameObject.Find("ItemDescription").GetComponent<Text>();
        popover.SetActive(false);
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //CastForWall();
    }

    void CastForWall()
    {
        if (Physics.Raycast(camController.camPivot.position, camController.camPivot.forward * -1, out wallHit, 10, wallMask))
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

    public void OnTouchEnter()
    {
        startTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        DebugText.text = startTouchPos.ToString();
    }

    public void OnTouchExit()
    {
        endTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(startTouchPos == endTouchPos)
        {
            DebugText.text = "Click";
            CastForItem();
        }
    }

    void CastForItem()
    {
        DebugText.text = "Cast";
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out itemHit, 40, itemMask))
        {
            if (!itemHit.transform.CompareTag("Item"))
            {
                return;
            }
            ItemInfo info = itemHit.transform.GetComponent<ItemInfo>();
            if(info != null)
            {
                itemImage.sprite = info.itemImage;
                itemName.text = info.itemName;
                itemDescription.text = info.itemDescription;

                popover.SetActive(true);
            }

            DebugText.text = itemHit.transform.gameObject.name;
        }
        else
        {
            popover.SetActive(false);
        }
    }
}
