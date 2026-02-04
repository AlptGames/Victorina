using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public AudioMixer Mixer;
    public Slider MusicSlider;
    public float musicVolume;

    public Slider sfxSlider;
    public float sfxVolume;

    public Image musicSound;
    public Image sfxSound;

    public Sprite soundOn;
    public Sprite soundOff;
    public bool onSoundMusic;
    public bool onSoundSFX;
    // Start врн рш дчееееекющщщь
    void Start()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            if (MusicSlider != null)
            {
                MusicSlider.value = PlayerPrefs.GetFloat("Music");
            }
            Mixer.SetFloat("Music", PlayerPrefs.GetFloat("Music"));
        }
        else
        {
            MusicSlider.value = -20;
            Mixer.SetFloat("Music", -20);
        }

        if (PlayerPrefs.HasKey("SFX")) 
        {
            if (sfxSlider != null)
            {
                sfxSlider.value = PlayerPrefs.GetFloat("SFX");
            }
            Mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX"));
        }
        else
        {
            sfxSlider.value = -20;
            Mixer.SetFloat("SFX", -20);
        }
    }

    void Update()
    {
        if (MusicSlider.value == -80)
        {
            musicSound.sprite = soundOff;
            onSoundMusic = false;
        }
        else
        {
            onSoundMusic = true;
            musicSound.sprite = soundOn;
        }
        if (sfxSlider.value == -80)
        {
            sfxSound.sprite = soundOff;
            onSoundSFX = false;
        }
        else
        {
            onSoundSFX = true;
            sfxSound.sprite = soundOn;
        }
    }

    public void SetMusicVolume()
    {
        musicVolume = MusicSlider.value;
        Mixer.SetFloat("Music", musicVolume);
        PlayerPrefs.SetFloat("Music", musicVolume);
    }

    public void SetSFXVolume()
    {
        sfxVolume = sfxSlider.value;
        Mixer.SetFloat("SFX", sfxVolume);
        PlayerPrefs.SetFloat("SFX", sfxVolume);
    }

    public void OnOffSoundMusic()
    {
        if (onSoundMusic == false)
        {
            MusicSlider.value = 15;
            onSoundMusic = true;
        }
        else
        {
            MusicSlider.value = -80;
            onSoundMusic = false;
        }
    }
    public void OnOffSoundSFX()
    {
        if (onSoundSFX == false)
        {
            sfxSlider.value = 15;
            onSoundSFX= true;
        }
        else
        {
            sfxSlider.value = -80;
            onSoundSFX = false;
        }
    }
}
