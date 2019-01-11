using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
public class Place :FsmState<GameLogic>
{
    public Place () : base (GameLogic.Place)
    {

    }

    public override void Enter()
    {
        GameMrg.Ins.isMoveTower=false;
    }
    public override void Update()
    {
        InputManager.Ins.UpdateLogic();
    }

    public override void End()
    {
        if (GameMrg.Ins.gemInstantCount < 5)
        {
            GameMrg.Ins.getLogic.SetState(GameLogic.TowerSpawn);
        }
        else
        {
            //keep tower select
        }
    }
}

