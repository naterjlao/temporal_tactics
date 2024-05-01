using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerDefenseUI : MonoBehaviour
{
    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI ProgressText, BaseHealth, GoldAmount;

    [SerializeField] Transform completed, failed;

    private void Start()
    {
        completed.localScale = failed.localScale = Vector3.zero;
    }

    public void UpdateProgress(float pct)
    {
        ProgressText.text = $"{pct:F0}%";
        progressBar.fillAmount = pct / 100;

        // print($"{pct}  {progressBar.fillAmount}");
    }

    public void UpdateBaseHealth(int health)
    {
        BaseHealth.text = health.ToString();
    }
    public void UpdateGold(int amount)
    {
        GoldAmount.text = amount.ToString();
    }

    public void LevelCompleted()
    {
        completed.DOScale(1, 0.3f).SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
            Observable.Timer(System.TimeSpan.FromSeconds(5)).Subscribe(_ =>
            {
                SceneManager.LoadScene("Overworld");
            });
        });
    }
    public void LevelFailed()
    {
        failed.DOScale(1, 0.3f).SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
            Observable.Timer(System.TimeSpan.FromSeconds(5)).Subscribe(_ =>
            {
                SceneManager.LoadScene("Overworld");
            });
        });
    }
}
