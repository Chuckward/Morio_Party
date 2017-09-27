using UnityEngine;

public class Luigi : Character {

    public Luigi()
    {
        characterName = "Luigi";
        characterModel = Resources.Load<GameObject>("Characters/Luigi");

        soundGood = Resources.Load<AudioClip>("Audio/Characters/Luigi/Luigi_Yahoo");
        soundBad = Resources.Load<AudioClip>("Audio/Characters/Luigi/Luigi_Hahohu");
    }
}
