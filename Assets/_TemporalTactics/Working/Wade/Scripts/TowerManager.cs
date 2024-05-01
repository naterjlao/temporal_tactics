using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    [SerializeField] GridSelectionManager selectionManager;
    [SerializeField] TowerDefenseGoldManager goldManager;
    [SerializeField] RectTransform towerUI;
    [SerializeField] string layerName = "BuiltTower";
    [SerializeField] AudioClip buildSound;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI rateOfFireText, RadiusText, costText, damageText;

    public Tower defaultTower, selectedTower;
    public AudioSource audioSource;

    [SerializeField] GameObject selectedTile;

    Tween towerUiTween;
    void Start()
    {
        if (!selectionManager) FindObjectOfType<GridSelectionManager>();

        selectionManager.OnGridSelectedGO.AddListener(OpenTowerUI);

        SelectTower(defaultTower);
        TweenTowerUI(false, true);
    }

    public void OpenTowerUI(GameObject selection)
    {
        selectedTile = selection;
        TweenTowerUI(true);
    }
    public void CloseTowerUI()
    {
        TweenTowerUI(false);
    }

    public void TweenTowerUI(bool state, bool imm = false)
    {
        towerUiTween?.Kill();
        towerUiTween = towerUI.DOScale(state ? 0.9f : 0, imm ? 0 : (state ? 0.3f : 0.15f)).SetEase(Ease.OutQuad);
    }

    public void BuildTower()
    {
        if (selectedTower.Data.Cost > playerStats.goldCount) return;

        goldManager.UpdateGold(-(int)selectedTower.Data.Cost);

        // selectedTile = selectionManager.GetSelectedGameObject();
        var gridTile = selectedTile.GetComponentInParent<GridTile>();
        gridTile.SetTile(TileType.DirtHigh);

        var tower = Instantiate(selectedTower, gridTile.transform);
        tower.transform.SetLocalPositionAndRotation(Vector3.zero, quaternion.identity);

        tower.transform.localScale = Vector3.zero;

        TweenTowerUI(false);

        tower.transform.DOScale(1, 0.3f).SetEase(Ease.OutQuad).SetDelay(0.5f);

        // play tower build sound
        audioSource.PlayOneShot(buildSound);

        selectedTile.layer = gridTile.gameObject.layer = LayerMask.NameToLayer(layerName);
    }
    public void SelectTower(Tower tower)
    {
        selectedTower = tower;

        icon.sprite = tower.Data.icon;
        titleText.text = tower.Data.name;
        rateOfFireText.text = tower.Data.RateOfFire.ToString();
        costText.text = tower.Data.Cost.ToString();
        damageText.text = tower.AmmoPrefab.Data.Damage.ToString();
    }
}
