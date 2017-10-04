using UnityEngine;
using XInputDotNetPure;

public class Star : NavigatableField {

    private Player player;
    private StarStates currentState;

    private MapLogic mapScript;

    private enum StarStates
    {
        Inactive,
        DisplayText,
        ConfirmA,
        StarAnimation,
        TriggerStarRelocation
    }

    public override void ApplyEffect(Player player)
    {
        // one cannot land directly on star!
    }

    public override void PassBy(Player player)
    {
        this.player = player;
        player.currentPlayerState = Player.PlayerState.Stop;
        soundToPlay = Resources.Load<AudioClip>("Audio/Fields/starField");
        fieldSound.clip = soundToPlay;
        fieldSound.Play();
        player.statistics.AddToField(Waypoint.FieldType.Star);
        HUD_Info_Text.enabled = true;
        HUD_Info_Avatar.enabled = true;
        currentState = StarStates.DisplayText;
    }

    private void Awake()
    {
        mapScript = GameObject.Find("Map").GetComponent<MapLogic>();
    }

    private void Update()
    {
        switch(currentState)
        {
            case StarStates.Inactive:
                break;
            case StarStates.DisplayText:
                HUD_Info_Avatar.sprite = Resources.Load<Sprite>("Characters/Toad_avatar");
                if (player.GetPlayerCoins() > 20)
                    HUD_Info_Text.text = "You gonna buy a star, right? No answer needed, here take it!";
                else
                {
                    HUD_Info_Text.text = "Oh no, you haven't got even 20 coins? Come back later with enough money!";
                    player.statistics.AddUnlucky(PlayerStatistics.Unluckies.Star_notenoughcoins);
                }
                currentState = StarStates.ConfirmA;
                break;
            case StarStates.ConfirmA:
                if(GamePad.GetState(mapScript.GetCurrentPlayer().index).Buttons.A.Equals(ButtonState.Pressed))
                {
                    HUD_Info_Text.enabled = false;
                    HUD_Info_Avatar.enabled = false;
                    currentState = StarStates.StarAnimation;
                }
                break;
            case StarStates.StarAnimation:
                currentState = StarStates.TriggerStarRelocation;
                break;
            case StarStates.TriggerStarRelocation:
                player.currentPlayerState = Player.PlayerState.Move;
                currentState = StarStates.Inactive;
                break;
        }
    }
}
