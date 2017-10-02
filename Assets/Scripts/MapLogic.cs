using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class MapLogic : MonoBehaviour {

    public int currentTurn;
    public int maxTurns;

    public Camera mainCamera;
    private CameraMove cameraMoveScript;
    public GameObject canvas;
    private System.Random rng;

    public UnityEvent m_introductionFinished;

    private Player playerOne;
    private Player playerTwo;
    private Player playerThree;
    private Player playerFour;
    private Player[] order;
    public Image[] ranking;
    private PlayerObject currentPlayer;

    public UnityEvent m_Player_finished;

    private GameState currentState;
    private GameState lastState;
    private GameState nextState;

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
        new MinigameListItem("temp1", "Bowling"),
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
        new MinigameListItem("Hot Bob Omb", "HotBobOmb")
    };
    private MinigameListItem[] itemGames = new MinigameListItem[] {
       
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

    public Transform[] ui_main;
    public Transform[] ui_item;

    public Image HUD_Info;
    public Text HUD_Info_Text;

    //TODO make table containing minigame names and their tags to select quickly
    // to choose one, get number of all viable minigames and get some random ones
    // choose a random number of them and load it !!!without!!! destroying the scene

    private enum GameState
    {
        Start,
        ChooseNextMinigame,
        Minigame,
        BattleGame,
        ItemMinigame,
        Bossfight,
        PlayerFirst,
        PlayerSecond,
        PlayerThird,
        PlayerFourth,
        Introduction,
        DetermineOrder,
        Reset,
        Inactive,               // Inactive waits for an event to be triggered externally
        Exit,
    }

    private void Awake()
    {
        currentTurn = 1;
        currentState = GameState.DetermineOrder;
        ranking = new Image[4];
        rng = new System.Random();

        m_introductionFinished = new UnityEvent();
        m_introductionFinished.AddListener(IntroductionFinished);

        canvas = GameObject.FindGameObjectWithTag("Canvas");

        currentPlayer = new PlayerOne();    // must match player tag!
        ranking[0] = canvas.transform.Find("Avatars/Avatar_first/Rank").GetComponent<Image>();
        ranking[1] = canvas.transform.Find("Avatars/Avatar_second/Rank").GetComponent<Image>();
        ranking[2] = canvas.transform.Find("Avatars/Avatar_third/Rank").GetComponent<Image>();
        ranking[3] = canvas.transform.Find("Avatars/Avatar_fourth/Rank").GetComponent<Image>();
        ranking[0].sprite = Resources.Load<Sprite>("Ranking/first");
        ranking[1].sprite = Resources.Load<Sprite>("Ranking/first");
        ranking[2].sprite = Resources.Load<Sprite>("Ranking/first");
        ranking[3].sprite = Resources.Load<Sprite>("Ranking/first");

        m_Player_finished = new UnityEvent();
        m_Player_finished.AddListener(PlayerFinished);

        Transform temp = canvas.transform.Find("Avatars").GetComponent<Transform>();
        ui_main = new Transform[4];
        ui_item = new Transform[4];
        print(temp.Find("Avatar_first/Items"));
        ui_main[0] = temp.Find("Avatar_first/Main");
        ui_main[1] = temp.Find("Avatar_second/Main");
        ui_main[2] = temp.Find("Avatar_third/Main");
        ui_main[3] = temp.Find("Avatar_fourth/Main");
        ui_item[0] = temp.Find("Avatar_first/Items");
        ui_item[1] = temp.Find("Avatar_second/Items");
        ui_item[2] = temp.Find("Avatar_third/Items");
        ui_item[3] = temp.Find("Avatar_fourth/Items");

        SwitchUIMainItem(false);                // disables/enables item boxes @ start
    }

    // Use this for initialization
    void Start () {
        cameraMoveScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        playerOne.alignment = "left";
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();
        playerTwo.alignment = "right";
        playerThree = GameObject.FindGameObjectWithTag("PlayerThree").GetComponent<Player>();
        playerThree.alignment = "left";
        playerFour = GameObject.FindGameObjectWithTag("PlayerFour").GetComponent<Player>();
        playerFour.alignment = "right";
        HUD_Info.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        rng.Next();
		if(currentState == GameState.Start)
        {
            canvas.SetActive(false);
            currentState = GameState.Introduction;
           
        } 
        else if(currentState == GameState.Introduction)
        {
            cameraMoveScript.m_StartIntroductionCamera.Invoke();
            currentState = GameState.Inactive;
        }
        else if(currentState == GameState.DetermineOrder)
        {
            currentState = GameState.PlayerFirst;       //TODO: change with actual things to do
            canvas.SetActive(true);
        }
        else if(currentState == GameState.PlayerFirst)
        {
            currentPlayer = new PlayerOne();
            playerOne.m_StartPlayer.Invoke();
            currentState = GameState.Inactive;
            nextState = GameState.PlayerSecond;
        }
        else if (currentState == GameState.PlayerSecond)
        {
            currentPlayer = new PlayerTwo();
            playerTwo.m_StartPlayer.Invoke();
            currentState = GameState.Inactive;
            nextState = GameState.PlayerThird;
        }
        else if (currentState == GameState.PlayerThird)
        {
            currentPlayer = new PlayerThree();
            playerThree.m_StartPlayer.Invoke();
            currentState = GameState.Inactive;
            nextState = GameState.PlayerFourth;
        }
        else if (currentState == GameState.PlayerFourth)
        {
            currentPlayer = new PlayerFour();
            playerFour.m_StartPlayer.Invoke();
            currentState = GameState.Inactive;
            nextState = GameState.ChooseNextMinigame;
        }
        else if(currentState == GameState.ChooseNextMinigame)
        {
            int blues = 0;
            playerOne.color = playerOne.color == Player.Color.Green ? PickRandomColor() : playerOne.color; 
            playerTwo.color = playerTwo.color == Player.Color.Green ? PickRandomColor() : playerTwo.color; 
            playerThree.color = playerThree.color == Player.Color.Green ? PickRandomColor() : playerThree.color;
            playerFour.color = playerFour.color == Player.Color.Green ? PickRandomColor() : playerFour.color;

            RefreshAvatarSprites();

            blues += playerOne.color == Player.Color.Blue ? 1 : 0;
            blues += playerTwo.color == Player.Color.Blue ? 1 : 0;
            blues += playerThree.color == Player.Color.Blue ? 1 : 0;
            blues += playerFour.color == Player.Color.Blue ? 1 : 0;

            MinigameListItem[] listing = new MinigameListItem[6];

            if (blues == 4 || blues == 0) // 4 player game 
            {
                listing = PickRandomMinigames(list4x4, 6);
            }
            else if(blues == 2) // 2 vs 2 player game
            {
               listing = PickRandomMinigames(list2x2, 6);
            }
            else if(blues == 1 || blues == 3) // 1 vs 3 player game
            {
                listing = PickRandomMinigames(list1x3, 6);
            }

            for(int i = 0; i < listing.Length; i++)
            {
                print(listing[i].name);
            }

            // TODO give listing visible, start with highlighting box
            // rotate through the list 2 to 3 times, then pick actual game (std. 12 + random(12)) % 6 (range 0 - 5)
            // when picked actual game jump to scene without destroying this scene, before leaving, reset camera, players to std. values
            currentState = GameState.Reset;
        }
        else if(currentState == GameState.ItemMinigame)
        {
            MinigameListItem[] listing = new MinigameListItem[3];
            listing = PickRandomMinigames(itemGames, 3);

        }
        else if(currentState == GameState.Reset)
        {
            playerOne.color = Player.Color.None;
            playerTwo.color = Player.Color.None;
            playerThree.color = Player.Color.None;
            playerFour.color = Player.Color.None;

            RefreshAvatarSprites();

            currentState = GameState.PlayerFirst;
        }
    }

    public PlayerObject GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void PlayerFinished()
    {
        currentState = nextState;
    }

    public void TriggerItemMinigame()
    {
        lastState = currentState;
        currentState = GameState.ItemMinigame;
    }

    public void SwitchUIMainItem(bool fromMainToItem)
    {
        for (int i = 0; i < 4; i++)
        {
            print(i);
            print(ui_main);
            ui_main[i].gameObject.SetActive(!fromMainToItem);
            ui_item[i].gameObject.SetActive(fromMainToItem);
        }
    }

    void IntroductionFinished()
    {
        currentState = GameState.DetermineOrder;
        print("currentState: DetemineOrder");
    }

    private Player.Color PickRandomColor()
    {
        if (rng.Next(2) == 0)
            return Player.Color.Blue;
        return Player.Color.Red;
    }

    private void RefreshAvatarSprites()
    {
        playerOne.avatar.sprite = playerOne.LoadAvatarSprite();
        playerTwo.avatar.sprite = playerTwo.LoadAvatarSprite();
        playerThree.avatar.sprite = playerThree.LoadAvatarSprite();
        playerFour.avatar.sprite = playerFour.LoadAvatarSprite();
    }

    public MinigameListItem[] PickRandomMinigames(MinigameListItem[] listToPick, int numToPick)
    {
        MinigameListItem[] temp = new MinigameListItem[6];
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
}
