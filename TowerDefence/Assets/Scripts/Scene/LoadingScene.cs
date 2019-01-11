using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
	#region INSPECTOR

	public bool			m_bLoading = false;

	#endregion
	
	void Start()
	{
		TableDownloader.Ins.Download();
	}

	private void Update()
	{
		if (TableDownloader.Ins.fPercent >= ( 1f - float.Epsilon ) &&
			m_bLoading == false )
		{
			m_bLoading = true;
		}
	}
}
