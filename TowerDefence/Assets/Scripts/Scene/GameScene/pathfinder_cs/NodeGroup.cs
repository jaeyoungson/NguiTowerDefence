using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGroup
{    
    int m_xCount = 0;
    int m_zCount = 0;
    float m_width = 1f;
    Vector3 m_offset = Vector3.zero;
    
    Node[,] m_nodeList;
    Dictionary<Vector2, Node> nodeDictionary ;

    public int getXCount
    {
        get
        {
            return m_xCount;
        }
    }
    public int getZCount
    {
        get
        {
            return m_zCount;
        }
    }
    public float getWidth
    {
        get
        {
            return m_width;
        }
    }

    public Node GetNodePosition( Vector3 _pos )
    {       
        float _x = (_pos.x-m_offset.x)/m_width;
        float _y = (_pos.y-m_offset.y)/m_width;       
        int _nodeX = (int)(_x);
        int _nodeY = (int)(_y);
        //Debug.Log("x :" + _xx + " : " + "y :" + _yy);

        //return GetNode(_nodeX, _nodeY);
        return GetNode(_nodeX, _nodeY);
    }

    //public Vector3 GetPosition(int nodex, int nodey )
    //{
    //    Node _node = GetNode(nodex, nodey);
    //    if (null == _node)
    //        return Vector3.zero;
    //    float _half = m_width * 0.5f;
    //    return new Vector3(m_offset.x + _node.x * m_width + _half, m_offset.y + _node.z * m_width + _half, 0f);
    //}

    //public Vector3 GetPosition( Node _node)
    //{
    //    if (null == _node)
    //        return Vector3.zero;
    //    float _half = m_width * 0.5f;
    //    return new Vector3(m_offset.x+_node.x* m_width+ _half, m_offset.y + _node.z * m_width + _half, 0f);
    //}

    //
    public Vector3 GetPosition(Node _node)
    {
        if (_node == null)
            return Vector3.zero;
        float _half = m_width * 0.5f;
        return new Vector3(m_offset.x + _node.x * m_width + _half, m_offset.y + _node.z * m_width + _half, 0f);
    }
    //
    public Vector3 GetPosition(int x, int y)
    {
        Node _node = GetNode(x, y);
        if (_node == null)
            return Vector3.zero;
        float _half = m_width * 0.5f;
        return new Vector3(m_offset.x + _node.x * m_width + _half, m_offset.y + _node.z * m_width + _half, 0f);
    }


    //public Node GetNode(int x, int z)
    //{
    //    //Used to get a node from a grid,
    //    //If it's greater than all the maximum values we have
    //    //then it's going to return null

    //    Node retVal = null;
    //    if (x < m_xCount && x >= 0 &&            
    //        z >= 0 && z < m_zCount)
    //    {
    //        retVal = m_nodeList[x, z];
    //    }

    //    return retVal;
    //}
    //
    public Node GetNode(int x,int y)
    {
        Node retVal = null;

        if (nodeDictionary.ContainsKey(new Vector2(x, y)))
        {
            retVal = nodeDictionary[new Vector2(x, y)];
        }

        return retVal;
    }

    public NodeGroup( int _x, int _y, float _width, Vector3 _offset)
    {
        m_xCount = _x;
        m_zCount = _y;
        m_width = _width;
        m_offset = _offset;
        
        nodeDictionary = new Dictionary<Vector2, Node>();
        
        for (int z = -m_zCount / 2; z < m_zCount / 2; z++)
        {
            for (int x = -m_xCount / 2; x < (m_xCount / 2)+1; x++)
            {
                //Apply the offsets and create the world object for each node
                float posX = (x * _width)+m_offset.x;
                float posZ = (z * _width)+m_offset.y;

                //Create a new node and update it's values
                Node node = new Node();
                node.x = x;
                node.z = z;
                Vector2 nodePos = new Vector2();
                nodePos.x = x;
                nodePos.y = z;

                /*GameObject go = ResUtil.CreateGameObject("node");
                go.transform.position = m_offset + new Vector3(posX, posZ, posY);
                go.transform.localScale = Vector3.one * 5f;
                node.worldObject = go;*/

                //then place it to the grid
                if(nodeDictionary.ContainsKey(nodePos)==false)
                {
                    nodeDictionary.Add(nodePos, node);
                }
            }
        }

    }

    public void DrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int z =- m_zCount / 2;  z< m_zCount/2; z++)
        {            
            for (int  x= -m_xCount/2;  x< m_xCount/2; x++)
            {
                float posX = (x * m_width) + m_offset.x;                
                float posZ = (z * m_width) + m_offset.y; 
                Vector3 _startPos = new Vector3(posX, posZ, 0f);
                Gizmos.DrawLine(_startPos, new Vector3(posX + m_width, posZ, 0f));
                Gizmos.DrawLine(_startPos, new Vector3(posX, posZ + m_width, 0f));
            }
        }
    }   


}
