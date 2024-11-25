using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour
{
    public float stretchMin;
    public float stretchMax;
    public float mouseMin;
    public float mouseMax;
    public Image wireEnd;
    Vector3 startPoint;
    Vector2 startSize;
    private bool isDragging = false;

    private void Start()
    {
        startPoint = wireEnd.rectTransform.position;
        startSize = wireEnd.rectTransform.sizeDelta;
    }

    private void Update()
    {
        if (!isDragging && Input.GetMouseButtonDown(0) && IsPointerOverUIElement()) isDragging = true;

        else if (isDragging)
        {
            MoveWire();
            StretchWire();
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = false;
                 
            }
        }
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.transform.parent.parent == transform.parent)
                return true;
        }
        return false;
    }

    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
    private void MoveWire()
    {
        // Reset the position
        transform.position = startPoint;

        // Get the mouse input
        Vector3 mouse = Input.mousePosition;
        mouse.z = 0;
        //mouse.y = transform.position.y;

        // Set the rotation to look at the mouse
        wireEnd.rectTransform.LookAt(mouse);
        Quaternion rotation = wireEnd.rectTransform.rotation;
        rotation = new Quaternion(0, 0, -rotation.x, 1);
        wireEnd.rectTransform.rotation = rotation;

        // Get the direction from the wire start to the mouse
        Vector3 direction = mouse - startPoint;

        // Set the position to the mouse
        Vector3 newPosition = mouse;
        transform.position = newPosition;
    }
    private void StretchWire()
    {
        // Get the mouse input
        Vector3 mouse = Input.mousePosition;
        mouse.z = 0;
        //mouse.y = transform.position.y;

        Vector3 newPosition = mouse;

        // Get some variables for the stretching
        float dist = Vector2.Distance(startPoint, newPosition);
        if (dist < mouseMin) dist = mouseMin;
        if (dist > mouseMax) dist = mouseMax;

        // Get the percentage of the mouse position regarding how far it is to the max.
        float distPercentile = dist / mouseMax;

        // Check how much the wire needs to stretch to match the mouse position percentage.
        float stretchDiff = stretchMax - stretchMin;
        float stretch = stretchDiff * distPercentile;

        // Stretch the wire
        Vector2 size = startSize;
        size.x += stretch;
        wireEnd.rectTransform.sizeDelta = size;

        Debug.Log(dist);
    }   
}