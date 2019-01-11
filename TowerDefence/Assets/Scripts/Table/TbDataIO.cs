using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public interface ITbIO
{
	void Req(TableWWW a_refTb, System.Action<bool> a_refCallback);
	void FileWrite();
	void FileRead(System.Action<string> a_refAddCallback);
	void LocalReq(System.Action<bool> a_refCallback);
}

public class TbDataIO<TB> : ITbIO
							where TB : TableBase
							
{
	public static List<TB> m_liTb = new List<TB>();
	public Global_Define.eTable m_eValue = Global_Define.eTable.None;

	public string strExtension
	{
		get { return "csv"; }
	}

	public string strFileName
	{
		get
		{
			return string.Format(@"{0}\{1}.{2}", Global_Define.Path.Config_Root, typeof(TB).Name, strExtension);
		}
	}

	public void Req(TableWWW a_refTb, System.Action<bool> a_refCallback)
	{
		a_refTb.Req<TB>(int.Parse(typeof(TB).TableDescription()), m_liTb, a_refCallback);
	}

	public void LocalReq(System.Action<bool> a_refCallback)
	{
		TableWWW.LocalReq<TB>(int.Parse(typeof(TB).TableDescription()), m_liTb, a_refCallback);
	}

	public void FileWrite()
	{
		FileStream fs = new FileStream(strFileName, FileMode.Create);
		StreamWriter sw = new StreamWriter(fs);

		for (int i = 0; i < m_liTb.Count; i++)
		{
			sw.WriteLine(m_liTb[i].TbData());
		}

		sw.Close(); sw = null;
		fs.Close(); fs = null;
	}

	public void FileRead(System.Action<string> a_refAddCallback)
	{
		string readstr;
		FileStream fs = new FileStream(strFileName, FileMode.Open);
		StreamReader sr = new StreamReader(fs);

		while ((readstr = sr.ReadLine()) != null)
		{
			a_refAddCallback(readstr);
		}

		sr.Close(); sr = null;
		fs.Close(); fs = null;
	}
}
