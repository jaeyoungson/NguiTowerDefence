using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class FsmState<TFsm_Type>
{   
	private TFsm_Type m_stateType;
	
	public FsmState(TFsm_Type _stateIdx )
	{
        m_stateType = _stateIdx;
	}
	
	public TFsm_Type getStateType
	{
		get 
		{
			return m_stateType;
		}
	}	
	
	#region - virtaul 
	public virtual void Enter()
	{		
	}	
	public virtual void Update()
	{		
	}	
	public virtual void End()
	{		
	}

    public virtual void DrawGizmos()
    {

    }
    #endregion
}
