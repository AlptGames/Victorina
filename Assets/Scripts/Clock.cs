using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float currentTime = 0;
    public bool isRunning = true;

    public TMP_Text endTime;
    public TMP_Text correctAnswersText;
    public TMP_Text wrongAnswersText;

    public Variant variantScript;

    private string saveKey = "SavedGameTime"; // Ключ для PlayerPrefs

    // Результат (время, за которое игрок прошел этап)
    public float finalTime { get; private set; }

    private void Start()
    {
        currentTime = PlayerPrefs.GetFloat(saveKey, 0f);
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            DisplayTime(currentTime);
        }
    }

    // Сохранение при выходе из сцены или уничтожении объекта
    private void OnDisable()
    {
        isRunning = false;
        SaveProgress();
    }

    // Сохранение при закрытии приложения (на ПК или мобилках)
    private void OnApplicationQuit()
    {
        isRunning = false;
        SaveProgress();
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetFloat(saveKey, currentTime);
        PlayerPrefs.Save(); // Принудительная запись на диск
        Debug.Log("Время сохранено: " + currentTime);
    }

    // ЭТОТ МЕТОД ВЫЗЫВАЕТСЯ ИЗ ДРУГИХ СКРИПТОВ
    public void StopClock()
    {
        if (isRunning)
        {
            isRunning = false;
            finalTime = currentTime;
            endTime.text = "Финальное время: " + FormatTime(finalTime);
            correctAnswersText.text = "Правильных ответов: " + PlayerPrefs.GetInt("RightAnswers");
            wrongAnswersText.text = "Неправильных ответов: " + PlayerPrefs.GetInt("FalseAnswers");
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        //Debug.Log(FormatTime(timeToDisplay));
    }

    // Вынесли форматирование в отдельную функцию для удобства
    string FormatTime(float time)
    {
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt((time % 3600) / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    public void ResetTimer()
    {
        currentTime = 0;
        PlayerPrefs.DeleteKey(saveKey);
    }
}