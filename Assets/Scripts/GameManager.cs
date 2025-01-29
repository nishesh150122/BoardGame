using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UseFullCodes.ToolKit;
 public enum PlayerState
{
    One,Two
}
public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] private Slider strikerSlider;
    [SerializeField] private TestScript test;
    [SerializeField] private GameDataSO gameData;
    [SerializeField]public ScoreDataSO playerOneScoreData,playerTwoScoreData;
    [SerializeField] private bool playerOneTurn = true;
    [SerializeField] private bool playerTwoTurn = false;
    [SerializeField] private GameObject coinHolder;
    [SerializeField] private List<Coin> coins = new List<Coin>();
    [SerializeField] private Striker striker;
    [SerializeField] public PlayerState _playerState; 
    public PlayArea[] PlayAreas;
    public string gottiName;

    public bool isSliderClicked = false;

    private void Awake()
    {
        playerTwoScoreData.ResetPlayerValue();
        playerOneScoreData.ResetPlayerValue();
        _playerState = PlayerState.One;
        EventManager.Instance.OnRoundOver.AddListener(SwitchActivePlayer);
        EventManager.Instance.OnGottiCollected.AddListener(ScorePlayer);
    }

    private void OnDisable()
    {
        EventManager.Instance.OnRoundOver.RemoveAllListeners();
    }

    private void SwitchActivePlayer()
    {
        if (_playerState == PlayerState.One)
        {
            ActivatePlayerTwo();
        }
        else
        {
            ActivatePlayerOne();
        }
    }

    private void ActivatePlayerOne()
    {
        test.ResetStriker(new Vector2(0f,-4f));
        _playerState = PlayerState.One;
    }

    private void ActivatePlayerTwo()
    {
        test.ResetStriker(new Vector2(0f,4f));
        _playerState = PlayerState.Two;
    }

    private void Update()
    {
        CheckActivePlayer();
    }

    public int CheckActivePlayer()
    {
        if (_playerState == PlayerState.One)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
    private void ScorePlayer(string gottiName)
    {
        if (_playerState == PlayerState.One)
        {
            UpdatePlayerScore(playerOneScoreData, gottiName);
        }
        else
        {
            UpdatePlayerScore(playerTwoScoreData, gottiName);
        }
    }

    private static void UpdatePlayerScore(ScoreDataSO playerScoreData, string gottiName)
    {
        switch (gottiName)
        {
            case CoinNames.BlackCoinName:
                playerScoreData.SetPlayerScore(5);
                break;
            case CoinNames.WhiteCoinName:
                playerScoreData.SetPlayerScore(10);
                break;
            case CoinNames.RedCoinName:
                playerScoreData.SetPlayerScore(25);
                break;
            case CoinNames.Striker:
                playerScoreData.SetPlayerScore(-5);
                break;
            default:
                break;
        }
    }


}
