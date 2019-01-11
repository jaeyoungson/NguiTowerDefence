using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    private float slowSpeed;
    private float originalSpeed;
    private bool slowState;

    private float SetSlowSpeed(float origin)
    {
        originalSpeed = origin;
       return slowSpeed = originalSpeed - (originalSpeed / 2);
    }
    
    public void SkillSlow()
    {
        if(GameMrg.Ins.getLogic.getCurState.getStateType == Global_Define.GameLogic.Battle)
        {
            if (slowState == false)
            {
                if (GameMrg.Ins.money > 1000)
                {
                    for (int i = 0; i < GameMrg.Ins.monsterList.Count; i++)
                    {
                        Monster monster = GameMrg.Ins.monsterList[i].GetComponent<Monster>();
                        monster.moveSpeed = SetSlowSpeed(monster.moveSpeed);
                    }
                    slowState = true;
                    StartCoroutine(ReturnMoveSpeed());
                }
                else
                {
                    GameSceneUI.Ins.centerUiLabel.text = "Not enough money";
                    GameSceneUI.Ins.centerUiLabel.gameObject.SetActive(true);
                    StartCoroutine(GameSceneUI.Ins.GameObjectWaitSetFalse(GameSceneUI.Ins.centerUiLabel.gameObject));
                }
            }
            else
            {

                GameSceneUI.Ins.centerUiLabel.text = "It's currently in use.";
                GameSceneUI.Ins.centerUiLabel.gameObject.SetActive(true);
                StartCoroutine(GameSceneUI.Ins.GameObjectWaitSetFalse(GameSceneUI.Ins.centerUiLabel.gameObject));
            }
        }
        else
        {
            GameSceneUI.Ins.centerUiLabel.text = "Use Battle Plz";
            GameSceneUI.Ins.centerUiLabel.gameObject.SetActive(true);
            StartCoroutine(GameSceneUI.Ins.GameObjectWaitSetFalse(GameSceneUI.Ins.centerUiLabel.gameObject));

        }
 

    }

    IEnumerator ReturnMoveSpeed()
    {        
        yield return new WaitForSeconds(10.0f);
        for (int i = 0; i < GameMrg.Ins.monsterList.Count; i++)
        {
            Monster monster = GameMrg.Ins.monsterList[i].GetComponent<Monster>();
            monster.moveSpeed =originalSpeed;
            slowState = false;
        }
    }

    public void HoverSlowSkill()
    {
        GameSceneUI.Ins.consumeUI.info.text = "Map exist All Monster slow 10Seconds";
    }

    public void HoverOutSlowSkill()
    {
        GameSceneUI.Ins.consumeUI.info.text = null;
    }
}
