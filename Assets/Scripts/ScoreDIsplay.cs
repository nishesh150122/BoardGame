using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class ScoreDIsplay : MonoBehaviour
{
    private TextMeshProUGUI playerOneScoreText,playerTwoScoreText;
    private TextMeshPro _textMeshPro;

    private void Awake()
    {
        playerOneScoreText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        playerTwoScoreText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        playerOneScoreText.text = GameManager.Instance.playerOneScoreData.GetPlayerOneScore().ToString();
        playerTwoScoreText.text = GameManager.Instance.playerTwoScoreData.GetPlayerOneScore().ToString();
    }

    private void Update()
    {
        if (GameManager.Instance._playerState==PlayerState.One)
        {
            playerOneScoreText.text = GameManager.Instance.playerOneScoreData.GetPlayerOneScore().ToString();
        }
        else
        {
            playerTwoScoreText.text = GameManager.Instance.playerTwoScoreData.GetPlayerOneScore().ToString();
        }
    }
}
