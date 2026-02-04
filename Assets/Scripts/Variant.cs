using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variant : MonoBehaviour
{
    public static int rightAnswers = 0;
    public static int falseAnswers = 0;

    public GameObject gOrK;

    public AudioSource click;

    public RectTransform levels;
    public float shiftAmount = 800f;
    public float duration = 1f;
    public AnimationCurve easingCurve; 

    private bool isMoving = false;

    void Start()
    {
        rightAnswers = PlayerPrefs.GetInt("RightAnswers");
        rightAnswers = PlayerPrefs.GetInt("RightAnswers");
    
        // «агружаем сохраненную позицию X (по умолчанию 0)
        float savedX = PlayerPrefs.GetFloat("LevelsPosX", 0);
        levels.anchoredPosition = new Vector2(savedX, levels.anchoredPosition.y);
    }

    public void RightAnswer()
    {
        if (isMoving || levels == null) return;
        click.Play();

        rightAnswers++;
        PlayerPrefs.SetInt("RightAnswers", rightAnswers);
        gOrK.SetActive(true);
        Debug.Log(rightAnswers);
        Vector2 startPos = levels.anchoredPosition;
        Vector2 targetPos = startPos + new Vector2(-shiftAmount, 0);
        StartCoroutine(SmoothMove(startPos, targetPos));
    }

    public void FalseAnswer()
    {
        if (isMoving || levels == null) return;
        click.Play();

        falseAnswers++;
        PlayerPrefs.SetInt("FalseAnswers", falseAnswers);
        Debug.Log(falseAnswers);
        Vector2 startPos = levels.anchoredPosition;
        Vector2 targetPos = startPos + new Vector2(-shiftAmount, 0);
        StartCoroutine(SmoothMove(startPos, targetPos));
    }

    IEnumerator SmoothMove(Vector2 start, Vector2 target)
    {
        isMoving = true;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // ‘ормула SmoothStep дл€ "живой" анимации
            // ќна делает движение медленным в начале и в конце, и быстрым в середине
            float curveValue = easingCurve.Evaluate(t);

            levels.anchoredPosition = Vector2.Lerp(start, target, curveValue);
            yield return null;
        }

        levels.anchoredPosition = target;

        PlayerPrefs.SetFloat("LevelsPosX", target.x);
        PlayerPrefs.Save(); 

        isMoving = false;
    }
}
