using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    [SerializeField] public List<GameObject> collisionObjects  = new List<GameObject>() ;

    private void Start()
    {
        EventManager.Instance.OnRoundOver.AddListener(ResetCollisionList);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (!collisionObjects.Contains(other.gameObject))
        {
            collisionObjects.Add(other.gameObject);
        }
    }

   


    private void ResetCollisionList()
    {
        collisionObjects.Clear();
    }
}
