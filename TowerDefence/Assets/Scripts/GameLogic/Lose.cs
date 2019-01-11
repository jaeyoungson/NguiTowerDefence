using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class Lose : FsmState<GameLogic>
{
    public Lose() : base(GameLogic.Lose)
    {
    }

    public override void Enter()
    {
        Time.timeScale = 0;
        GameSceneUI.Ins.gameLoseUI.gameObject.SetActive(true);
    }

    public override void End()
    {

    }

}
