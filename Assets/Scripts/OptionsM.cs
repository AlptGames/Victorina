using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsM : MonoBehaviour
{
    public Animator bttnAmnimator;
    public Animator bttn2Amnimator;

    public Button toHome;
    public Button settings;
    public Button exit;

    private bool isOptions = true;
    private bool isSettings = true;

    public GameObject levels;
    public GameObject settingsMenu;

    public void Options()
    {
        bttnAmnimator.SetTrigger("popa");
        bttnAmnimator.SetBool("isOpen", !bttnAmnimator.GetBool("isOpen"));
        bttn2Amnimator.SetTrigger("popa2");
        bttn2Amnimator.SetBool("isOpen2", !bttn2Amnimator.GetBool("isOpen2"));
        if (isOptions == true)
        {
            toHome.interactable = true;
            settings.interactable = true;
        }
        else 
        {
            toHome.interactable = false;
            settings.interactable = false;
        }
    }

    public void ToHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Settings()
    {
        if (isSettings == true)
        {
            levels.SetActive(false);
            settingsMenu.SetActive(true);
            isSettings = false;
        }
        else
        {
            levels.SetActive(true);
            settingsMenu.SetActive(false);
            isSettings = true;
        }
    }
}
