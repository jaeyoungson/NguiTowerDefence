using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoseUI : MonoBehaviour
{
    public void ReStart()
    {
        GameMrg.Ins.PlayerMoneySave();
        GameMrg.Ins.GameMrgReSet();
        SceneMrg.Ins.ChangeScene(Global_Define.enumScene.GameScene);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
