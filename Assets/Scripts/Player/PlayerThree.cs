using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThree : PlayerObject {

    public PlayerThree()
    {
        playerTag = "PlayerThree";
        avatarTag = "Avatar_third";
        index = XInputDotNetPure.PlayerIndex.One;
    }
}
