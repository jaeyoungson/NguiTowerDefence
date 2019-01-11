using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class Ready : FsmState<GameLogic>
{
    public Ready () : base (GameLogic.Ready)      
    {

    }

    public override void Enter()
    {
        GameSceneUI.Ins.RoundReSet();
        GameSceneUI.Ins.uiPlace.Close();
    }

    public override void Update()
    {
        if (TableDownloader.Ins.tableLoadSuccess)
        {
            Time.timeScale = 1.0f;
            GameMrg.Ins.getLogic.SetState(GameLogic.TowerSpawn);
        }
    }


    public override void End()
    {
        GameMrg.Ins.SetSpawnRandom();
        GameSceneUI.Ins.LifeReset();
    }
}
