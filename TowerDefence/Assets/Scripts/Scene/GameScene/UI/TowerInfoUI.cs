using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
public class TowerInfoUI : MonoBehaviour
{
    public UISprite infoGemSprite;
    public UILabel infoDamage;
    public UILabel infoName;
    public UILabel infoAttackSpeed;
    public UILabel infoAttackType;
    public UILabel infoRange;
    public TweenPosition panelTween;
    public bool isOpen = false;

    public void SetInfoUI()
    {
        infoGemSprite.spriteName = GameMrg.Ins.selectTower.towerName;
        infoDamage.text = "Damage : "+GameMrg.Ins.selectTower.damage.ToString();
        infoName.text = "Name : "+GameMrg.Ins.selectTower.towerName;
        infoAttackSpeed.text = "AttackSpeed : "+GameMrg.Ins.selectTower.attackSpeed.ToString();
        infoAttackType.text ="AttackType : "+ GameMrg.Ins.selectTower.attackType.ToDesc();
        infoRange.text = "Range :"+GameMrg.Ins.selectTower.range.ToString();
    }

    public void OpenUI()
    {
        SetInfoUI();
        panelTween.PlayForward();
        isOpen = true;
    }

    public void ClosedUI()
    {
        panelTween.PlayReverse();
        isOpen = false;
    }
    
    public void LevelDown()
    {
        if(GameMrg.Ins.selectTower.eId/10<1||(enumTower)GameMrg.Ins.selectTower.eId ==enumTower.Stone)
        {
            GameSceneUI.Ins.centerUiLabel.text = "Already is tower at least";
            GameSceneUI.Ins.CenterLabelOnWaitOff();
            SetInfoUI();
        }
        else
        {
            int downId = (GameMrg.Ins.selectTower.eId / 10) - 1;
            int kind = GameMrg.Ins.selectTower.eId % 10;

            int changeTowerNumber = (downId * 10) + kind;
            enumTower towerId = (enumTower)changeTowerNumber;
            string towerIdToDesc = towerId.ToDesc();
            Tower changeTower = new Tower();
            var tbData = ExtensionMethod.GetTowerTb(towerIdToDesc);
            GameMrg.Ins.selectTower.eId = (int)tbData.eID;
            GameMrg.Ins.selectTower.damage = tbData.damage;
            GameMrg.Ins.selectTower.attackSpeed = tbData.attackSpeed;
            GameMrg.Ins.selectTower.range = tbData.range;
            GameMrg.Ins.selectTower.towerName = tbData.towerName;
            GameMrg.Ins.selectTower.bulletSpeed = tbData.bulletSpeed;
            GameMrg.Ins.selectTower.levelUpcost = tbData.levelUpcost;
            ExtensionMethod.GameGemSpriteChange(GameMrg.Ins.selectTower.towerName , GameMrg.Ins.selectTower.gameObject);
            GameMrg.Ins.selectTower = changeTower;
            SetInfoUI();
        }
    }

    public void LevelUp()
    {
        if(GameMrg.Ins.selectTower!=null)
        {
            if((enumTower)GameMrg.Ins.selectTower.eId != enumTower.Stone)
            {
                if (GameMrg.Ins.selectTower.eId / 10 < 3)
                {
                    if (GameMrg.Ins.money > GameMrg.Ins.selectTower.levelUpcost)
                    {
                        int downId = (GameMrg.Ins.selectTower.eId / 10) + 1;
                        int kind = GameMrg.Ins.selectTower.eId % 10;

                        int changeTowerNumber = (downId * 10) + kind;
                        enumTower towerId = (enumTower)changeTowerNumber;
                        string towerIdToDesc = towerId.ToDesc();
                        Tower changeTower = new Tower();
                        var tbData = ExtensionMethod.GetTowerTb(towerIdToDesc);
                        GameMrg.Ins.selectTower.eId = (int)tbData.eID;
                        GameMrg.Ins.selectTower.damage = tbData.damage;
                        GameMrg.Ins.selectTower.attackSpeed = tbData.attackSpeed;
                        GameMrg.Ins.selectTower.range = tbData.range;
                        GameMrg.Ins.selectTower.towerName = tbData.towerName;
                        GameMrg.Ins.selectTower.bulletSpeed = tbData.bulletSpeed;
                        GameMrg.Ins.selectTower.levelUpcost = tbData.levelUpcost;
                        ExtensionMethod.GameGemSpriteChange(GameMrg.Ins.selectTower.towerName, GameMrg.Ins.selectTower.gameObject);                       
                        SetInfoUI();
                    }
                    else
                    {
                        GameSceneUI.Ins.centerUiLabel.text = "Not enough money";
                        GameSceneUI.Ins.CenterLabelOnWaitOff();
                    }
                }
                else
                {
                    GameSceneUI.Ins.centerUiLabel.text = "Stone impossible level up ";
                    GameSceneUI.Ins.CenterLabelOnWaitOff();
                }
            }
            else
            {

                GameSceneUI.Ins.centerUiLabel.text = "Already is tower maximum the level";
                GameSceneUI.Ins.CenterLabelOnWaitOff();
            }
        }
    }

    public void Delete()
    {
        if(GameMrg.Ins.getLogic.getCurStateType !=GameLogic.Place|| 
            GameMrg.Ins.getLogic.getCurStateType != GameLogic.TowerSpawn            
            )
        {
            Vector3Int selectTowerPosition = GridTile.Ins.GetBoardPosition(GameMrg.Ins.selectTower.transform);
            GameMrg.Ins.createTower[GameMrg.Ins.selectTower.createTowerNumber]=null;
            GridTile.Ins.InhibitNodeFalse(selectTowerPosition);
            GameMrg.Ins.selectTower.gameObject.SetActive(false);            

            ClosedUI();
            GameSceneUI.Ins.uiPlace.ItemSpriteChange();
            DestroyImmediate(GameMrg.Ins.selectTower.gameObject);
        }
        else
        {
            GameSceneUI.Ins.centerUiLabel.text = "all spawn tower please";
            GameSceneUI.Ins.CenterLabelOnWaitOff();
        }
    }
}
