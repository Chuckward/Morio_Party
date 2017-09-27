using UnityEngine;

public class CocoBalls : Food {

	public CocoBalls()
    {
        // probability 15%
        foodName = "Coconut Balls";
        price = 300;
        timeout = -1;
        model = Resources.Load<GameObject>("Models/Minigames/SushiGoRound/CoconutBalls");
        audio = null;
    }
}
