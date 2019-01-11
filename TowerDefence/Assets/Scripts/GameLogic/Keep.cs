using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class Keep : FsmState<GameLogic>
{
    public Keep():base (GameLogic.Keep)
    {

    }

    public override void End()
    {
        
    }
}
