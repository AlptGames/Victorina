using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Volume : MonoBehaviour
{
    public AudioMixer Mixer;

    public Slider MusicSlider;
    public float musicVolume;
    public TMP_Text musicSliderText;

    public Slider sfxSlider;
    public float sfxVolume;
    public TMP_Text sfxSliderText;

    public Slider masterSlider;
    public float masterVolume;
    public TMP_Text masterSliderText;

    public Image musicSound;
    public Image sfxSound;
    public Image masterSound;

    public Sprite soundOn;
    public Sprite soundOff;
    public bool onSoundMusic;
    public bool onSoundSFX;
    public bool onSoundMaster;
    // Start ◊“Œ “€ ƒﬁ≈≈≈≈≈À¿›››ÿ
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
        if (PlayerPrefs.HasKey("Master")) 
        {
            if (masterSlider != null)
            {
                masterSlider.value = PlayerPrefs.GetFloat("Master");
            }
            Mixer.SetFloat("Master", PlayerPrefs.GetFloat("Master"));
        }
        else
        {
            masterSlider.value = -20;
            Mixer.SetFloat("Master", -20);
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
        if (masterSlider.value == -80)
        {
            masterSound.sprite = soundOff;
            onSoundMaster = false;
        }
        else
        {
            onSoundMaster = true;
            masterSound.sprite = soundOn;
        }
    }

    public void SetMusicVolume()
    {
        musicVolume = MusicSlider.value;
        musicSliderText.text = "ÃÛÁ˚Í‡: " + (int)(MusicSlider.value + 80);
        Mixer.SetFloat("Music", musicVolume);
        PlayerPrefs.SetFloat("Music", musicVolume);
    }

    public void SetSFXVolume()
    {
        sfxVolume = sfxSlider.value;
        sfxSliderText.text = "«‚ÛÍÓ‚˚Â ˝ÙÙÂÍÚ˚: " + (int)(sfxSlider.value + 80);
        Mixer.SetFloat("SFX", sfxVolume);
        PlayerPrefs.SetFloat("SFX", sfxVolume);
    }

    public void SetMasterVolume()
    {
        masterVolume = masterSlider.value;
        masterSliderText.text = "Œ·˘ËÈ: " + (int)(masterSlider.value + 80);
        Mixer.SetFloat("Master", masterVolume);
        PlayerPrefs.SetFloat("Master", masterVolume);
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
            onSoundSFX = true;
        }
        else
        {
            sfxSlider.value = -80;
            onSoundSFX = false;
        }
    }

    public void OnOffSoundMaster()
    {
        if (onSoundMaster == false)
        {
            masterSlider.value = 15;
            onSoundMaster = true;
        }
        else
        {
            masterSlider.value = -80;
            onSoundMaster = false;
        }
    }
}
