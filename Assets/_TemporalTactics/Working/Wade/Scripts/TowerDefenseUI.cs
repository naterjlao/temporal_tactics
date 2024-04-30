using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TowerDefenseUI : MonoBehaviour
{
    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI ProgressText, BaseHealth, WalletHealth;

    public void UpdateProgress(float pct)
    {
        ProgressText.text = $"{pct:F0}%";
        progressBar.fillAmount = pct / 100;

        print($"{pct}  {progressBar.fillAmount}");
    }

    public void UpdateBaseHealth(int health)
    {
        BaseHealth.text = Mathf.Min(0, health).ToString();
    }
}
