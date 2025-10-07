using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractableRaycast : MonoBehaviour
{
    private float raylength = 5;

    private ButtonController raycastObject;

    private KeyCode openDoorKey = KeyCode.Mouse0;
    private KeyCode CollectKey = KeyCode.E;

    public Image crosshair;

    private const string interactableTag = "DoorLever";
    private const string interactableTag1 = "Battery";
    private const string interactableTag2 = "Flashlight";

    public bool hasBattery;
    public bool hasFlashlight;
    public GameObject battery;
    public GameObject Flashlight;
    public GameObject playerFlashlight;


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        //Button Raycast
        if (Physics.Raycast(transform.position, forward, out hit, raylength))
        {
            if (hit.collider.CompareTag(interactableTag))
            {                
                raycastObject = hit.collider.gameObject.GetComponent<ButtonController>();
                CrosshairChange(true);                

                if (Input.GetKeyDown(openDoorKey))
                {
                    raycastObject.PlayAnimation();
                }
            }

            if (hit.collider.CompareTag(interactableTag1))
            {
                if (Input.GetKeyDown(CollectKey))
                {
                    hasBattery = true;
                    Destroy(battery);
                }

                CrosshairChange(true);
            }

            if(hit.collider.CompareTag(interactableTag2))
            {
                if (Input.GetKeyDown(CollectKey))
                {
                    hasFlashlight = true;
                    Destroy(Flashlight);
                    // add time for how long the hasbattery bool will stay true
                }

                CrosshairChange(true);
            }

        }
        else
        {   
             CrosshairChange(false);           
        }

        if(hasBattery == true && hasFlashlight == true)
        {
            playerFlashlight.SetActive(true);
        }

    }
    

    void CrosshairChange(bool on)
    {
        if (on)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
        }
    }
}
