using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UseFullCodes.ToolKit;

public class EventManager : SingletonBehaviour<EventManager>
{
    public UnityEvent OnRoundOver = new UnityEvent();
    public UnityEvent<string> OnGottiCollected = new UnityEvent<string>();
    
}
