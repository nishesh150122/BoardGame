using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class Striker : StrikerAgent
{
    private void OnEnable()
    {
        sliderElement.onValueChanged.AddListener(StrikerXPos);
    }

    private void OnDisable()
    {
        EventManager.Instance.OnRoundOver.RemoveAllListeners();
    }

    public void ResetStriker(Vector2 playerPosition)
    {
        transform.localPosition = playerPosition;
        base.ResetStriker();
    }


    
}
