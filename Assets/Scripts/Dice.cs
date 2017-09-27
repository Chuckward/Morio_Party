using System;
using UnityEngine;
using UnityEngine.Events;

public class Dice : MonoBehaviour {

    private Transform player;
    public UnityEvent diceActivate;
    public UnityEvent diceDeactivate;

    public AudioSource diceAudio;
    public Material[] diceMaterials = new Material[10];

    private bool diceEnabled;
    private float angle;

    private Player playerScript;
    private MapLogic mapScript;

    private int loadOrder = 1;

    // Use this for initialization
    void Start () {
        diceEnabled = false;
        diceActivate = new UnityEvent();
        diceDeactivate = new UnityEvent();
        diceActivate.AddListener(EnableDice);
        diceDeactivate.AddListener(DisableDice);
        mapScript = GameObject.FindGameObjectWithTag("Map").GetComponent<MapLogic>();
        
        DisableDice();
    }
	
    void EnableDice()
    {
        diceEnabled = true;
        player = GameObject.FindGameObjectWithTag(mapScript.GetCurrentPlayer().playerTag).GetComponent<Transform>();     // allows dynamic player selection
        playerScript = GameObject.FindGameObjectWithTag(mapScript.GetCurrentPlayer().playerTag).GetComponent<Player>();     // allows dynamic player selection
        print(mapScript.GetCurrentPlayer().ToString());
        GetComponent<MeshRenderer>().enabled = true;
        transform.position = new Vector3(player.position.x, player.position.y + 3, player.position.z);
        LoadDiceMaterial();
        // TODO: play dice sound -> rip from cartrige
        InvokeRepeating("ChangeNumber", 0, 0.08f);
    }

    private void DisableDice()
    {
        diceEnabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        CancelInvoke();
    }

	private void Update () {
		if(diceEnabled)
            HoverDice();
	}

    private void LoadDiceMaterial()
    {
        string path = "Dice/Materials/";
        switch (playerScript.activeItem.itemEffect)
        {
            case Item.Effect.Dicex2:
                path += "red";
                // TODO: red dice, store in Materials/red
                break;
            case Item.Effect.Dicex3:
                path += "gold";
                // TODO: gold dice, store in Materials/gold
                break;
            case Item.Effect.DicePoison:
                path += "violet";
                // TODO: green dice, store in Materials/green
                break;
            case Item.Effect.DiceReverse:
                path += "green";
                // TODO: violet dice, store in Materials/violet
                break;
            case Item.Effect.DicePowerReverse:
                path += "special";
                // TODO: red/green dice, store in Materials/special
                break;
            default:
                path += "normal";
                break;
        }
        diceMaterials = Resources.LoadAll<Material>(path);
    }

    private void ChangeNumber()
    {
        GetComponent<MeshRenderer>().material = diceMaterials[loadOrder];
        loadOrder = loadOrder < 9 ? ++loadOrder : 0;
    }

    private void HoverDice()
    {
        angle += 0.02f;
        transform.Translate(new Vector3(0, (float)Math.Sin(angle)*0.01f, 0));
    }
}
