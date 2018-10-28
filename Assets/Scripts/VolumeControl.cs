using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour {

    static VolumeControl vc;
    public AudioSource source;
    public Image muteButton;
    public Sprite mute;
    public Sprite muteActive;
    private float maxVolume = 1;
    private float minVolume = 0;
    private bool mutedOrNot = false;

    private void Awake()
    {
        if (vc == null)
        {
            vc = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start () {
        if(PlayerPrefs.GetString("mute")==null || PlayerPrefs.GetString("mute") == "false")
        {
            Unmuted();
        }
        else if(PlayerPrefs.GetString("mute") == "true")
        {
            Muted();
        }
    }

    private void Muted()
    {
        source.volume = minVolume;
        muteButton.sprite = muteActive;
        mutedOrNot = true;
        PlayerPrefs.SetString("mute", "true");
    }

    private void Unmuted()
    {
        source.volume = maxVolume;
        muteButton.sprite = mute;
        mutedOrNot = false;
        PlayerPrefs.SetString("mute", "false");
    }

    public void MuteButtonClick()
    {
        if (mutedOrNot)
        {
            Unmuted();
        }
        else
        {
            Muted();
        }
    }

}
