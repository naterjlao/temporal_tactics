using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCount : MonoBehaviour
{
    private int lastGoldCount;
    private TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        lastGoldCount = playerStats.goldCount;
        textMeshProUGUI.text = lastGoldCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastGoldCount != playerStats.goldCount)
        {
            StartCoroutine(update_counter(lastGoldCount, playerStats.goldCount));
            lastGoldCount = playerStats.goldCount;
        }
    }

    IEnumerator update_counter(int oldCount, int newCount)
    {
        int delta = (oldCount < newCount) ? 1 : -1;
        int count = oldCount;
        do
        {
            textMeshProUGUI.text = count.ToString();
            count += delta;
            yield return new WaitForSeconds(0.005f);
        } while (count != newCount);
        textMeshProUGUI.text = count.ToString();
    }
}
