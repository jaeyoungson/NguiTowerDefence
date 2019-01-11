using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    private const int cost = 10000;
    bool isSkillStop = false;
    public void MonsterMoveStop()
    {
        if(GameMrg.Ins.getLogic.getCurStateType == Global_Define.GameLogic.Battle)
        {
            if (GameMrg.Ins.money >= cost)
            {
                if (isSkillStop == false)
                {
                    GameMrg.Ins.money -= cost;
                    GameSceneUI.Ins.consumeUI.MoneyUISet();
                    SkillStop();
                }
                else
                {
                    GameSceneUI.Ins.centerUiLabel.text = "Stop skill is Cooldown";
                    GameSceneUI.Ins.GameObjectWaitSetFalse(GameSceneUI.Ins.centerUiLabel.gameObject);

                }
            }
            else
            {
                GameSceneUI.Ins.centerUiLabel.text = "Not enough Money";
                StartCoroutine(GameSceneUI.Ins.GameObjectWaitSetFalse(GameSceneUI.Ins.centerUiLabel.gameObject));
            }
        }
        else
        {
            GameSceneUI.Ins.centerUiLabel.text = "Use Plz Battle";
            StartCoroutine(GameSceneUI.Ins.GameObjectWaitSetFalse(GameSceneUI.Ins.centerUiLabel.gameObject));
        }
 
    }

    private void SkillStop()
    {
        isSkillStop = true;
        for(int i =0;i<GameMrg.Ins.monsterList.Count;i++)
        {
            Monster monster=GameMrg.Ins.monsterList[i].GetComponent<Monster>();
            monster.move.isStop = true ;
        }
        StartCoroutine(StopCoolDown());
    }

    IEnumerator StopCoolDown()
    {
        yield return new WaitForSeconds(10.0f);
        isSkillStop = false;
        for (int i = 0; i < GameMrg.Ins.monsterList.Count; i++)
        {
            Monster monster = GameMrg.Ins.monsterList[i].GetComponent<Monster>();
            monster.move.isStop = false;
        } 
    }

    public void StopHover()
    {
        GameSceneUI.Ins.consumeUI.info.text = "Map exist all monster Stop 10 Seconds";
    }
    public void StopHoverOut()
    {
        GameSceneUI.Ins.consumeUI.info.text = null;
    }
}
