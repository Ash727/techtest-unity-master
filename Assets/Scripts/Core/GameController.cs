using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
	public Text playerHand;
	public Text enemyHand;
	public int playerBetValue;
	private Text _nameLabel;
	private Text _moneyLabel;
    
	
	public List<GameObject> _modals;

	private Player _player;
    private Text _betAmountLabel;
    private TMPro.TMP_InputField _betAmounText;

    private Text _resultsText;
    public int PlayerBetValue { get =>_player.Bet;  private set {
			_player.Bet = value;
		} }

    void Awake()
	{
		_nameLabel = transform.Find ("Canvas/Name").GetComponent<Text>();
		_moneyLabel = transform.Find ("Canvas/Money").GetComponent<Text>();
		_betAmountLabel = transform.Find ("Canvas/BetAmount").GetComponent<Text>();
		_betAmounText = transform.Find("Canvas/BetAmount/BetInputField").GetComponent<TMPro.TMP_InputField>();

		_resultsText = transform.Find ("Canvas/Winner").GetComponent<Text>();
	}

	void Start()
	{
		PlayerInfoLoader playerInfoLoader = new PlayerInfoLoader();
		playerInfoLoader.OnLoaded += OnPlayerInfoLoaded;
		playerInfoLoader.load();
	}

	void Update()
	{
		if (!_betAmounText.isFocused) { 
			UpdateHud();
		} else
        {
			GetBetFromField();
		}
	}

	public void OnPlayerInfoLoaded(Hashtable playerData)
	{
		_player = new Player(playerData);
	}

	public void UpdateHud()
    {
        _nameLabel.text = "Name: " + _player.GetName();
        _moneyLabel.text = "Money: $" + _player.GetCoins().ToString();
		_betAmountLabel.text = $"${_player.Bet.ToString()}";
		//_player.Bet = int.TryParse( _betAmountLabel.text, out int value) ? value: PlayerBetValue;	
		_betAmounText.text = ( $"{_player.Bet.ToString()}");

	}

	public void HandlePlayerInput(string item)
	{
        if (!_player.CanBet())
        {
            _modals[0].SetActive(true);
            return;
        }

        UseableItem playerChoice = UseableItem.None;

        switch (item)
        {
            case "RockButton":
                playerChoice = UseableItem.Rock;
                break;
            case "PaperButton":
                playerChoice = UseableItem.Paper;
                break;
            case "ScissorsButton":
                playerChoice = UseableItem.Scissors;
                break;
        }

        UpdateGame(playerChoice, _player);
    }


	public void AdjustBet(bool increase)
	{
		if (_player.CanBet())
        {
			_player.AdustBet(increase);
			
        }
      
    }

	public void GetBetFromField()
    {		
        if (string.IsNullOrWhiteSpace(_betAmounText.text))
        {
            Debug.Log("Was Null White space");
            return;
        }
        else
        {
            int.TryParse(_betAmounText.text, out int bet);
			_player.Bet = bet;
		}
        
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
		_resultsText.text = UIResultMsg.GetResultMsg((Result)gameUpdateData["drawResult"], _resultsText);
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