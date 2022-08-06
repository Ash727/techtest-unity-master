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
	private Text _levelLabel;
    private Text _freeCashAmt;

	[SerializeField]
	private ModalManager _modalManager;

	private Player _player;
    private Text _betAmountLabel;
    private TMPro.TMP_InputField _betAmounText;

    private Text _resultsText;
    private int freeCashAmt = 10;

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
		_levelLabel = transform.Find ("Canvas/LevelLabel").GetComponent<Text>();
		_freeCashAmt = transform.Find ("Canvas/GetCashButton/Text").GetComponent<Text>();
		_freeCashAmt.text = $"Free Cash: x10";

	}

	void Start()
	{
		_modalManager = FindObjectOfType<ModalManager>();

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
		//_betAmountLabel.text = $"${_player.Bet.ToString()}";
		//_player.Bet = int.TryParse( _betAmountLabel.text, out int value) ? value: PlayerBetValue;	
		_betAmounText.text = ( $"{_player.Bet.ToString()}");
		_levelLabel.text = $"Level: {_player.GetUserLvl().ToString()}";

	}

	public void HandlePlayerInput(string item)
	{
		if(_player.Bet == 0)
        {
			_modalManager.ShowModal(Modal.Types.BetZero);
			return;
        }
        if (!_player.CanBet())
        {
			_modalManager.ShowModal(Modal.Types.InsufficantFundsModal);
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

	public void GetFreeCash()
    {
		_player.ChangeCoinAmount(freeCashAmt);

	}

	public void AdjustBet(bool increase)
	{
		//if (_player.CanBet())
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
            if (bet<0)
            {
				_betAmounText.text = "0";
				bet = 0;
            }
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
		_resultsText.text = UIResultMsg.GetResultMsg((Result)gameUpdateData["drawResult"], _resultsText);
		LevelManager.CheckForLevelUp((Result)gameUpdateData["drawResult"], _player);
		_freeCashAmt.text = $"Free Cash: x{gameUpdateData["freeCash"].ToString()}";
		freeCashAmt = ((int)gameUpdateData["freeCash"]);
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
