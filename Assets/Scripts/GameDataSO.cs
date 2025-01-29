using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    [CreateAssetMenu(fileName = "GameData", menuName = "Game/GamePlayData", order = 0)]
    public class GameDataSO : ScriptableObject
    {
        public float timer;
        public TMP_Text timerText;
        public TMP_Text scoreText;
        
        public AudioClip collisionSound;
        public AudioClip releaseSound;
        public AudioClip scoreSound;

    }