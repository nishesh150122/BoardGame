using System;
using UnityEngine;
using UnityEngine.Events;

public class Holes : MonoBehaviour
{
    private void Start()
    {
        gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
        gameObject.GetComponent<CircleCollider2D>().radius = 1f;

    }

    private void OnTriggerEnter2D(Collider2D colliderGotti)
    {
        EventManager.Instance.OnGottiCollected?.Invoke(colliderGotti.name);
       

        if (colliderGotti.name == CoinNames.Striker)
        {
            colliderGotti.attachedRigidbody.velocity = Vector2.zero;
        }
        else
        {
            colliderGotti.gameObject.SetActive(false);
        }
    }
}