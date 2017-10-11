using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowserHappening {

    public Type happeningType;

	public enum Type
    {
        CoinsForBowser,
        CoinPotLuck,
        Revolution,
        ChanceTime,
        Coins1k,
        Stars100,
        Visit,
        CurseMovement,
        CurseReverse,
        CurseSwitch
    }

    public BowserHappening(Type type)
    {
        happeningType = type;
    }

    public void ExecuteHappening(Player player)
    {
        switch(happeningType)
        {
            case Type.ChanceTime:
                break;
            case Type.Coins1k:
                break;
            case Type.CoinsForBowser:
                break;
            case Type.CoinPotLuck:
                break;
            case Type.CurseMovement:
                break;
            case Type.CurseReverse:
                break;
            case Type.CurseSwitch:
                break;
            case Type.Revolution:
                break;
            case Type.Stars100:
                break;
            case Type.Visit:
                break;
        }
    } 
}
