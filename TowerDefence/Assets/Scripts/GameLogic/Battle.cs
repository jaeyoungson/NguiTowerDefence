using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
public class Battle : FsmState<GameLogic>
{
    bool isBattleEnd = false;
    public Battle ():base(GameLogic.Battle)
    {

    }
    public override void Update()
    {
        if(GameMrg.Ins.monsterCheck==30&& isBattleEnd==false)
        {
            GameMrg.Ins.getLogic.SetState(GameLogic.TowerSpawn);
        }
        if(GameMrg.Ins.life<=0)
        {
            GameMrg.Ins.getLogic.SetState(GameLogic.Lose);
        }
    }
    public override void End()
    {
        Debug.Log("battle log " + GameMrg.Ins.currentRound.ToString());
        if(GameMrg.Ins.currentRound>20)
        {
            GameMrg.Ins.getLogic.SetState(GameLogic.Victory);
        }
        else
        {            
            GameMrg.Ins.monsterList.Clear();
            BuildTurn();  
        }
    }

    public void BuildTurn()
    {
        GameMrg.Ins.monsterCheck = 0;
        GameMrg.Ins.gemInstantCount = 0;
        GameMrg.Ins.currentRound++;
        GameSceneUI.Ins.RoundReSet();
        GameSceneUI.Ins.uiPlace.ItemSpriteReset();
        for (int i = 0; i < GameMrg.Ins.createTower.Count; i++)
        {
            GameMrg.Ins.createTower[i] = null;
        }

    }

}
