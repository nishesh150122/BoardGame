using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderOverUI : MonoBehaviour
{
    public bool CheckIsOverSlider()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        // Set the position of the pointer event data to the mouse position
        eventData.position = Input.mousePosition;

        // Create a list to store the raycast results
        var results = new List<RaycastResult>();

        // Raycast into the UI using the GraphicRaycaster
        EventSystem.current.RaycastAll( eventData, results );

        foreach (var result in results)
        {
            if (result.gameObject.name == "Handle")
            {
                return true;
            }
        }

        return false;
    }
    
}
