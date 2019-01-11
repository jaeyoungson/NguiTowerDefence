using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeUI : MonoBehaviour
{
    public List<GameObject> skillButton;
    public GameObject levelUp;
    public UILabel info;
    public GameObject consumePanel;
    public UILabel money;

    private const int gemMaxLevel = 6;

    public void Start()
    {
        MoneyUISet();
    }

    public void ClosedUI()
    {
        TweenPosition tween = consumePanel.GetComponent<TweenPosition>();
        tween.PlayReverse();
    }

    public void OpenUI()
    {
        TweenPosition tween = consumePanel.GetComponent<TweenPosition>();
        tween.PlayForward();
    }

    public void LevelUp()
    {
        if(GameMrg.Ins.currentGemUpgrade>= gemMaxLevel)
        {
            GameSceneUI.Ins.centerUiLabel.text = "Your UpgradeLevel Max";
            GameSceneUI.Ins.centerUiLabel.gameObject.SetActive(true);
            StartCoroutine(GameSceneUI.Ins.GameObjectWaitSetFalse(GameSceneUI.Ins.centerUiLabel.gameObject, 0.5f));
        }
        else
        {
            if (GameMrg.Ins.money > ExtensionMethod.GetSpawnTowerTb(GameMrg.Ins.currentGemUpgrade).costGold)
            {
                GameMrg.Ins.money -= ExtensionMethod.GetSpawnTowerTb(GameMrg.Ins.currentGemUpgrade).costGold;
                MoneyUISet();
                GameMrg.Ins.currentGemUpgrade++;
                GameMrg.Ins.ReSetSpawnRadom();
            }
            else
            {
                GameSceneUI.Ins.centerUiLabel.gameObject.SetActive(true);
                GameSceneUI.Ins.centerUiLabel.text = "You have not enough money";
                StartCoroutine(GameSceneUI.Ins.GameObjectWaitSetFalse(GameSceneUI.Ins.centerUiLabel.gameObject, 0.5f));
            }
        }

        
    }
    
    public void LevelUpHover()
    {
        info.text = "Gem spawn level higher";
    }

    public void HoverOut()
    {
        info.text = null;
    }

    public void MoneyUISet()
    {
        money.text = "Money : " +GameMrg.Ins.money.ToString();
    }
    

}
