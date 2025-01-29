using System;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "ScoreData", menuName = "Game/ScoreData", order = 0)]
    public class ScoreDataSO : ScriptableObject
    {
        
        [SerializeField]private int playerOneScore=0;

        private void Awake()
        {
            
        }

        public void ResetPlayerValue()
        {
            playerOneScore = 0;
        }
        
        public int GetPlayerOneScore()
        {
            return playerOneScore;
        }

        public void SetPlayerScore(int value)
        {
            playerOneScore += value;
        }
    }
}