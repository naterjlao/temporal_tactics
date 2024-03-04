using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using NaughtyAttributes;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _appearTime = 1;
    [SerializeField] SplineFollower _splineFollower;
    [SerializeField][Expandable] EnemyData Data;

    public float PathPosition => (float)_splineFollower.GetPercent();

    Tween enemyTweenerScale, enemyTweenerSpeed;

    void Start()
    {
        name = Data.name;

        _splineFollower.followSpeed = 0;
        transform.localScale = Vector3.zero;

        ScaleEnemy(true, false, () =>
        {
            TweenSpeed(true);
        });
    }

    public void SetSplineComputer(SplineComputer computer)
    {
        _splineFollower.spline = computer;
    }

    void ScaleEnemy(bool state, bool imm = false, System.Action OnComplete = null)
    {
        enemyTweenerScale?.Kill();
        enemyTweenerScale = transform.DOScale(state ? 1 : 0, imm ? 0 : _appearTime)
                                .SetEase(Ease.OutQuad)
                                .OnComplete(() => OnComplete?.Invoke());
    }

    public void ScaleOut()
    {
        _splineFollower.enabled = false;
        ScaleEnemy(false, false, () =>
        {
            Destroy(gameObject);
        });
    }

    // Tween a float called myFloat to 52 in 1 second
    // DOTween.To(()=> myFloat, x=> myFloat = x, 52, 1);
    void TweenSpeed(bool state)
    {
        enemyTweenerSpeed?.Kill();
        enemyTweenerSpeed = DOTween.To(() => _splineFollower.followSpeed,
                                        x => _splineFollower.followSpeed = x,
                                        state ? Data.Speed : 0, _appearTime);
    }
}