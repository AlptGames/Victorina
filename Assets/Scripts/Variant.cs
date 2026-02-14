using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Variant : MonoBehaviour
{
    public static int rightAnswers = 0;
    public static int falseAnswers = 0;
    public static int levelsCounting = 1;
    public TMP_Text levelsCount;

    public AudioSource click;
    public RectTransform levels;

    [Header("Настройки цветов")]
    public Color correctColor = new Color(0.5f, 1f, 0.5f); // Светло-зеленый
    public Color wrongColor = new Color(1f, 0.5f, 0.5f);   // Светло-красный

    [Header("Настройки времени")]
    public float waitBeforeMove = 0.8f;
    public float duration = 1f;
    public AnimationCurve easingCurve;

    private bool isMoving = false;

    void Start()
    {
        rightAnswers = PlayerPrefs.GetInt("RightAnswers", 0);
        falseAnswers = PlayerPrefs.GetInt("FalseAnswers", 0);
        levelsCounting = PlayerPrefs.GetInt("LevelsCount", 1);
        levelsCount.text = levelsCounting + "/100";

        float savedX = PlayerPrefs.GetFloat("LevelsPosX", 0);
        levels.anchoredPosition = new Vector2(savedX, levels.anchoredPosition.y);
    }

    // Метод для кнопок. В инспекторе выберите тип 'Dynamic Button' или просто перетащите кнопку в поле
    public void OnAnswerClick(Button clickedButton)
    {
        if (isMoving) return;

        // 1. Определяем, правильно ли ответил игрок по тегу нажатой кнопки
        bool isCorrect = clickedButton.CompareTag("Correct");

        if (isCorrect) rightAnswers++;
        else falseAnswers++;

        levelsCounting++;
        UpdateUIAndStats();

        // 2. Находим ВСЕ кнопки в текущем вопросе
        // Мы берем родителя нажатой кнопки, чтобы найти её "соседей"
        Button[] siblingButtons = clickedButton.transform.parent.GetComponentsInChildren<Button>();

        foreach (Button btn in siblingButtons)
        {
            Image img = btn.GetComponent<Image>();
            if (img != null)
            {
                // Красим в зависимости от тега каждой кнопки
                if (btn.CompareTag("Correct"))
                    img.color = correctColor;
                else
                    img.color = wrongColor;
            }
            // Выключаем возможность нажатия, чтобы не кликать во время анимации
            btn.interactable = false;
        }

        StartCoroutine(ProcessAnswer());
    }

    void UpdateUIAndStats()
    {
        PlayerPrefs.SetInt("RightAnswers", rightAnswers);
        PlayerPrefs.SetInt("FalseAnswers", falseAnswers);
        PlayerPrefs.SetInt("LevelsCount", levelsCounting);
        levelsCount.text = levelsCounting + "/100";
    }

    IEnumerator ProcessAnswer()
    {
        isMoving = true;
        if (click != null) click.Play();

        yield return new WaitForSeconds(waitBeforeMove);

        float screenWidth = levels.parent.GetComponent<RectTransform>().rect.width;
        Vector2 startPos = levels.anchoredPosition;
        Vector2 targetPos = startPos + new Vector2(-screenWidth, 0);

        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float curveValue = easingCurve.Evaluate(t);
            levels.anchoredPosition = Vector2.Lerp(startPos, targetPos, curveValue);
            yield return null;
        }

        levels.anchoredPosition = targetPos;
        PlayerPrefs.SetFloat("LevelsPosX", targetPos.x);
        PlayerPrefs.Save();
        isMoving = false;
    }
}