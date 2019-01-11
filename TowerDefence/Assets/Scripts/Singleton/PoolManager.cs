using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
public class PoolManager : SingletonBase<PoolManager>
{
    public Dictionary<PoolType, Queue<GameObject>> minipools = new Dictionary<PoolType, Queue<GameObject>>();
    public List<GameObject> prefabs;
    public Transform hpPoolParent;
    public Transform damagePoolParent;

    private void Awake()
    {

        base.InitAftwerAwake();
        GameObject monsterPool = Instantiate(prefabs[(int)PoolPrefab.MiniPool]);
        SetPool(30, monsterPool.transform, PoolType.Monster);
        GameObject bulletPool = Instantiate(prefabs[(int)PoolPrefab.MiniPool]);
        SetPool(30, bulletPool.transform, PoolType.Bullet);
        GameObject effectPool = Instantiate(prefabs[(int)PoolPrefab.MiniPool]);
        SetPool(50, effectPool.transform, PoolType.ColorEffect);
        GameObject iceEffectPool = Instantiate(prefabs[(int)PoolPrefab.MiniPool]);
        SetPool(20, iceEffectPool.transform, PoolType.IceEffect);

        SetPool(20, hpPoolParent, PoolType.HpBar);
        SetPool(40, damagePoolParent, PoolType.Damage);

    }

    private enum PoolPrefab
    {
        MiniPool,
        Monster,
        Bullet,
        HpBar,
        Damage,
        ColorfulEffect,
        IceEffect,
    }

    public void SetPool(int count,Transform parent,PoolType poolType)
    {
        Queue<GameObject> list = new Queue<GameObject>();
        for(int i=0;i<count;i++)
        {
            GameObject obj = Instantiate(prefabs[(int)poolType+1], parent);
            list.Enqueue(obj);
        }
        minipools.Add(poolType, list);
    }

    public Queue<GameObject> GetPool(PoolType type)
    {
        return minipools[type];
    }

    public GameObject GetPoolObject(PoolType type)
    { 
        return minipools[type].Dequeue();
    }
}
