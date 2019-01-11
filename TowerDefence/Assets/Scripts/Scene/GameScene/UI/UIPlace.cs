using UnityEngine;

public class UIPlace : MonoBehaviour {
    public UISprite[] itemSprites = new UISprite[5];
    public UISprite combineButton;
    public UISprite keepButton;
    public GameObject newGemButton;
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void ItemSpriteReset()
    {
        for(int i=0; i<itemSprites.Length;i++)
        {
            itemSprites[i].gameObject.SetActive(false);
        }
    }

    public void ItemSpriteChange()
    {
        int count = 0;        
        for (int i = 0; i < GameMrg.Ins.createTower.Count; i++)
        {
            if(GameMrg.Ins.createTower[i]!=null)
            {
                itemSprites[i-count].spriteName = GameMrg.Ins.createTower[i].towerName;                
            }
            else
            {
                count++;
            }
        }
        
        for(int i =5-count; i<5;i++)
        {
            itemSprites[i].gameObject.SetActive(false);
        }
        
    }

    public void NewGemButtonFuction()
    {
        GameMrg.Ins.InstanceGem();
    }
}
