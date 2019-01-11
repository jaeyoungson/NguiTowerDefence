using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
using UnityEngine.U2D;

public class GameScene : Scene
{        
    private new void Awake()
    {
        m_Scene = enumScene.GameScene;
        SceneMrg.Ins.SetScene(this);
    }
}
