using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class ConfigTb : TableBase
{
	public string strConfigID { get; private set; }
	public float fValue { get; private set; }

	public ConfigTb(string s, string v)
	{
		strConfigID = s;
		fValue = float.Parse(v);
	}

	public ConfigTb(string[] a_Val) : this(a_Val[0], a_Val[1])
	{
	}
}
