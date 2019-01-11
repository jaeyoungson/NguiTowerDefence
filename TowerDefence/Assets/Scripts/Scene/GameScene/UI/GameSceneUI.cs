using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Global_Define;
using System;

public class GameSceneUI : SingletonBase<GameSceneUI>
{
    public UILabel centerUiLabel;
    public UIPlace uiPlace;
    public TopUI topUI;
    public ConsumeUI consumeUI;
    public TowerInfoUI infoUI;
    public Camera uiCamera;
    public Animation ani;
    public GameLoseUI gameLoseUI;

    public override void InitBeforeAwake()
    {
        base.InitBeforeAwake();
    }
    public override void InitAftwerAwake()
    {
        base.InitAftwerAwake();
    }
    public void BuildCannot()
    {
        centerUiLabel.gameObject.SetActive(true);
        centerUiLabel.text = "I can't build there";
        StartCoroutine(GameObjectWaitSetFalse(centerUiLabel.gameObject));
    }
    public void OverSummonCount()
    {
        centerUiLabel.gameObject.SetActive(true);
        centerUiLabel.text = "Summon Count Over";
        StartCoroutine(GameObjectWaitSetFalse(centerUiLabel.gameObject,0.5f));
    }

    public void SelectTower()
    {
        centerUiLabel.gameObject.SetActive(true);
        centerUiLabel.text = "Select Tower Plz";
        StartCoroutine(GameObjectWaitSetFalse(centerUiLabel.gameObject, 0.5f));
    }

    public IEnumerator GameObjectWaitSetFalse(GameObject target,float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        target.gameObject.SetActive(false);
    }

    public IEnumerator GameObjectWaitSetFalse(GameObject target)
    {
        yield return new WaitForSeconds(0.5f);
        target.SetActive(false);
    }
    
    public void CombineButton()
    {
        GameMrg.Ins.Combine();
    }
    
    public void KeepButton()
    {
        GameMrg.Ins.Keep();        
    }

    public void LifeReset()
    {
        topUI.lifeUI.text = "X" + GameMrg.Ins.life;
    }
    public void RoundReSet()
    {
        topUI.roundUI.text = GameMrg.Ins.currentRound.ToString();
    }

    public void CenterLabelOnWaitOff()
    {
        centerUiLabel.gameObject.SetActive(true);
        StartCoroutine(GameObjectWaitSetFalse(centerUiLabel.gameObject));
    }

}
