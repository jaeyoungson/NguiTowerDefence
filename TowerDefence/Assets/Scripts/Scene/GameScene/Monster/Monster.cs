using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
using System;

public class Monster : MonoBehaviour
{
    public MonsterId id;
    public int maxHp;
    public int hp;
    public float moveSpeed;
    public string name;
    public MonsterType targetType;
    public int monsterPoint;
    public int curTurningPoint = 0;
    public bool isDie = false;
    public SpriteRenderer monsterSprite;
    //Ui damage and hpBar
    public Transform uiTransform;
    public TargetPathMover move = new TargetPathMover();
    [HideInInspector]
    public GameObject hpBar;
    Coroutine temp;
    TweenPosition tp;

    public void SetMonsterStat(MonsterTb monsterData)
    {
        id = monsterData.id;
        maxHp = monsterData.hp;
        moveSpeed = monsterData.moveSpeed;
        name = monsterData.name;
        targetType = monsterData.targetType;
        monsterPoint = monsterData.monsterPoint;
        hp = maxHp;
    }


    private void Update()
    {
        if (move.isStop == false)
        {
            Vector3 movePos = move.GetPos(transform.position, moveSpeed, eMOVE_OUT_TYPE.DELTA);
            transform.position += movePos;
            transform.up = Vector3.Lerp(transform.up, movePos.normalized, Time.deltaTime * 10f);
        }
        if (hpBar != null)
        {
            Vector3 viewportPosition = WorldPositionToUICameraViewport(uiTransform.position);
            viewportPosition.y += 0.08f;
            hpBar.transform.position = viewportPosition;
            
            UISprite hpSprite = hpBar.GetComponent<UISprite>();
            hpSprite.fillAmount = (float)hp / (float)maxHp;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet=collision.gameObject.GetComponent<Bullet>();

            if(bullet.m_tower.eId%10==6)//whitetower endNumber 6
            {
                GameObject effectObject = PoolManager.Ins.minipools[PoolType.IceEffect].Dequeue();
                Effect effect = effectObject.GetComponent<Effect>();
                effect.colorNumber = bullet.m_tower.eId % 10;
                effectObject.transform.position = gameObject.transform.position;
                effectObject.SetActive(true);
            }
            else
            {
                GameObject effectObject = PoolManager.Ins.minipools[PoolType.ColorEffect].Dequeue();
                effectObject.transform.position = gameObject.transform.position;
                Effect effect = effectObject.GetComponent<Effect>();
                effect.effectColor.Color = EffectColor((GemColor)(bullet.m_tower.eId % 10));
                effectObject.SetActive(true);
            }
            
            //damage UI
            GameObject damage = PoolManager.Ins.minipools[PoolType.Damage].Dequeue();
            Vector3 damageViewportPosition = WorldPositionToUICameraViewport(uiTransform.position, new Vector2(0.0f, 0.0f));
            damage.transform.position = damageViewportPosition;
            DamageUI ui=damage.GetComponent<DamageUI>();           
            ui.damageLabel.text = bullet.damage.ToString();
            damage.gameObject.SetActive(true);
            ui.WaitDelete();
            ui.TweenStart();
        

            hp -= bullet.damage;

            //hpBar is null
            if (hpBar == null)
            {
                Vector3 viewportPosition = WorldPositionToUICameraViewport(uiTransform.position,new Vector2(0,0.08f));
                GameObject hpBarObj = PoolManager.Ins.minipools[PoolType.HpBar].Dequeue();
                hpBar = hpBarObj;
                hpBarObj.transform.position = viewportPosition;
                hpBarObj.gameObject.SetActive(true);
                UISprite hpSprite = hpBarObj.GetComponent<UISprite>();               
                hpSprite.fillAmount = (float)hp / (float)maxHp;
                if(gameObject.activeInHierarchy ==true)
                temp = StartCoroutine(HpBarWaitDelete(hpBar));                
            }
            else
            //HpBar가 존재할때
            {
                UISprite hpSprite = hpBar.GetComponent<UISprite>();
                hpSprite.fillAmount = (float)hp / (float)maxHp;
                if (temp == null)
                {
                    temp = StartCoroutine(HpBarWaitDelete(hpBar));
                }
                else
                {
                    StopCoroutine(temp);
                    temp = StartCoroutine(HpBarWaitDelete(hpBar));
                }

            } 
            PoolManager.Ins.minipools[PoolType.Bullet].Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);

            if(hp<=0&&isDie==false) 
            {
                isDie = true;   
                PoolManager.Ins.minipools[PoolType.Monster].Enqueue(gameObject);
                GameMrg.Ins.monsterCheck++;
                GameMrg.Ins.money += monsterPoint;
                GameSceneUI.Ins.consumeUI.MoneyUISet();
                HpBarDelete(hpBar);
                gameObject.SetActive(false);
            }
        }

    }

    private Color EffectColor(GemColor gemColor)
    {
        gemColor=gemColor-1;
        Color tempColor = new Color();
        switch (gemColor)
        {
            case GemColor.Black:
                tempColor = Color.gray;
                break;
            case GemColor.Green:
                tempColor = Color.green;
                break;
            case GemColor.Red:
                tempColor = Color.red;
                break;
            case GemColor.Sky:
                tempColor = new Color(0.31f, 0.73f, 0.87f);
                break;
            case GemColor.Yellow:
                tempColor = Color.yellow;
                break;
        }
        return tempColor;
    }

    private Vector3 WorldPositionToUICameraViewport(Vector3 worldPosition)
    {
        Vector3 mainCamera = Camera.main.WorldToViewportPoint(uiTransform.position);
        Vector3 uiCamera = GameSceneUI.Ins.uiCamera.ViewportToWorldPoint(mainCamera);

        return uiCamera;
    }

    private Vector3 WorldPositionToUICameraViewport(Vector3 worldPosition,Vector2 addPosition)
    {
        Vector3 mainCamera = Camera.main.WorldToViewportPoint(uiTransform.position);
        Vector3 uiCamera = GameSceneUI.Ins.uiCamera.ViewportToWorldPoint(mainCamera);
        uiCamera.x += addPosition.x;
        uiCamera.y += addPosition.y;
        return uiCamera;
    }
    
    private IEnumerator HpBarWaitDelete(GameObject hpBarObj)
    {
        yield return new WaitForSeconds(2.0f);
        PoolManager.Ins.minipools[PoolType.HpBar].Enqueue(hpBarObj);
        hpBar = null;
        hpBarObj.SetActive(false);
    }

    private void HpBarDelete(GameObject hpBarObj)
    {
        if(temp!=null)
        {
            StopCoroutine(temp);
            temp = null;
        }
        PoolManager.Ins.minipools[PoolType.HpBar].Enqueue(hpBarObj);
        hpBar = null;
        hpBarObj.SetActive(false);
    }

}