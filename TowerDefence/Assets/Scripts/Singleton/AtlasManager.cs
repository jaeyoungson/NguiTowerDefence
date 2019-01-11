using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Global_Define;
public class AtlasManager : SingletonBase<AtlasManager>
{
    public List<SpriteAtlas> atlasList;
    Dictionary<GameAtlasName, SpriteAtlas> gameAtlasDictionary = new Dictionary<GameAtlasName, SpriteAtlas>();
    private void Awake()
    {

        base.InitAftwerAwake();
        for (int i =0;i<atlasList.Count;i++)
        {
            string name = atlasList[i].name;
            GameAtlasName atlas = (GameAtlasName)System.Enum.Parse(typeof(GameAtlasName), name);
        }
    }

    public SpriteAtlas GetGameGemAtlas(GameAtlasName atlasName)
    {
        if(gameAtlasDictionary.ContainsKey(atlasName)==false)
        {
            SpriteAtlas atlasObject = Resources.Load("Atlas/"+atlasName.ToDesc()) as SpriteAtlas;
            gameAtlasDictionary.Add(atlasName, atlasObject);
        }
        return gameAtlasDictionary[atlasName];
    }

    public Sprite AtlasGetSprite(GameAtlasName atlasName,string spriteName)
    { 
        return GetGameGemAtlas(atlasName).GetSprite(spriteName);
    }
}
