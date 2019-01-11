using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
public class Tower : MonoBehaviour
{
    [HideInInspector]
    public int eId;
    [HideInInspector]
    public string towerName;
    [HideInInspector]
    public float range;
    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public int createTowerNumber;
    [HideInInspector]
    public AttackType attackType;
    [HideInInspector]
    public float bulletSpeed;
    [HideInInspector]
    public int levelUpcost;

    public bool isCombineTower;
    bool isAttack = true;

    public void SetTowerData(TowerTb towerData,int gemInstanceCount)
    {
        eId = (int)towerData.eID;
        towerName = towerData.towerName;
        range = towerData.range;
        attackSpeed = towerData.attackSpeed;
        damage = towerData.damage;
        bulletSpeed = towerData.bulletSpeed;
        createTowerNumber = gemInstanceCount;
        levelUpcost = towerData.levelUpcost;
    }

    private void Update()
    {
       if(GameMrg.Ins.getLogic.getCurStateType==GameLogic.Battle && eId != (int)enumTower.Stone&&isAttack==true)
        {
            Search();
        }
    }

    public void Search()
    {
        List<GameObject> monsterPool = GameMrg.Ins.monsterList;


        for(int i =0; i<monsterPool.Count;i++)
        {
            if(monsterPool[i] != null && monsterPool[i].activeInHierarchy == true)
            {
                Vector3 offset = monsterPool[i].transform.position - gameObject.transform.position;
                float distance = offset.sqrMagnitude;
                if(distance<range && isAttack ==true)
                {
                    StartCoroutine(Attack(monsterPool[i].transform));
                }

            }
        }
    }

    private IEnumerator Attack(Transform monsterTransform)
    {
        isAttack = false;
        GameObject bulletObject = PoolManager.Ins.GetPoolObject(PoolType.Bullet);
        bulletObject.transform.position = gameObject.transform.position;
        bulletObject.SetActive(true);
        ExtensionMethod.BulletSpriteChange(towerName, bulletObject);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.startPosition = transform.position;
        bullet.attackTarget = monsterTransform;
        bullet.damage = damage;
        bullet.bulletSpeed = bulletSpeed;
        bullet.elapsedTime = 0.0f;
        bullet.m_tower = gameObject.GetComponent<Tower>();
        yield return new WaitForSeconds(attackSpeed);
        isAttack = true;
    }
}
