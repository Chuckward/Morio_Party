using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowserSpace : MonoBehaviour {

    private BowserState currentState;
    private BowserState nextState;
    private int multiplicator = 0;
    private const int COINS = 5;
    private System.Random rng;

    public Text bowserText;
    public AudioSource voice;

    private enum BowserState
    {
        Inactive,
        Introduction,
        ConfirmA,
        Roulette,
        Event,
        ByeText
    };

    private void Awake()
    {
        rng = new System.Random();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        rng.Next();

        switch(currentState)
        {
            case BowserState.Inactive:
                break;
            case BowserState.Introduction:
                bowserText.text = "";
                voice.clip = Resources.Load<AudioClip>("Audio/Characters/Bowser/yell");
                voice.Play();
                break;
            case BowserState.ConfirmA:
                break;
            case BowserState.Roulette:
                break;
            case BowserState.Event:
                break;
            case BowserState.ByeText:
                bowserText.text = "";
                voice.clip = Resources.Load<AudioClip>("Audio/Characters/Bowser/laugh");
                voice.Play();
                break;
        }
	}

    private MinigameListItem[] bowserGames = new MinigameListItem[] 
    {

    };

    private BowserEvent[] bowserEvents = new BowserEvent[]
    {
        new BowserEvent("Bowsers Chance Time", new BowserHappening(BowserHappening.Type.ChanceTime)),
        new BowserEvent("Bowser Coin Potluck", new BowserHappening(BowserHappening.Type.CoinPotLuck)),
        new BowserEvent("1000 Coins Present", new BowserHappening(BowserHappening.Type.Coins1k)),
        new BowserEvent(" Coins for Bowser", new BowserHappening(BowserHappening.Type.CoinsForBowser)),
        new BowserEvent("Bowsers Curse", new BowserHappening(BowserHappening.Type.CurseMovement)),
        new BowserEvent("Bowsers Curse", new BowserHappening(BowserHappening.Type.CurseReverse)),
        new BowserEvent("Bowsers Shuffle", new BowserHappening(BowserHappening.Type.CurseSwitch)),
        new BowserEvent("Bowser Revolution", new BowserHappening(BowserHappening.Type.Revolution)),
        new BowserEvent("100 Stars Present", new BowserHappening(BowserHappening.Type.Stars100)),
        new BowserEvent("Bowsers Visit", new BowserHappening(BowserHappening.Type.Visit))
    };
}
