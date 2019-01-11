using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class InputManager : SingletonBase<InputManager>
{
    //Gem Mouse Move
    public Grid tilemapGrid;
    public bool overlapCheck=false;

    public override void InitBeforeAwake()
    {
        base.InitBeforeAwake();
    }

    public void UpdateLogic ()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

        Vector3Int cellPosition = tilemapGrid.WorldToCell(mousePosition);
        
        //Move Stone
        if (GameMrg.Ins.stone != null &&GameMrg.Ins.getLogic.getCurStateType==GameLogic.Place)
        {            
            //boundary
            if (tilemapGrid.WorldToCell(mousePosition).x > -23 &&
                  tilemapGrid.WorldToCell(mousePosition).x<22&&
                  tilemapGrid.WorldToCell(mousePosition).y>-8 &&
                  tilemapGrid.WorldToCell(mousePosition).y<15)
            {
                GameMrg.Ins.stone.transform.position = tilemapGrid.CellToWorld(cellPosition);
            }
            
        }


        //Place Stone
        if (Input.GetButtonDown("Fire1") &&
            GameMrg.Ins.getLogic.getCurStateType==GameLogic.Place&&
            GameMrg.Ins.isMoveTower==false&&
            GameMrg.Ins.stone!=null)
        {            
           //OverlapsCheck and findPath
           if(GridTile.Ins.pathfinder.isCanTower(cellPosition,GridTile.Ins.turningNode)&& GridTile.Ins.OverlapCheck(cellPosition))
           {
                string randomGemName = GameMrg.Ins.spawnRandom.RandomGetGemList();
                if (GameMrg.Ins.gemInstantCount>5)
                {
                    GameMrg.Ins.getLogic.SetState(GameLogic.Keep);
                }
                else
                {   
                    ExtensionMethod.GameGemSpriteChange(randomGemName, GameMrg.Ins.stone);
                    GridTile.Ins.AddBoardTower(cellPosition);
                    GridTile.Ins.InhibitNode(cellPosition);
                    GameSceneUI.Ins.uiPlace.newGemButton.SetActive(true);
                    if (GameMrg.Ins.gemInstantCount >= 5)
                    {
                        GameMrg.Ins.getLogic.SetState(GameLogic.Keep);
                    }
                    else
                    {
                        GameMrg.Ins.getLogic.SetState(GameLogic.TowerSpawn);
                    }                   
                }
                GameMrg.Ins.StonetoTower(randomGemName);
                GameMrg.Ins.stone = null;
            }
            else
            {
                GameSceneUI.Ins.BuildCannot();
            }
 
        }
    }

    public IEnumerator UiStateToMove()
    {
        yield return new WaitForSeconds(0.18f);
        GameMrg.Ins.getLogic.SetState(GameLogic.Place);
    }
}
