using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
using UnityEngine.SceneManagement;

public class StartScene : Scene {

    private new void Awake()
    {
        m_Scene = enumScene.StartScene;
        SceneMrg.Ins.SetScene(this);
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.anyKey || Input.GetButtonDown("Fire1"))
        {
            SceneMrg.Ins.ChangeScene(enumScene.SelectScene);
        }
    }


}
