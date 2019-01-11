using UnityEngine;
using System.Collections;

public enum eMOVE_OUT_TYPE
{
	POS,
	DELTA,
}

public enum eIGNORE_AXIS
{
    NONE,
    X,
    Y,
    Z
}

public class TargetMove
{	
	protected bool m_isStop = true;
	protected Vector3 m_target;

    static public Vector3 GetIgnoreAxis( Vector3 _pos, eIGNORE_AXIS _axis )
    {
        switch (_axis)
        {
            case eIGNORE_AXIS.X:
                _pos.x = 0f;
                break;

            case eIGNORE_AXIS.Y:               
                _pos.y = 0f;
                break;

            case eIGNORE_AXIS.Z:              
                _pos.z = 0f;
                break;
        }

        return _pos;
    }

	public bool isStop
	{
		get
		{
			return m_isStop;
		}
        set
        { m_isStop = value; }
	}
        


	virtual public void SetMove( Vector3 _target )
	{
		m_target = _target;
		m_isStop = false;
    }	
	
	virtual public Vector3 GetPos(
        Vector3 _pos, 
        float _speed, 
        eMOVE_OUT_TYPE _outType = eMOVE_OUT_TYPE.POS, 
        eIGNORE_AXIS _axis = eIGNORE_AXIS.NONE)
	{
		if( true == m_isStop)
			return _pos;
		
		float _fMoveSpeed = _speed * Time.deltaTime;
        m_target = GetIgnoreAxis(m_target, _axis);
        _pos = GetIgnoreAxis(_pos, _axis);

        Vector3 vec3Movement = m_target - _pos;		
		if( _fMoveSpeed < vec3Movement.magnitude )
		{
			Vector3 _delta = vec3Movement.normalized * _fMoveSpeed;
            if (_outType == eMOVE_OUT_TYPE.DELTA )
			{
				return _delta;
			}
			            
			return _pos + _delta;			
		}
		else
		{
			m_isStop = true;
			Vector3 _delta = vec3Movement;
			if (_outType == eMOVE_OUT_TYPE.DELTA)
			{
				return _delta;
			}
			return _pos + _delta;
		}		
	}
}
