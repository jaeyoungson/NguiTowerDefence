using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class SelectScene : Scene {

    private void Awake()
    {
        m_Scene = enumScene.SelectScene;
        SceneMrg.Ins.SetScene(this);
    }
    
    void Start () {

        UIMrg.Ins.UIMrgSelectSceneSet();
    }

}
