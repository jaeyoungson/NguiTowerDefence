using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    public GameObject warning;
    public UIEventTrigger warningYes;
    public void WarningOpen()
    {
        warning.SetActive(true);
    }

    public void WarningClose()
    {
        warning.SetActive(false);
    }

    public void ToTitleWarningYes()
    {
        GameMrg.Ins.PlayerMoneySave();
        SceneMrg.Ins.ChangeScene(Global_Define.enumScene.SelectScene);
    }

    public void RestartWarningYes()
    {
        GameMrg.Ins.PlayerMoneySave();
        GameMrg.Ins.GameMrgReSet();
        SceneMrg.Ins.ChangeScene(Global_Define.enumScene.GameScene);
    }
    public void Closed()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void WarningYesFuctionChange(bool toTitle)
    {
        if(toTitle == true)
        {
            warningYes.onClick.Clear();
            warningYes.onClick.Add(new EventDelegate(ToTitleWarningYes));
        }
        else
        {
            warningYes.onClick.Clear();
            warningYes.onClick.Add(new EventDelegate(RestartWarningYes));
        }

    }
    public void ToTitle()
    {
        warning.SetActive(true);
        WarningYesFuctionChange(true);
    }

    public void Restart()
    {
        warning.SetActive(true);
        WarningYesFuctionChange(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Save()
    {

    }
}
