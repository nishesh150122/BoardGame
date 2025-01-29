using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class CameraHelper
{
    public static (Vector3, float) CalculateOrthoSize(Camera cam, float buffer)
    {
        Collider2D[] colliders = GameObject.FindObjectsOfType<Collider2D>();
        if (colliders.Length == 0)
            return (Vector3.zero, 0f); // Return if no colliders are found
        
        var bounds = new Bounds(colliders[0].bounds.center, colliders[0].bounds.size);

        for(int i = 1; i < colliders.Length; i++) {
            bounds.Encapsulate(colliders[i].bounds);
        }
        
        bounds.Expand(buffer);
        var vertical = bounds.size.y;
        var horizontal = bounds.size.x * cam.pixelHeight / cam.pixelWidth;
        var size = Mathf.Max(horizontal, vertical) *.1f;
        var center = bounds.center + new Vector3(1f, 12.5f, -10);
        
        return (center, size);
    }
    public static bool IsPointerOverUIObject(Vector2 touchPosition) 
    {
        // Set up the PointerEventData with the given position
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = touchPosition;

        // Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        // Raycast using the Graphics Raycaster and mouse click position
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        // Return true if there's at least one UI element under the position
        return results.Count > 0;
    }
    
}