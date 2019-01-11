using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class EndReach : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Monster"))
        {
            GameMrg.Ins.life--;
            GameSceneUI.Ins.LifeReset();
            GameMrg.Ins.monsterCheck++;
            Monster monster=collision.GetComponent<Monster>();
            
            
            if(monster.hpBar !=null)
            {

                PoolManager.Ins.minipools[PoolType.HpBar].Enqueue(monster.hpBar);
                monster.hpBar.SetActive(false);         
            }
            collision.gameObject.SetActive(false);
            PoolManager.Ins.minipools[Global_Define.PoolType.Monster].Enqueue(collision.gameObject);
        }
    }
}
