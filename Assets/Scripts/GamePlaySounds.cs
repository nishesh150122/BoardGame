using System;
using UnityEngine;

public class GamePlaySounds : AudioManager
{
    [SerializeField] private GameDataSO gamePlaySounds;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "BlackCoin(Clone)" && collision.gameObject.name == "WhiteCoin(Clone)")
        {
            PlayOneShotAudio(gamePlaySounds.scoreSound);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //PlayOneShotAudio(gamePlaySounds.releaseSound);
            
        }

        if (collision.gameObject.name == "RedCoin(Clone)" || collision.gameObject.name == "BlackCoin(Clone)" ||
            collision.gameObject.name == "WhiteCoin(Clone)")
        {
            PlayOneShotAudio(gamePlaySounds.collisionSound);
        }
    }
}