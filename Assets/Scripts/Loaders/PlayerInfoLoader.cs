using UnityEngine;
using System.Collections;
using System;

public class PlayerInfoLoader
{
	public delegate void OnLoadedAction(Hashtable playerData);
	public event OnLoadedAction OnLoaded;

	public void load()
	{
		Hashtable mockPlayerData = new Hashtable();
		mockPlayerData["userId"] = 1;
		mockPlayerData["name"] = "Player 1";
		mockPlayerData["coins"] = 50;
		mockPlayerData["bet"] = 5;
		mockPlayerData["level"] = 1;

		OnLoaded(mockPlayerData);
	}
}