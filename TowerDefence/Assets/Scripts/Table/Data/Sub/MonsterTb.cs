using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class MonsterTb : TableBase
{
    public MonsterId id { get; private set; }
    public int hp { get; private set; }
    public int moveSpeed { get; private set; }
    public string name { get; private set; }
    public MonsterType targetType { get; private set; }
    public int monsterPoint { get; private set; }
     
    public MonsterTb(string i,string h,string m,string n,string mt,string mp)
    {
        id = (MonsterId)int.Parse(i);
        hp = int.Parse(h);
        moveSpeed = int.Parse(m);
        name = n;
        targetType = (MonsterType)int.Parse(mt);
        monsterPoint = int.Parse(mp);
    }

    public MonsterTb(string [] input)
        :this(input[0],input[1] ,input[2],input[3],input[4],input[5])
    {
    }

}
