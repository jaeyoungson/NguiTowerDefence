using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTowerTb : TableBase
{
    public int upgradeLevel { get; private set; }
    public int chip         { get; private set; }
    public int flawed       { get; private set; }
    public int normal       { get; private set; }
    public int perfect { get; private set; }
    public int costGold     { get; private set; }

    public SpawnTowerTb( string up,string c,string f,string n,string p,string cg)
    {
        upgradeLevel = int.Parse(up);
        chip = int.Parse(c);
        flawed = int.Parse(f);
        normal = int.Parse(n);
        perfect = int.Parse(p);
        costGold = int.Parse(cg);
    }

    public SpawnTowerTb(string [] a_Val):this(a_Val[0], a_Val[1], a_Val[2],
               a_Val[3], a_Val[4],a_Val[5])
    {
    }
}
