using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
public class DamageUI : MonoBehaviour {
    public UILabel damageLabel;
    public TweenPosition positionTween;
    public TweenScale scaleTween;

    public void TweenStart()
    {
        positionTween.PlayForward();
        scaleTween.PlayForward();
    }

    public void WaitDelete()
    {
        StartCoroutine(TweeningEnd());
    }

    private IEnumerator TweeningEnd()
    {
        yield return new WaitForSeconds(positionTween.duration);
        PoolManager.Ins.minipools[PoolType.Damage].Enqueue(gameObject);
        gameObject.SetActive(false);
    }
}
