using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefenseGoldManager : MonoBehaviour
{
    [SerializeField] TowerDefenseUI ui;

    [SerializeField] int additionalBaseGold = 500;

    void Awake()
    {
        playerStats.goldCount += additionalBaseGold;
        // print(playerStats.goldCount);
    }

    private void Start()
    {
        ui.UpdateGold(playerStats.goldCount);
    }

    public void UpdateGold(int amount)
    {
        playerStats.goldCount += amount;
        ui.UpdateGold(playerStats.goldCount);
    }
}
