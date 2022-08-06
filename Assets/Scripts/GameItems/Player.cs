using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class Player
{
	private int _userId;
	private string _name;
	private int _coins;
	private int _betAmt;

    public int Bet { 
		get
        {
			return _betAmt;
        }
		set
        {
			_betAmt = value;
        }
	}

    public Player(Hashtable playerData)
	{
		_userId = (int)playerData["userId"];
		_name = playerData["name"].ToString (); 
		_coins = (int)playerData["coins"];
		_betAmt = (int)playerData["bet"];
	}
	
	public int GetUserId()
	{
		return _userId;
	}
	
	public string GetName()
	{
		return _name;
	}

	public int GetCoins()
	{
		return _coins;
	}

	public void ChangeCoinAmount(int amount)
	{
		_coins += amount;
	}

	public bool CanBet()
    {
		var inBetRange = Enumerable.Range(1, _coins).Contains(_betAmt);
		if(inBetRange &&  (_coins >= _betAmt))
        {
			Debug.Log($" Bet within range {inBetRange}");
			return true;
        }
		return false;
    }

    internal void AdustBet(bool increase)
    {
		if (increase)
		{
			Bet++;
		}
		else
		{
				Bet--;
           
		}
	}
}