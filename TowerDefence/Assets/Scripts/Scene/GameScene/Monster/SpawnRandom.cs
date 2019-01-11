using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class SpawnRandom : MonoBehaviour {

    private List<GemLevel> gemLevelList= new List<GemLevel>();
    private Dictionary<GemLevel, int> gemMaps = new Dictionary<GemLevel, int>();

    public bool setRandom = false;
    const int gemLevelKinds = 4;
    const int zero=0;
    const int maximumPercentage = 10000;

    const int colorKinds = 6;

    GemLevel randomLevel;

    public void SetTablePercentage()
    {

        int chip=ExtensionMethod.GetSpawnTowerTb(GameMrg.Ins.currentGemUpgrade).chip;
        gemMaps.Add(GemLevel.Chip, chip);
        int flawed = ExtensionMethod.GetSpawnTowerTb(GameMrg.Ins.currentGemUpgrade).flawed;
        gemMaps.Add(GemLevel.Flawed, flawed);
        int normal = ExtensionMethod.GetSpawnTowerTb(GameMrg.Ins.currentGemUpgrade).normal;
        gemMaps.Add(GemLevel.Normal, normal);
        int perpect = ExtensionMethod.GetSpawnTowerTb(GameMrg.Ins.currentGemUpgrade).perfect;
        gemMaps.Add(GemLevel.Perfect, perpect);

        for(int i =0;i< gemLevelKinds;i++)
        {
            int percentage;
            gemMaps.TryGetValue((GemLevel)i,out percentage);
            AddList((GemLevel)i,percentage);
        }
        setRandom = true;
    }

    public void AddList(GemLevel level,int spawnPercentage)
    {
        for(int i=0; i<spawnPercentage;i++)
        {
            gemLevelList.Add(level);
        }
    }

    public string RandomGetGemList()
    {
       int random= Random.Range(zero, gemLevelList.Count);

        randomLevel= gemLevelList[random];
        int randomColor = Random.Range(0, colorKinds);

        GemColor color = (GemColor)randomColor;

        string name = randomLevel.ToDesc() +color.ToDesc();

        return name;
    }

    public void ReSetTablePercentage()
    {
        gemMaps.Clear();
        gemLevelList.Clear();
        SetTablePercentage();
    }
}
