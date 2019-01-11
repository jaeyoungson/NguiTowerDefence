using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class FsmClass<TFsm_Type>
{      
    protected SortedDictionary<TFsm_Type, FsmState<TFsm_Type>> m_fsmStateList = new SortedDictionary<TFsm_Type, FsmState<TFsm_Type>>();   
    protected FsmState<TFsm_Type> m_curState = null;    
    protected FsmState<TFsm_Type> m_nextState = null;


    #region - get
    public FsmState<TFsm_Type> getCurState
	{
		get
		{
			return m_curState;
		}
	}
	
	public TFsm_Type getCurStateType
	{
		get
		{			
			if( null == m_curState)
				return default(TFsm_Type);

			return m_curState.getStateType;				
		}
	}
    public FsmState<TFsm_Type> getNextState
	{
		get
		{
			return m_nextState;
		}
	}

	public TFsm_Type getNextStateType
	{
		get
		{
			if( null == m_nextState)
				return default(TFsm_Type);

			return m_nextState.getStateType;
		}
	}
    #endregion

    #region - virtual
    public virtual void Clear()
    {
        m_fsmStateList.Clear();
        m_curState = null;
        m_nextState = null;       
    }

	public virtual void Init()
	{
		m_curState = null;
		m_nextState = null;
	}
	
	public virtual void AddFsm(FsmState<TFsm_Type> _state )
	{
#if UNITY_EDITOR
        if (true == m_fsmStateList.ContainsKey(_state.getStateType))
        {
            Debug.LogError("FsmClass::AddFsm()[ exist id : " + _state.getStateType);
            return;
        }
#endif 

        m_fsmStateList.Add(_state.getStateType, _state );
	}	
	
	public virtual void SetState(TFsm_Type _state)
	{
#if UNITY_EDITOR
        if (false == m_fsmStateList.ContainsKey(_state))
        {
            Debug.LogError("FsmClass::SetState()[ con't find id : " + _state);
            return;
        }
#endif

        m_nextState = m_fsmStateList[_state];	
	}		
	
	public virtual void Update()
	{		
		if( null != m_nextState )
		{
			if( null != m_curState )
                m_curState.End();

            m_curState = m_nextState;
            m_curState.Enter();

            m_nextState = null;			
		}
		
		if( null != m_curState )
            m_curState.Update();
	}		

    public virtual void DrawGizmos()
    {
        if (null != m_curState)
            m_curState.DrawGizmos();
    }
    #endregion

}
