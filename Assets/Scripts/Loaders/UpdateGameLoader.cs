using UnityEngine;
using System.Collections;
using System;

public class UpdateGameLoader
{
	public delegate void OnLoadedAction(Hashtable gameUpdateData);
	public event OnLoadedAction OnLoaded;

    private Result _drawResult;
    public int BetAmount
    {
        get
        {
            return Player.Bet;
        }
    }



    public Player Player { get; internal set; }

    private UseableItem _choice;


    public UpdateGameLoader(UseableItem playerChoice)
	{
		_choice = playerChoice;
	}

	public void load()
	{
		UseableItem opponentHand = (UseableItem)Enum.GetValues(typeof(UseableItem)).GetValue(UnityEngine.Random.Range(1, 4));

		Hashtable mockGameUpdate = new Hashtable();
		mockGameUpdate["resultPlayer"] = _choice;
		mockGameUpdate["resultOpponent"] = opponentHand;
		mockGameUpdate["coinsAmountChange"] = GetCoinsAmount(_choice, opponentHand);
		mockGameUpdate["drawResult"] = _drawResult;
		mockGameUpdate["freeCash"] = 10  * Player.GetUserLvl();
		OnLoaded(mockGameUpdate);
	}

	private int GetCoinsAmount (UseableItem playerHand, UseableItem opponentHand)
	{
		_drawResult = ResultAnalyzer.GetResultState(playerHand, opponentHand);

		if (_drawResult.Equals (Result.Won))
		{
			return BetAmount;
		}
		else if (_drawResult.Equals (Result.Lost))
		{
			return -(BetAmount);
		}
		else
		{
			return 0;
		}

		return 0;
	}
}