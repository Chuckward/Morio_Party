using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MapLogic : MonoBehaviour {

    public int currentTurn;
    public int maxTurns;

    public Camera mainCamera;
    private CameraMove cameraMoveScript;
    public GameObject canvas;

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
    private Minigame[] list4x4;
    private Minigame[] list1x3;
    private Minigame[] list2x2;
    private Minigame[] battle;

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
        SpecialMinigame,
        Bossfight,
        PlayerFirst,
        PlayerSecond,
        PlayerThird,
        PlayerFourth,
        Introduction,
        DetermineOrder,
        Inactive,               // Inactive waits for an event to be triggered externally
        Exit,
    }

    private void Awake()
    {
        currentTurn = 1;
        currentState = GameState.DetermineOrder;
        ranking = new Image[4];

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
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();
        //playerThree = GameObject.FindGameObjectWithTag("PlayerThree").GetComponent<Player>();
        //playerFour = GameObject.FindGameObjectWithTag("PlayerFour").GetComponent<Player>();
        HUD_Info.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
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
            nextState = GameState.PlayerFirst;
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

        }
        else
        {

        }
    }

    public PlayerObject GetCurrentPlayer()
    {
        return currentPlayer;
    }

    private void PlayerFinished()
    {
        currentState = nextState;

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
}
