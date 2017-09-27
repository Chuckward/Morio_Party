using UnityEngine;

public class Morio : Character {

	public Morio() { 
        characterName = "Morio";
        characterModel = Resources.Load<GameObject>("Characters/Morio");

        soundGood = Resources.Load<AudioClip>("Audio/Characters/Morio/Morio_Huhu");
        soundBad = Resources.Load<AudioClip>("Audio/Characters/Morio/Morio_OhNo");
    }
}
