using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class TowerSpawn : FsmState<GameLogic>
{
    public TowerSpawn (): base(GameLogic.TowerSpawn)
    {

    }

    public override void Enter()
    {
        GameSceneUI.Ins.uiPlace.Open();
        if(GameSceneUI.Ins.uiPlace.newGemButton.gameObject.activeSelf==false)
        {
            GameSceneUI.Ins.uiPlace.newGemButton.gameObject.SetActive(true);
        }       
    }
    public override void Update()
    {
        if(GameMrg.Ins.isMoveTower)
        {
            GameMrg.Ins.getLogic.SetState(GameLogic.Place);
        }
    }
    public override void End()
    {
    }
}
