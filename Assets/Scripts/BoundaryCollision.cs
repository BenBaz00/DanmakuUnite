using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCollision : MonoBehaviour
{
    private Vector3 objectSize;

    private float xBounds;
    private float yBounds;

    // Start is called before the first frame update
    void Start()
    {
        // Get Y/X Boundary
        yBounds = Camera.main.orthographicSize;
        xBounds = yBounds * 1.333333f;

        // Get Object Height/Width
        objectSize = transform.GetComponent<SpriteRenderer>().bounds.extents;
    }


    void LateUpdate()
    {
        Vector2 position = (Vector2)transform.position;
        
        // Force gameobjects position within the bounds of the camera view
        position.x = Mathf.Clamp(position.x, xBounds * -1 + objectSize.x, xBounds - objectSize.x);
        position.y = Mathf.Clamp(position.y,  yBounds * -1 + objectSize.y, yBounds - objectSize.y);
        transform.position = position;
    }
}
