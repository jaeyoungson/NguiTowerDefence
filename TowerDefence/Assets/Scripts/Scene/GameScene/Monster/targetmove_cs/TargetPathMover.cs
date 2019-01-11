using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetPathMover : TargetMove
{
	int m_pathIndex = 0;	
	protected Vector3[] m_pathList;	

	virtual public void SetMove(Vector3[] _list, int _pathIdx = 0)
	{
		if (_list == null )
			return;

		if (_list.Length <= 0)
			return;

		m_pathIndex = _pathIdx;
		m_pathList = _list;
		m_target = m_pathList[m_pathIndex];
		m_isStop = false;		
	}

	override public Vector3 GetPos(
        Vector3 _pos, 
        float _speed, 
        eMOVE_OUT_TYPE _outType = eMOVE_OUT_TYPE.POS, 
        eIGNORE_AXIS _axis = eIGNORE_AXIS.NONE)
	{
        if( m_pathList == null )
        {
            if (_outType == eMOVE_OUT_TYPE.DELTA)
                return Vector3.zero;
            return _pos;
        }


		float _fMoveSpeed = _speed * Time.deltaTime;
        m_target = GetIgnoreAxis(m_target, _axis);
        _pos = GetIgnoreAxis(_pos, _axis);  
		Vector3 vec3Movement = m_target - _pos;

		if (_fMoveSpeed < vec3Movement.magnitude)
		{
			Vector3 _delta = vec3Movement.normalized * _fMoveSpeed;
			if (_outType == eMOVE_OUT_TYPE.DELTA)
			{
				return _delta;
			}

			return _pos + _delta;
		}
		else
		{
			Vector3 _delta = vec3Movement.normalized * _fMoveSpeed;
			++m_pathIndex;
			if (m_pathList.Length > m_pathIndex)
			{
				m_target = m_pathList[m_pathIndex];
			}
			else
			{
				m_isStop = true;
			}

			if (_outType == eMOVE_OUT_TYPE.DELTA)
			{
				return _delta;
			}

			return _pos + _delta;
		}
	}	
}