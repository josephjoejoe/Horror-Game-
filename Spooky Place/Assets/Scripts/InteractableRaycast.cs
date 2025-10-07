using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractableRaycast : MonoBehaviour
{
    [Range(0f, 100f)]
    public float batteryLevel = 100f; 
    public float batteryDrainRate = 10f; 

    private float raylength = 5;

    private ButtonController raycastObject;

    private KeyCode openDoorKey = KeyCode.Mouse0;
    private KeyCode CollectKey = KeyCode.E;
    private KeyCode UseFlashlight = KeyCode.F;

    public Image crosshair;

    private const string interactableTag = "DoorLever";
    private const string interactableTag1 = "Battery";
    private const string interactableTag2 = "Flashlight";

    public bool hasBattery;
    public bool hasFlashlight;
    public bool flashlightOn;
    public GameObject battery;
    public GameObject Flashlight;
    public GameObject playerFlashlight;

    void Start()
    {
        if (playerFlashlight != null)
            playerFlashlight.SetActive(false);
    }

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
                    batteryLevel = 100f;
                    Destroy(hit.collider.gameObject);
                }

                CrosshairChange(true);
            }

            if(hit.collider.CompareTag(interactableTag2))
            {
                if (Input.GetKeyDown(CollectKey))
                {
                    hasFlashlight = true;
                    Destroy(Flashlight);
                }

                CrosshairChange(true);
            }

        }
        else
        {   
             CrosshairChange(false);           
        }

        if(hasBattery == true && hasFlashlight == true && batteryLevel > 0f)
        {
            if (Input.GetKeyDown(UseFlashlight))
            {
                flashlightOn = !flashlightOn; 
                playerFlashlight.SetActive(flashlightOn);
            }
        }

        // Drain battery if flashlight is on
        if (flashlightOn && hasBattery)
        {
            batteryLevel -= batteryDrainRate * Time.deltaTime;

            // If battery runs out
            if (batteryLevel <= 0f)
            {
                batteryLevel = 0f;
                flashlightOn = false;
                playerFlashlight.SetActive(false);
                hasBattery = false; // You could also leave this true and just check batteryLevel
            }
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
