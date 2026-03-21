using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject settings;
	public GameObject menu;
	public bool isSettings = true;
	public AudioSource click;
	public Clock clock;

    public void ToStart()
	{
		SceneManager.LoadScene("Questions");
		click.Play();
	}

	public void Restart()
	{
		PlayerPrefs.DeleteAll();
		clock.ResetTimer();
		SceneManager.LoadScene("Questions");
	}

	public void ToSettings()
	{
		menu.SetActive(false);
        settings.SetActive(true);
		click.Play();
	}

	public void BackSettings()
	{
		menu.SetActive(true);
        settings.SetActive(false);
		click.Play();
	}

	public void ToMenu()
	{
		menu.SetActive(true);
        settings.SetActive(false);
		click.Play();
	}

	public void Exit()
    {
        Application.Quit();
    }
}
