using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIResultMsg{
    public static string GetResultMsg(Result result, Text winningText)
    {
        string msg = string.Empty;
        switch (result)
        {
            case Result.Won:
                msg = "Congratulations Player Wins!!";
                winningText.color = Color.green;
                break;
            case Result.Lost:
                msg = "You Lose.....Opponent Wins Try Again.";
                winningText.color = Color.red;
                break;
            case Result.Draw:
                msg = "DRAW!";
                winningText.color = Color.yellow;
                break;
        }
        return msg;
    }
}
