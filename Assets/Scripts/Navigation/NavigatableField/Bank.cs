using UnityEngine;
using XInputDotNetPure;

public class Bank : NavigatableField {

    private enum BankState
    {
        Inactive,
        Landed,
        PassBy,
        ConfirmA,
        ChangeCoins
    }

    private BankState bankState;
    private BankState firstState;
    private int bankAccount;
    private int coinsToChange;
    private Player currentPlayer;

    private void Awake()
    {
        bankState = BankState.Inactive;
        bankAccount = 0;
    }

    public override void ApplyEffect(Player player)
    {
        player.currentPlayerState = Player.PlayerState.Stop;
        bankState = BankState.Landed;
        firstState = bankState;
        currentPlayer = player;
        player.color = Player.Color.Green;
        player.statistics.AddToField(Waypoint.FieldType.Bank);
    }

    public override void PassBy(Player player)
    {
        player.currentPlayerState = Player.PlayerState.Stop;
        currentPlayer = player;
        bankState = BankState.PassBy;
        firstState = bankState;
    }

    private void Update()
    {
        switch(bankState)
        {
            case BankState.Landed:
                soundToPlay = Resources.Load<AudioClip>("Audio/Fields/bank_land");
                fieldSound.clip = soundToPlay;
                fieldSound.Play();
                HUD_Info.enabled = true;
                HUD_Info_Text.enabled = true;
                HUD_Info_Avatar.enabled = true;
                if (bankAccount == 0)
                {
                    HUD_Info_Text.text = "Welcome to Loopa Bank!\n Your account is ready to withdraw... \n unfortunately we are in a deep bank crisis and " +
                        "cannot give you any money. Feel free to visit us another time!";
                    HUD_Info_Avatar.sprite = Resources.Load<Sprite>("Characters/Koopa_green_avatar");
                    currentPlayer.statistics.AddUnlucky(PlayerStatistics.Unluckies.Bank_broke);
                    coinsToChange = 0;
                }
                else
                {
                    HUD_Info_Text.text = "Welcome to Loopa Bank!\n Your account is ready to withdraw, please visit us again!";
                    coinsToChange = bankAccount;
                    bankAccount = 0;
                }
                bankState = BankState.ConfirmA;
                break;
            case BankState.PassBy:
                soundToPlay = Resources.Load<AudioClip>("Audio/Fields/bank_pass");
                fieldSound.clip = soundToPlay;
                fieldSound.Play();
                HUD_Info.enabled = true;
                HUD_Info_Text.enabled = true;
                HUD_Info_Text.text = "Welcome to Loopa Bank!\n Please deposit some coins for us to waste on fonds. Thank you!";
                coinsToChange = -5;
                bankState = BankState.ConfirmA;
                break;
            case BankState.ConfirmA:
                if(GamePad.GetState(PlayerIndex.One).Buttons.A.Equals(ButtonState.Pressed))
                {
                    bankState = BankState.ChangeCoins;
                }
                break;
            case BankState.ChangeCoins:
                currentPlayer.ChangeCoins(coinsToChange);
                if (coinsToChange > 0)
                {
                    // character happy
                }
                HUD_Info.enabled = false;
                HUD_Info_Text.enabled = false;
                bankState = BankState.Inactive;
                if(firstState == BankState.Landed)
                    currentPlayer.currentPlayerState = Player.PlayerState.Reset;
                else if(firstState == BankState.PassBy)
                    currentPlayer.currentPlayerState = Player.PlayerState.Move;
                break;
            default:
                break;
        }
    }
}
