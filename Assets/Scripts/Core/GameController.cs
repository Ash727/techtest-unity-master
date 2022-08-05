﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
	public Text playerHand;
	public Text enemyHand;

	private Text _nameLabel;
	private Text _moneyLabel;

	private Player _player;
    private Text _betAmountLabel;
    private Text _resultsPanel;

    void Awake()
	{
		_nameLabel = transform.Find ("Canvas/Name").GetComponent<Text>();
		_moneyLabel = transform.Find ("Canvas/Money").GetComponent<Text>();
		_betAmountLabel = transform.Find ("Canvas/BetAmount").GetComponent<Text>();
		_resultsPanel = transform.Find ("Canvas/Winner").GetComponent<Text>();
	}

	void Start()
	{
		PlayerInfoLoader playerInfoLoader = new PlayerInfoLoader();
		playerInfoLoader.OnLoaded += OnPlayerInfoLoaded;
		playerInfoLoader.load();
	}

	void Update()
	{
		UpdateHud();
	}

	public void OnPlayerInfoLoaded(Hashtable playerData)
	{
		_player = new Player(playerData);
	}

	public void UpdateHud()
	{
		_nameLabel.text = "Name: " + _player.GetName();
		_moneyLabel.text = "Money: $" + _player.GetCoins().ToString();
		_betAmountLabel.text = $"Bet Amounts:{_player.GetBet().ToString()}";
	}

	public void HandlePlayerInput(int item)
	{
		UseableItem playerChoice = UseableItem.None;

		switch (item)
		{
			case 1:
				playerChoice = UseableItem.Rock;
				break;
			case 2:
				playerChoice = UseableItem.Paper;
				break;
			case 3:
				playerChoice = UseableItem.Scissors;
				break;
		}

		UpdateGame(playerChoice, _player);
	}

	private void UpdateGame(UseableItem playerChoice, Player playerData)
	{
		UpdateGameLoader updateGameLoader = new UpdateGameLoader(playerChoice);
		updateGameLoader.OnLoaded += OnGameUpdated;
		updateGameLoader.Player = playerData;
		updateGameLoader.load();
			
	}

	public void OnGameUpdated(Hashtable gameUpdateData)
	{
		playerHand.text = DisplayResultAsText((UseableItem)gameUpdateData["resultPlayer"]);
		enemyHand.text = DisplayResultAsText((UseableItem)gameUpdateData["resultOpponent"]);

		_player.ChangeCoinAmount((int)gameUpdateData["coinsAmountChange"]);

		//_resultsPanel.text = ((Result)gameUpdateData["drawResult"] == Result.Won) ? "Congratulations Player Wins": "Y";
		_resultsPanel.text = UIResultMsg.GetResultMsg((Result)gameUpdateData["drawResult"]);
	}
	

	private string DisplayResultAsText (UseableItem result)
	{
		switch (result)
		{
			case UseableItem.Rock:
				return "Rock";
			case UseableItem.Paper:
				return "Paper";
			case UseableItem.Scissors:
				return "Scissors";
		}

		return "Nothing";
	}
}