using UnityEngine;
using System.Collections;
using System;

public class Player
{
	private int _userId;
	private string _name;
	private int _coins;
	private int _betAmt;

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

    public int GetBet ()
    {
		return _betAmt;
    }
}