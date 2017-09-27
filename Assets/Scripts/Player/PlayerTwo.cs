using UnityEngine;

public class PlayerTwo : PlayerObject {

    public PlayerTwo()
    {
        playerTag = "PlayerTwo";
        avatarTag = "Avatar_second";
        index = XInputDotNetPure.PlayerIndex.Two;
    }
}
