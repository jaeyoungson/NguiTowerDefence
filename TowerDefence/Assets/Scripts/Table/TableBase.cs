using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public abstract class TableBase
{
	public string TbData()
	{
		StringBuilder strReturn = new StringBuilder();

		var arProp = this.GetType().GetProperties();

		for( int i=0; i<arProp.Length; ++i )
		{
			if( arProp[i].PropertyType.IsEnum == true )
			{
				int nVal = (int)(arProp[i].GetValue(this, null));
				strReturn.Append(nVal);
			}
			else
			{
				strReturn.Append(arProp[i].GetValue(this, null));
			}

			if( i != (arProp.Length -1) )
			{
				strReturn.Append(",");
			}
		}

		return strReturn.ToString();
	}
}
