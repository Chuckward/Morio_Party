using System;
using UnityEngine;

public class ItemField : NavigatableField {

    private System.Random rng;

    private MapMinigameSM minigameminigameChooserScript;

    private void Awake()
    {
        rng = new System.Random();

        minigameminigameChooserScript = GameObject.Find("Map").GetComponent<MapMinigameSM>();
    }

    private void Update()
    {
        rng.Next();
    }

    public override void ApplyEffect(Player player)
    {
        soundToPlay = Resources.Load<AudioClip>("Audio/Fields/special");
        fieldSound.clip = soundToPlay;
        fieldSound.Play();
        player.color = Player.Color.Green;
        player.statistics.AddToField(Waypoint.FieldType.Item);
        player.currentPlayerState = Player.PlayerState.Reset;
        HUD_Info_Text.enabled = true;
        HUD_Info_Avatar.enabled = true;
        HUD_Info_Avatar.sprite = Resources.Load<Sprite>("Characters/Koopa_red_avatar");
        if(rng.Next(2) == 0)
        {
            // trigger question
        }
        else
            minigameminigameChooserScript.m_chooseItemgame.Invoke();
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }

	
}
