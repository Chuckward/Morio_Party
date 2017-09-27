using UnityEngine;
using UnityEngine.Events;
using XInputDotNetPure;

public class PlayerControls : MonoBehaviour {

    public UnityEvent m_PlayerSoundGood;
    public UnityEvent m_PlayerSoundEvil;

    public Player playerScript;

    public AudioSource playerSound;

    private void Awake()
    {
        m_PlayerSoundGood = new UnityEvent();
        m_PlayerSoundGood.AddListener(PlayCharacterSound);
        m_PlayerSoundEvil = new UnityEvent();
        m_PlayerSoundEvil.AddListener(PlayCharacterSound);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder.Equals(ButtonState.Pressed))
        {
            playerSound.clip = Resources.Load<AudioClip>("Audio/Characters/Morio/Morio_Huhu");
            m_PlayerSoundGood.Invoke();
        }
        if(GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder.Equals(ButtonState.Pressed))
        {
            playerSound.clip = Resources.Load<AudioClip>("Audio/Characters/Morio/Morio_OhNo");
            m_PlayerSoundEvil.Invoke();
        }
    }

    //TODO: also make dynamic for every Character
    private void PlayCharacterSound()
    {
        playerSound.Play();
    }
}
