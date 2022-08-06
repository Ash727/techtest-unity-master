using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Result
{
	Won,
	Lost,
	Draw
}

public class ResultAnalyzer
{
	public static Result GetResultState(UseableItem playerHand, UseableItem enemyHand)
	{

		if (isStronger2(playerHand, enemyHand))
		{
			return Result.Won;
		}
		else if (isStronger2(enemyHand, playerHand))
		{
			return Result.Lost;
		}
		else
		{
			return Result.Draw;
		}
	}

    private static bool isStronger2(UseableItem firstHand, UseableItem secondHand)
    {
		bool isWinner = false;
        switch (firstHand)
        {
            case UseableItem.Rock:
				isWinner = secondHand == UseableItem.Scissors ? true : false;                
                break;
            case UseableItem.Paper:
				isWinner = secondHand == UseableItem.Rock ? true : false;
				break;
            case UseableItem.Scissors:
				isWinner = secondHand == UseableItem.Paper ? true : false;
				break;        }
		return isWinner;
    }

	private static bool isStronger (UseableItem firstHand, UseableItem secondHand)
	{
		switch (firstHand)
		{
			case UseableItem.Rock:
			{
				switch (secondHand)
				{
					case UseableItem.Scissors:
						return true;
					case UseableItem.Paper:
						return false;
				}
				break;
			}
			case UseableItem.Paper:
			{
				switch (secondHand)
				{
					case UseableItem.Rock:
						return true;
					case UseableItem.Scissors:
						return false;
				}
				break;
			}
			case UseableItem.Scissors:
			{
				switch (secondHand)
				{
					case UseableItem.Paper:
						return true;
					case UseableItem.Rock:
						return false;
				}
				break;
			}
		}

		return false;
	}
}