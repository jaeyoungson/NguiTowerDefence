using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class TowerClick : MonoBehaviour
{
    private Tower onCilckTower;

    public void SetOnClickTower()
    {
        onCilckTower = gameObject.GetComponent<Tower>();

        GameMrg.Ins.selectTower = onCilckTower;
    }

    private void OnMouseDown()
    {
        if (GameMrg.Ins.getLogic.getCurStateType == GameLogic.Keep||GameMrg.Ins.getLogic.getCurStateType == GameLogic.Battle)
        {
            SetOnClickTower();
            if (GameSceneUI.Ins.infoUI.isOpen == false)
            {
                GameSceneUI.Ins.infoUI.OpenUI();
            }
            else
            {
                GameSceneUI.Ins.infoUI.SetInfoUI();
            }
            if (GameMrg.Ins.selectTower.eId < 30)
            {
                GameSceneUI.Ins.uiPlace.combineButton.gameObject.SetActive(GameMrg.Ins.isCombine(onCilckTower));
            }
        }
    }
}

