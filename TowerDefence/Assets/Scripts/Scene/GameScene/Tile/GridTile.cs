using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Global_Define;
using System;

public class GridTile : SingletonBase<GridTile>
{

    public Grid board;
    List<Vector3Int> buildTower = new List<Vector3Int>();
    public List<GameObject> monsterTurningPoint;
    public Transform startPosition;
    //Path Finder
    public Pathfinder pathfinder;
    public Pathfinder testPathfinder;
    public List<Node> turningNode = new List<Node>();
    private void Awake()
    {
        base.InitAftwerAwake();
        //Set pathfinder 
        pathfinder = new Pathfinder();
        pathfinder.CreateNode(52, 30, 0.35f, new Vector3(0.0f, 0.0f, 0));
        SetStartBoard();
    }
    
    
    public Vector3Int GetBoardPosition(Transform position)
    {
        return board.WorldToCell(position.position);
    }
    public Vector3Int GetStartPosition()
    {
        return GetBoardPosition(startPosition);
    }

    private void SetStartBoard()
    {
        for (int i = 0; i < monsterTurningPoint.Count; i++)
        {
            Vector3Int cellPosition = board.WorldToCell(monsterTurningPoint[i].transform.position);
            SetTurningPoint(cellPosition);
        }
    }

    private void SetTurningPoint(Vector3Int nodePosition)
    {
        for(int x = nodePosition.x-1; x< nodePosition.x+1; x++)
        {
            for(int y= nodePosition.y-1; y< nodePosition.y+1; y++)
            {                
                Node node =pathfinder.nodeGroup.GetNode(x, y);
                node.isBuildable = false;
                turningNode.Add(node);
            }
        }
    }
    
    public void InhibitNode(Vector3Int towerPosition)
    {
        for(int x=towerPosition.x-1;x<towerPosition.x+1;x++)
        {
            for(int y = towerPosition.y-1;y<towerPosition.y+1;y++)
            {
               Node inhibitNode = pathfinder.nodeGroup.GetNode(x, y);
               inhibitNode.isWalkable = false;
               inhibitNode.isBuildable = false;
            }
        }
    }

    public void InhibitNodeFalse(Vector3Int towerPosition)
    {

        for (int x = towerPosition.x - 1; x < towerPosition.x + 1; x++)
        {
            for (int y = towerPosition.y - 1; y < towerPosition.y + 1; y++)
            {
                Node inhibitNode = pathfinder.nodeGroup.GetNode(x, y);
                inhibitNode.isWalkable = true;
                inhibitNode.isBuildable = true;
            }
        }
    }
   
    public void AddBoardTower(Vector3Int towerPosition)
    {
        buildTower.Add(towerPosition);
    }

    public bool OverlapCheck(Vector3Int stonePosition)
    {
        bool overlaps = false;
        NodeGroup nodeGroup = pathfinder.nodeGroup;
        for (int x = stonePosition.x - 1; x <stonePosition.x+1; x++)
        {
            for (int y = stonePosition.y - 1; y <stonePosition.y+1; y++)
            {
                if(nodeGroup.GetNode(x, y).isBuildable==false)
                {
                    overlaps = false;
                    break;
                }
                else
                {
                    overlaps = true;
                }
            }

            if (overlaps == false)
            {
                break;
            }
        }        
        
        return overlaps;

    }
    
    public List<Vector3> SetMovePath()
    {
        //monster reset position
        Vector3Int startPosition = GetStartPosition();
        //return MovePath
        List<Vector3> temp = new List<Vector3>();

        for(int i=0; i<monsterTurningPoint.Count-1;i++)
        {
            Vector3Int start = GetBoardPosition(monsterTurningPoint[i].transform);
            Vector3Int end = GetBoardPosition(monsterTurningPoint[i + 1].transform);
            temp.AddRange(pathfinder.FindPath(start.x, start.y, end.x, end.y));
        }
        return temp;
    }

    private void OnDrawGizmos()
    {   
        if(null!=pathfinder)        
            pathfinder.DrawGizmos();
    }
}
