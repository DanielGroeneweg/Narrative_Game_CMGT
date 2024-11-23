using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour
{
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
        mouse.y = transform.position.y;

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
        mouse.y = transform.position.y;

        Vector3 newPosition = mouse;

        // Resize the wire to so it fills from the start position to the mouse position
        float dist = Vector2.Distance(startPoint, newPosition);
        Vector2 size = startSize;
        size.x += dist;
        wireEnd.rectTransform.sizeDelta = size;

        //Debug.Log("distance: " + (dist) + " WireEnd Size Difference: " + (wireEnd.rectTransform.sizeDelta.x - startSize.x));
    }   
}