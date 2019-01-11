using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using Global_Define;
using UnityEngine.U2D;

public static class ExtensionMethod
{
    #region TableMethod
    // enum
    public static string ToDesc(this System.Enum a_eEnumVal)
	{
		var da = (DescriptionAttribute[])(a_eEnumVal.GetType().GetField(a_eEnumVal.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
        return da.Length > 0 ? da[0].Description : a_eEnumVal.ToString();
 	}

    public static Dictionary<System.Type, eTable> mapType = new Dictionary<System.Type, eTable>();
    //Table ExtensionMethod
    static ExtensionMethod()
    {
        mapType.Add(typeof(ConfigTb), eTable.Config);
        mapType.Add(typeof(TowerTb), eTable.Tower);
        mapType.Add(typeof(SpawnTowerTb),eTable.SpawnTower);
        mapType.Add(typeof(MonsterTb), eTable.Monster);
    }

    public static string TableDescription(this System.Type type)
    {
        return mapType[type].ToDesc();
    }

    public static float GetConfigValue(this eConfig a_eConfig)
    {
        return Table<string, ConfigTb>.GetTb(a_eConfig.ToDesc()).fValue;
    }

    public static TowerTb GetTowerTb(this string id)
    {
        return Table<string, TowerTb>.GetTb(id);
    }

    public static SpawnTowerTb GetSpawnTowerTb(this int upgradeLevel)
    {
        return Table<int, SpawnTowerTb>.GetTb(upgradeLevel);
    }

    public static MonsterTb GetMonsterTb(this MonsterId id)
    {
        return Table<MonsterId, MonsterTb>.GetTb(id);
    }
    
    #endregion
        
    public static void RandomChangeSprite(UISprite target)
    {
        string randomName = ((GemColor)Random.Range(0, 4)).ToDesc()+"0"+Random.Range(1,8).ToString();
        target.spriteName = randomName;
    }


    //in GameGemSpriteChange
    public static void GameGemSpriteChange(string spriteName, GameObject changeObject)
    {
        SpriteRenderer targetSprite = changeObject.GetComponent<SpriteRenderer>();

        var changeSprite = AtlasManager.Ins.AtlasGetSprite(GameAtlasName.GameGemAtlas, spriteName);

        targetSprite.sprite = changeSprite;
    }
    public static void BulletSpriteChange(string spriteName, GameObject changeObject)
    {
        SpriteRenderer targetSprite = changeObject.GetComponent<SpriteRenderer>();

        var changeSprite = AtlasManager.Ins.AtlasGetSprite(GameAtlasName.BulletAtlas, spriteName);

        targetSprite.sprite = changeSprite;
    }    

    public static void MonsterSpriteChange(string spriteName,SpriteRenderer changeObject)
    {
        var changeSprite = AtlasManager.Ins.AtlasGetSprite(GameAtlasName.MonsterAtlas, spriteName);

        changeObject.sprite = changeSprite;
    }
   
}
