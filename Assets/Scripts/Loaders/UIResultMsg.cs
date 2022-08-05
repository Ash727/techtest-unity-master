using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIResultMsg{
    public static string GetResultMsg(Result result)
    {
        string msg = string.Empty;
        switch (result)
        {
            case Result.Won:
                msg = "Congratulations Player Wins!!";
                break;
            case Result.Lost:
                msg = "You Lose.....Opponent Wins Try Again.";
                break;
            case Result.Draw:
                msg = "DRAW!";
                break;
        }
        return msg;
    }
}
