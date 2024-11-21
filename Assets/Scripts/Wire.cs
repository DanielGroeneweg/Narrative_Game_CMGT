using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    Vector3 startPoint;
    void Start()
    {
        startPoint = transform.parent.position;
    }

    /*
    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        transform.position = newPosition;

        Vector3  direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        float dist = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }
    */

    private void Update()
    {
        if (transform.parent.name == "Wire Red")
        {
            Debug.Log(Input.mousePosition);

            Debug.Log("Wire: " + transform.position);
        }
    }
}
