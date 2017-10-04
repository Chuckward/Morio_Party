using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MapMinigameSM : MonoBehaviour {

    private GameState currentState;
    public UnityEvent m_chooseMinigame;
    public UnityEvent m_chooseItemgame;
    public UnityEvent m_chooseBattlegame;

    public MapLogic mapScript;

    private System.Random rng;
    private MinigameListItem[] listing;
    private int chosenGameIndex;

    /** UI Elements **/
    private Text[] minigameText;
    public GameObject minigameRoulette;

    private float transition;
    private int count;

    private enum GameState
    {
        Inactive,
        MinigameInit,
        ItemgameInit,
        BattlegameInit,
        ChooseAnimation,
        ChosenAnimation,
        LoadMinigame
    }

    private void Awake()
    {
        rng = new System.Random();
        m_chooseMinigame = new UnityEvent();
        m_chooseItemgame = new UnityEvent();
        m_chooseBattlegame = new UnityEvent();

        m_chooseMinigame.AddListener(ChooseMinigame);
        m_chooseItemgame.AddListener(ChooseItemgame);
        m_chooseBattlegame.AddListener(ChooseBattlegame);

        mapScript = GameObject.Find("Map").GetComponent<MapLogic>();
        minigameText = minigameRoulette.GetComponentsInChildren<Text>();

        minigameRoulette.SetActive(false);
    }

    private void ChooseMinigame()
    {
        currentState = GameState.MinigameInit;
    }

    private void ChooseItemgame()
    {
        currentState = GameState.ItemgameInit;
    }

    private void ChooseBattlegame()
    {
        currentState = GameState.BattlegameInit;
    }
	
	// Update is called once per frame
	void Update () {
        rng.Next();
		switch(currentState)
        {
            case GameState.Inactive:
                break;
            case GameState.MinigameInit:
                int blues = 0;
                transition = 0f;
                mapScript.playerOne.color = mapScript.playerOne.color == Player.Color.Green ? PickRandomColor() : mapScript.playerOne.color;
                mapScript.playerTwo.color = mapScript.playerTwo.color == Player.Color.Green ? PickRandomColor() : mapScript.playerTwo.color;
                mapScript.playerThree.color = mapScript.playerThree.color == Player.Color.Green ? PickRandomColor() : mapScript.playerThree.color;
                mapScript.playerFour.color = mapScript.playerFour.color == Player.Color.Green ? PickRandomColor() : mapScript.playerFour.color;

                RefreshAvatarSprites();

                blues += mapScript.playerOne.color == Player.Color.Blue ? 1 : 0;
                blues += mapScript.playerTwo.color == Player.Color.Blue ? 1 : 0;
                blues += mapScript.playerThree.color == Player.Color.Blue ? 1 : 0;
                blues += mapScript.playerFour.color == Player.Color.Blue ? 1 : 0;

                listing = new MinigameListItem[5];
                minigameRoulette.SetActive(true);
                
                print(minigameText);

                if (blues == 4 || blues == 0) // 4 player game 
                {
                    listing = PickRandomMinigames(list4x4, 5);
                }
                else if (blues == 2) // 2 vs 2 player game
                {
                    listing = PickRandomMinigames(list2x2, 5);
                }
                else if (blues == 1 || blues == 3) // 1 vs 3 player game
                {
                    listing = PickRandomMinigames(list1x3, 5);
                }
                for (int i = 0; i < listing.Length; i++)
                {
                    minigameText[i].text = listing[i].name;
                }
                chosenGameIndex = 10 + rng.Next(10);
                print("Game to play index: " + chosenGameIndex);
                currentState = GameState.ChooseAnimation;
                break;
            case GameState.ItemgameInit:
                transition = 0f;
                listing = new MinigameListItem[3];
                listing = PickRandomMinigames(itemGames, 3);
                for (int i = 0; i < listing.Length; i++)
                {
                    minigameText[i].text = listing[i].name;
                }
                chosenGameIndex = 9 + rng.Next(9);
                currentState = GameState.ChooseAnimation;
                break;
            case GameState.BattlegameInit:
                transition = 0f;
                listing = new MinigameListItem[5];
                listing = PickRandomMinigames(battle, 5);
                for (int i = 0; i < listing.Length; i++)
                {
                    minigameText[i].text = listing[i].name;
                }
                chosenGameIndex = 10 + rng.Next(10);
                currentState = GameState.ChooseAnimation;
                break;
            case GameState.ChooseAnimation:
                transition += Time.deltaTime * 6;
                print("Transition: " + transition);
                minigameText[((int)transition % listing.Length)].color = new Color(0f, 1f, 0f);
                minigameText[(int)(transition - 1) % listing.Length].color = new Color(1f, 1f, 1f);
                if ((Math.Floor(transition) > chosenGameIndex - 1))
                {
                    chosenGameIndex = chosenGameIndex % listing.Length;
                    currentState = GameState.ChosenAnimation;
                    transition = 0;
                    count = 0;
                }
                break;
            case GameState.ChosenAnimation:
                transition += Time.deltaTime * 3;
                print("Count: " + count);
                print("Transition II: " + transition);
                if (Math.Floor(transition) == 1)
                {
                    minigameText[chosenGameIndex].color = new Color(0f, 0f, 1f);
                }
                else if(Math.Floor(transition) == 2)
                { 
                    minigameText[chosenGameIndex].color = new Color(1f, 1f, 1f);
                    transition = 0;
                    count++;
                }
                if (count == 6)
                {
                    currentState = GameState.LoadMinigame;
                }
                break;
            case GameState.LoadMinigame:
                mapScript.playerOne.color = Player.Color.None;
                mapScript.playerTwo.color = Player.Color.None;
                mapScript.playerThree.color = Player.Color.None;
                mapScript.playerFour.color = Player.Color.None;

                RefreshAvatarSprites();

                minigameRoulette.SetActive(false);

                SceneManager.LoadSceneAsync(listing[chosenGameIndex].sceneName);
                mapScript.ExecuteNextState();
                currentState = GameState.Inactive;
                break;
        }
	}

    private Player.Color PickRandomColor()
    {
        if (rng.Next(2) == 0)
            return Player.Color.Blue;
        return Player.Color.Red;
    }

    private void RefreshAvatarSprites()
    {
        mapScript.playerOne.avatar.sprite = mapScript.playerOne.LoadAvatarSprite();
        mapScript.playerTwo.avatar.sprite = mapScript.playerTwo.LoadAvatarSprite();
        mapScript.playerThree.avatar.sprite = mapScript.playerThree.LoadAvatarSprite();
        mapScript.playerFour.avatar.sprite = mapScript.playerFour.LoadAvatarSprite();
    }

    public MinigameListItem[] PickRandomMinigames(MinigameListItem[] listToPick, int numToPick)
    {
        MinigameListItem[] temp = new MinigameListItem[numToPick];
        var tempList = new ArrayList();

        for (int i = 0; i < numToPick; i++)  // pick 6 games at random 
        {
            temp[i] = listToPick[rng.Next(listToPick.Length)];
            if (tempList.Contains(temp[i]))     // filter duplicates
                i--;
            else
                tempList.Add(temp[i]);
        }
        return temp;
    }

    /** Minigames **/
    private MinigameListItem[] list4x4 = new MinigameListItem[] {
        new MinigameListItem("Piranha Fishing", "PiranhaFishing"),
        new MinigameListItem("Robo Marathon", "RoboMarathon"),
        new MinigameListItem("Sushi Go Round", "SushiGoRound"),
        new MinigameListItem("M.P.I.Q", "MPIQ"),
        new MinigameListItem("Bombs Away", "BombsAway"),
        new MinigameListItem("Hot Rope Jump", "HotRopeJump"),
        new MinigameListItem("Totem Pole Pound", "TotemPolePound"),
        new MinigameListItem("Bookworm", "Bookworm"),

    };
    private MinigameListItem[] list1x3 = new MinigameListItem[] {
        new MinigameListItem("Simon Says", "SimonSays"),
        new MinigameListItem("Bob omb Barrage", "BobombBarage"),
        new MinigameListItem("Bowling", "Bowling"),
        new MinigameListItem("Tree Stomp", "TreeStomp"),
        new MinigameListItem("temp2", "Bowling"),
        new MinigameListItem("temp3", "Bowling")
    };
    private MinigameListItem[] list2x2 = new MinigameListItem[] {
        new MinigameListItem("Pizza Pronto", "PizzaPronto"),
        new MinigameListItem("Speed Hockey", "SpeedHockey"),
        new MinigameListItem("Hipster Lumberjacks", "HipserLumberjacks"),
        new MinigameListItem("Fish Cash(i)er", "FishCashier"),
        new MinigameListItem("temp1", "Bowling"),
        new MinigameListItem("temp2", "Bowling"),
        new MinigameListItem("temp3", "Bowling")
    };
    private MinigameListItem[] battle = new MinigameListItem[] {
        new MinigameListItem("Bumper Balloon Cars", "BumperBalloonCars"),
        new MinigameListItem("Crazy Cutters", "CrazyCutters"),
        new MinigameListItem("Face Lift", "FaceLift"),
        new MinigameListItem("Hot Bob Omb", "HotBobOmb"),
        new MinigameListItem("Drag Race", "DragRace")
    };
    private MinigameListItem[] itemGames = new MinigameListItem[] {
        new MinigameListItem("Roll out the Barrels", "RollOutTheBarrels")
    };
    private MinigameListItem[] duelGames = new MinigameListItem[] {
        new MinigameListItem("Chicken Run", "ChickenRun"),
        new MinigameListItem("Bowser Toss", "BowserToss"),
        new MinigameListItem("Shoot-em-up", "ShootEmUp")
    };
    private MinigameListItem[] casinoGames = new MinigameListItem[] {
        new MinigameListItem("Roulette", "Roulette"),
        new MinigameListItem("Black Jack", "BlackJack")
    };

    
}
