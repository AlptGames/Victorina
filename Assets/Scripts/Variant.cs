using System.Collections;
using UnityEngine;
using TMPro;

public class Variant : MonoBehaviour
{
    public static int rightAnswers = 0;
    public static int falseAnswers = 0;

    public static int levelsCounting;
    public TMP_Text levelsCount;

    public GameObject gOrK;
    public AudioSource click;

    public RectTransform levels;
    [Header("Настройки времени")]
    public float waitBeforeMove = 0.5f; // Сколько gOrK висит перед движением
    public float duration = 1f;         // Длительность самого движения
    
    public AnimationCurve easingCurve; 

    private bool isMoving = false;

    void Start()
    {
        // Подгружаем статистику (второй аргумент 0 — значение по умолчанию)
        rightAnswers = PlayerPrefs.GetInt("RightAnswers", 0);
        falseAnswers = PlayerPrefs.GetInt("FalseAnswers", 0);
        levelsCounting = PlayerPrefs.GetInt("LevelsCount", 0);
        levelsCount.text = levelsCounting + "/100";
    
        float savedX = PlayerPrefs.GetFloat("LevelsPosX", 0);
        levels.anchoredPosition = new Vector2(savedX, levels.anchoredPosition.y);
    }

    public void RightAnswer()
    {
        if (isMoving) return;
        rightAnswers++;
        levelsCounting++;
        PlayerPrefs.SetInt("RightAnswers", rightAnswers);
        PlayerPrefs.SetInt("LevelsCount", levelsCounting);
        levelsCount.text = levelsCounting + "/100";
        StartCoroutine(ProcessAnswer());
    }

    public void FalseAnswer()
    {
        if (isMoving) return;
        falseAnswers++;
        levelsCounting++;
        PlayerPrefs.SetInt("LevelsCount", levelsCounting);
        levelsCount.text = levelsCounting + "/100";
        StartCoroutine(ProcessAnswer());
    }

    IEnumerator ProcessAnswer()
    {
        isMoving = true;
        click.Play();

        // 1. Сразу активируем gOrK (до ожидания и до движения)
        if (gOrK != null) gOrK.SetActive(true);

        // 2. Ждем заданное время, пока игрок видит gOrK
        yield return new WaitForSeconds(waitBeforeMove);

        // 3. Вычисляем ширину экрана и целевую позицию
        float screenWidth = levels.parent.GetComponent<RectTransform>().rect.width;
        Vector2 startPos = levels.anchoredPosition;
        Vector2 targetPos = startPos + new Vector2(-screenWidth, 0);

        // 4. Плавное движение
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

        // 5. Сохранение позиции
        PlayerPrefs.SetFloat("LevelsPosX", targetPos.x);
        PlayerPrefs.Save(); 

        isMoving = false;
        
        // Опционально: можно выключать gOrK здесь, если он должен исчезнуть после движения
        // gOrK.SetActive(false);
    }
}