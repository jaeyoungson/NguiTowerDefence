using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class TowerTb : TableBase
{
    public string towerName { get; private set; }
    public enumTower eID { get; private set; }  
    public float attackSpeed { get; private set; }
    public int damage { get; private set; }
    public int range { get; private set; }
    public AttackType attackType { get; private set; }
    public float bulletSpeed { get; private set; }
    public int levelUpcost { get; private set; }

    public TowerTb(string n,string e, string a,string d,string r,string at,string bs,string lc)
    {
        towerName = n;
        eID = (enumTower)int.Parse(e);
        attackSpeed = float.Parse(a);
        damage = int.Parse(d);
        range = int.Parse(r);
        attackType = (AttackType)int.Parse(at);
        bulletSpeed = float.Parse(bs);
        levelUpcost = int.Parse(lc);
    }

    public TowerTb(string[] a_Val) : this(a_Val[0], a_Val[1], a_Val[2],
               a_Val[3], a_Val[4],a_Val[5],a_Val[6],a_Val[7])
    {
    }
}
