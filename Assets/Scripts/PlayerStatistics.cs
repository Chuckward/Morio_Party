using System.Collections.Generic;

public class PlayerStatistics {

    /** Field Statistics **/
    private int landed_blue;
    private int landed_red;
    private int landed_bank;
    private int landed_event;
    private int landed_chance;
    private int landed_bowser;
    private int landed_wtf;
    private int landed_battle;
    private int visited_ghost;

    /** Movement Statistics **/
    private int fields_moved;
    private int rights_taken;
    private int lefts_taken;
    private int straight_taken; // only split3 and reverse join3

    /** Common Statistics **/
    private int items_used;
    private int items_bought;
    private int items_stolen;
    private int items_lost;     // items lost to stealing attempts from other players
    private int items_revenue;  // ratio between item value and item cost, ideally 100% or higher
    private int star_attempts;
    private int coins_max;
    private int coins_won_mg;   // minigames coins won
    private int coins_stolen;
    private int coins_lost;     // coins lost to stealing attempts from other players
    private int times_broke;    // player has 0 coins in rounds
    private float average_dice_rolled;    // ratio rolled to rounds

    private int unlucky;    
    
    public enum Unluckies
    {
        Bank_broke,
        Ghost_unprofitable,
        Ghost_notenoughcoins,
        Star_notenoughcoins,
        Star_acatsleap,
        Coins_lost50,
        Map_Manson_fakedoor,
        Item_Dice_worstcase,
        Bowser_robbedme,
        Dice_avgbelow5,
        ItemShop_closed,

    }
    /** unlucky is a bit complex. Counts following: 
     * - banks landed, but bank was broke 
     * - steals with boo less than 5 coins
     * - got to star but not enough coins
     * - item shop closed (for day/nighttime maps)
     * - lost star 1 field before reaching it to other player
     * - lost more than 50 coins in one go (item buyings not counted!)
     * - average_dice_rolled below 5 (only granted after game ends 
     * - fake doors opened (The manson)
     * - rolled worst case dice with shrooms (1-2 red, 1-1-2 gold, 1 reverse)
     * - robbed by Bowser (stackable to loosing more than 50 coins)
     * - visited ghost, but not enough money (less than 5 coins) **/

    private Dictionary<Unluckies, int> unluckyTable = new Dictionary<Unluckies, int>();


    void Awake()
    {
        landed_blue = 0;
        landed_red = 0;
        landed_bank = 0;
        landed_event = 0;
        landed_wtf = 0;
        landed_bowser = 0;
        landed_chance = 0;
        landed_battle = 0;
        unluckyTable.Add(Unluckies.Bank_broke, 5);
        unluckyTable.Add(Unluckies.Bowser_robbedme, 10);
        unluckyTable.Add(Unluckies.Coins_lost50, 20);
        unluckyTable.Add(Unluckies.Dice_avgbelow5, 10);
        unluckyTable.Add(Unluckies.Ghost_notenoughcoins, 15);
        unluckyTable.Add(Unluckies.Ghost_unprofitable, 3);
        unluckyTable.Add(Unluckies.ItemShop_closed, 5);
        unluckyTable.Add(Unluckies.Item_Dice_worstcase, 50);
        unluckyTable.Add(Unluckies.Map_Manson_fakedoor, 15);
        unluckyTable.Add(Unluckies.Star_acatsleap, 30);
        unluckyTable.Add(Unluckies.Star_notenoughcoins, 10);
        //unluckyTable.Add(Unluckies.)
    }

    public void Advance()
    {
        fields_moved++;
    }
	
    public void DiceRolled(int dice, int round)
    {
        average_dice_rolled += dice / round;
    }

    public void AddToField(Waypoint.FieldType field)
    {
        switch(field)
        {
            case Waypoint.FieldType.Blue:
                landed_blue++;
                break;
            case Waypoint.FieldType.Red:
                landed_red++;
                break;
            case Waypoint.FieldType.Evnt:
                landed_event++;
                break;
            case Waypoint.FieldType.Bank:
                landed_bank++;
                break;
            case Waypoint.FieldType.Battle:
                landed_battle++;
                break;
            case Waypoint.FieldType.Bowser:
                landed_bowser++;
                break;
            case Waypoint.FieldType.Chance:
                landed_chance++;
                break;
            case Waypoint.FieldType.Boo:
                visited_ghost++;
                break;
            case Waypoint.FieldType.Wtf:
                landed_wtf++;
                break;
            case Waypoint.FieldType.Star:
                star_attempts++;
                break;
        }
    }

    public void AddUnlucky(Unluckies key)
    {
        int value;
        unluckyTable.TryGetValue(key, out value);
        unlucky += value;
    }
}
