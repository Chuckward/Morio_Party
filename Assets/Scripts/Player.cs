using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XInputDotNetPure;

public class Player : MonoBehaviour {

    public Waypoint currentPosition;               // collection of waypoints
    public Waypoint nextPosition;
    public AudioSource backGroundMusic;
    public Camera playerCamera;                 // camera to control by player
    public GameObject player;                   // dynamic character!
    public GameObject dice;
    public UnityEvent m_StartPlayer;
    private System.Random rng;
    public string alignment;
    public Character character;

    private Vector3 offset;
    private Quaternion offsetRotation;
    private int currentSegment;
    private float transition;

    private UnityEvent c_ConfirmA;

    /** UI Changes due to player **/
    private GameObject canvas;
    public Image avatar;
    public Text playerAlertText;
    public Text diceText;
    public Text textPause;
    public Text coinsText;
    public Text starsText;
    public Color color;

    /** Other Scripts related to this one **/
    private Dice diceScript;
    private PlayerControls controls;
    public PlayerStatistics statistics;
    private MapLogic mapScript;

    /** Player State Machine **/
    public enum PlayerState
    {
        Inactive,
        Alert,
        ZoomIn,
        RollDice,
        UseItem,
        Move,
        Stop,
        Decide,
        TriggerEvents,
        Reset,
        Pause   
    }

    public enum Color
    {
        Blue,
        Red,
        Green,
        None
    }

    public PlayerState currentPlayerState;
    private PlayerState lastPlayerState;

    /** Gamepad Control Events **/
    private UnityEvent m_ViewMap;
    private UnityEvent m_Pause;

    /** Player Current Data **/
    private int coins = 0;
    public int stars = 0;
    private int players_turn;
    private int dice_rolled;
    public Item activeItem;
    private Item[] itemContainer; // 3 items per player max
    public Image[] itemImages; 

    private void Awake()
    {
        diceScript = GameObject.FindGameObjectWithTag("Dice").GetComponentInChildren<Dice>();
        mapScript = GameObject.FindGameObjectWithTag("Map").GetComponentInChildren<MapLogic>();
        statistics = new PlayerStatistics();
        offset = new Vector3(0, 15, 10);
        offsetRotation = new Quaternion(0, 90, 0, 0);
        m_ViewMap = new UnityEvent();
        m_Pause = new UnityEvent();
        m_StartPlayer = new UnityEvent();
        
        m_Pause.AddListener(TogglePause);
        m_StartPlayer.AddListener(StartPlayer);

        c_ConfirmA = new UnityEvent();

        dice_rolled = 0;
        currentPlayerState = PlayerState.Inactive;
        textPause.enabled = false;
        diceText.enabled = false;
        color = Color.None;

        canvas = mapScript.canvas;

        itemContainer = new Item[3];

        itemContainer[0] = new None();   // TODO temp
        itemContainer[1] = new None();
        itemContainer[2] = new None();

        activeItem = new None();
        rng = new System.Random();
    }

    private void Start()
    {
        Transform[] temp = canvas.transform.Find("Avatars/" + mapScript.GetCurrentPlayer().avatarTag).GetComponentsInChildren<Transform>(true);

        foreach (Transform child in temp)
        {
            if (child.tag == "UI_Avatar_Item")
            {
                itemImages = child.GetComponentsInChildren<Image>(true);
            }

        }
        itemImages[0].sprite = itemContainer[0].itemImage;
    }

    private void Update()
    {
        //TODO: make dynamic for every player character
        rng.Next();
        if (currentPlayerState != PlayerState.Inactive)
        {
            if (GamePad.GetState(mapScript.GetCurrentPlayer().index).Buttons.Start.Equals(ButtonState.Pressed))
            {
                m_Pause.Invoke();
            }
            if (currentPlayerState == PlayerState.Alert)
            {
                playerAlertText.text = "Your turn!";
                playerAlertText.enabled = true;
                if (GamePad.GetState(mapScript.GetCurrentPlayer().index).Buttons.A.Equals(ButtonState.Pressed))
                {
                    currentPlayerState = PlayerState.ZoomIn;
                    playerAlertText.enabled = false;
                }
            }
            else if (currentPlayerState == PlayerState.Move)
            {
                MoveCharacter();
            }
            else if (currentPlayerState == PlayerState.RollDice)
            {
                if (GamePad.GetState(mapScript.GetCurrentPlayer().index).Buttons.A.Equals(ButtonState.Pressed))
                {
                    dice_rolled = rng.Next(10) + 1;
                    diceText.enabled = true;
                    diceText.text = dice_rolled.ToString();
                    currentPlayerState = PlayerState.Move;
                    diceScript.diceDeactivate.Invoke();
                    //statistics.DiceRolled(dice_rolled, mapScript.currentTurn);
                }
                if (GamePad.GetState(mapScript.GetCurrentPlayer().index).Buttons.X.Equals(ButtonState.Pressed))    // Use Item
                {
                    mapScript.SwitchUIMainItem(true);
                    currentPlayerState = PlayerState.UseItem;
                    diceScript.diceDeactivate.Invoke();
                }
            }
            else if (currentPlayerState == PlayerState.UseItem)
            {
                if (GamePad.GetState(mapScript.GetCurrentPlayer().index).Buttons.X.Equals(ButtonState.Pressed))        // Go Back
                {
                    mapScript.SwitchUIMainItem(false);
                    currentPlayerState = PlayerState.RollDice;
                    diceScript.diceActivate.Invoke();
                }
                else if (GamePad.GetState(mapScript.GetCurrentPlayer().index).Buttons.Y.Equals(ButtonState.Pressed))    // Fuse items
                {

                }
                else if (GamePad.GetState(mapScript.GetCurrentPlayer().index).Buttons.A.Equals(ButtonState.Pressed))    // select item
                {

                }
            }
            else if (currentPlayerState == PlayerState.TriggerEvents)
            {
                if (currentPosition is Field)
                {
                    ((Field)currentPosition).navigatableField.ApplyEffect(this);
                }
                else if (currentPosition is Join)
                {
                    ((Join)currentPosition).navigatableField.ApplyEffect(this);
                }
            }
            else if(currentPlayerState == PlayerState.Reset)
            { 
                avatar.sprite = LoadAvatarSprite();
                currentPlayerState = PlayerState.Inactive;
                offset = new Vector3(0, 15, 10);
                mapScript.m_Player_finished.Invoke();
            }
            else if (currentPlayerState == PlayerState.ZoomIn)
            {
                Vector3 offsetNew = new Vector3(0, 10, 7);

                offset = Vector3.Lerp(offset, offsetNew, Time.deltaTime / 0.25f);

                if(offset == new Vector3(0, 10, 7))
                {
                    currentPlayerState = PlayerState.RollDice;
                    diceScript.diceActivate.Invoke();
                    print("ZoomIn finished");
                }
            }
            else if (currentPlayerState == PlayerState.Decide)
            {
                c_ConfirmA.AddListener(ConfirmA);

                if (GamePad.GetState(mapScript.GetCurrentPlayer().index).ThumbSticks.Left.X > 0.0f)
                {
                    print("Right");
                    if (currentPosition is Split2)
                    {
                        nextPosition = ((Split2)currentPosition).nextRight;
                    }
                    else if (currentPosition is Split3)
                    {
                        nextPosition = ((Split3)currentPosition).nextRight;
                    }
                } else if (GamePad.GetState(mapScript.GetCurrentPlayer().index).ThumbSticks.Left.X < 0.0f)
                {
                    print("Left");
                    if (currentPosition is Split2)
                    {
                        nextPosition = ((Split2)currentPosition).nextLeft;
                    }
                    else if (currentPosition is Split3)
                    {
                        nextPosition = ((Split3)currentPosition).nextLeft;
                    }

                } else if (GamePad.GetState(mapScript.GetCurrentPlayer().index).ThumbSticks.Left.Y < 0.0f)
                {
                    if (currentPosition is Split3)
                    {
                        nextPosition = ((Split3)currentPosition).nextStraight;
                    }
                }
            }
            if(GamePad.GetState(mapScript.GetCurrentPlayer().index).Buttons.A.Equals(ButtonState.Pressed))
            {
                c_ConfirmA.Invoke();
            }
        }
    }

    private void LateUpdate()
    {
        if(currentPlayerState != PlayerState.Inactive)
        {
            playerCamera.transform.position = player.transform.position + offset;
            playerCamera.transform.rotation = new Quaternion(0, 180.0f, -80, 0);
            diceText.text = dice_rolled.ToString();
        }

        coinsText.text = coins.ToString() + "x ";       // always update for external changes like boo or other
        starsText.text = stars.ToString() + "x ";

    }

    private void MoveCharacter()
    {
        if(dice_rolled > 0)
        {
            transition += Time.deltaTime / 0.75f;
            if(transition > 1)
            {
                currentPosition = nextPosition;
                CheckPassBy();
                transition = 0;
                if(currentPosition is Field)
                {
                  //  statistics.Advance();
                    nextPosition = ((Field)currentPosition).next;
                    if(!(((Field)currentPosition).navigatableField is Star))
                        dice_rolled--;
                }
                else if(currentPosition is Split2)
                {
                    lastPlayerState = PlayerState.Move;
                    currentPlayerState = PlayerState.Decide;
                    nextPosition = ((Split)currentPosition).nextRight;
                    currentPosition.GetComponent<Split2>().m_EnableArrows.Invoke();
                    return;
                }
                else if(currentPosition is Split3)
                {
                    lastPlayerState = PlayerState.Move;
                    currentPlayerState = PlayerState.Decide;
                    nextPosition = ((Split)currentPosition).nextRight;
                    currentPosition.GetComponent<Split3>().m_EnableArrows.Invoke();
                    return;
                }
                else if(currentPosition is StartPoint)
                {
                    nextPosition = ((StartPoint)currentPosition).next;
                }
                else if(currentPosition is Join)                                // TODO: remember effect of reverse mushroom!
                {
                    //statistics.Advance();
                    nextPosition = ((Join)currentPosition).next;
                    dice_rolled--;
                }
                else
                {
                    throw new System.NotImplementedException();                 // Should not be possible if everything is covered
                }
            } else
            {
                player.transform.position = currentPosition.LinearPosition(null, transition, nextPosition);
                player.transform.rotation = currentPosition.transform.rotation;
            }
        } 
        else        // no dice rolls left, trigger related events (+ coins, - coins, ?, Bowser, Bank....)
        {
            currentPlayerState = PlayerState.TriggerEvents;
            diceText.enabled = false;
        }
    }

    void StartPlayer()
    {
        if (currentPlayerState == PlayerState.Inactive)
        {
            currentPlayerState = PlayerState.Alert;
        }
    }

    void TogglePause()
    {
        if(currentPlayerState == PlayerState.Pause)
        {
            //if(GamePad.GetState(PlayerIndex.One).Buttons.Start.Equals(ButtonState.Released))
            {
                currentPlayerState = lastPlayerState;
                textPause.enabled = false;
                backGroundMusic.volume = 1.0f;
            }
            
        } else
        {
            //if(GamePad.GetState(PlayerIndex.One).Buttons.Start.Equals(ButtonState.Released))
            {
                lastPlayerState = currentPlayerState;
                currentPlayerState = PlayerState.Pause;
                textPause.enabled = true;
                backGroundMusic.volume = 0.2f;
            }
        }
    }

    private void ConfirmA()
    {
        lastPlayerState = PlayerState.Decide;
        currentPlayerState = PlayerState.Move;
        c_ConfirmA.RemoveAllListeners();
        // TODO: play confirm sound
        if(currentPosition is Split2)
        {
            ((Split2)currentPosition).m_DisableArrows.Invoke();
        }
        else    // Split3
        {
            ((Split3)currentPosition).m_DisableArrows.Invoke();
        }
        
    }

    private void CheckPassBy()
    {
        if (currentPosition is Field)
        {
            ((Field)currentPosition).navigatableField.PassBy(this);
        }
        else if (currentPosition is Join)
        {
            ((Join)currentPosition).navigatableField.PassBy(this);
        }
    }

    public void ChangeCoins(int amount)
    {
        if (amount > 0)
            coins += amount;
        else
        {
            if (amount < -49) 
            {
                statistics.AddUnlucky(PlayerStatistics.Unluckies.Coins_lost50);
            }
            for (; amount < 0 && coins > 0; amount++)
            {
                coins--;    // players coins can't get below zero
            }
        }
    }

    public int GetPlayerCoins()
    {
        return coins;
    }

    public int GetPlayerStars()
    {
        return stars;
    }

    public Sprite LoadAvatarSprite()
    {
        switch(color)
        {
            case Color.None:
                return Resources.Load<Sprite>("HUD/avatar_" + alignment + "_none");
            case Color.Blue:
                return Resources.Load<Sprite>("HUD/avatar_" + alignment + "_blue");
            case Color.Red:
                return Resources.Load<Sprite>("HUD/avatar_" + alignment + "_red");
            case Color.Green:
                return Resources.Load<Sprite>("HUD/avatar_" + alignment + "_green");
            default:
                return Resources.Load<Sprite>("HUD/avatar_" + alignment + "_none");
        }
    }
}
