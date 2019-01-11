using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Table<K, V> // Key, Value
{
	static Dictionary<K, V> m_mapTb = new Dictionary<K, V>();

	public static Dictionary<K, V> GetTable()
	{
		return m_mapTb;
	}

	public static void SetTb(K a_key, V a_val)
	{
		try
		{
			m_mapTb.Add(a_key, a_val);
		}
		catch
		{
			Debug.LogError("fatal error!!! ----- check table");
		}
	}

	public static V GetTb(K a_key)
	{
		V returnVal;

		m_mapTb.TryGetValue(a_key, out returnVal);

		return returnVal;
	}
}
