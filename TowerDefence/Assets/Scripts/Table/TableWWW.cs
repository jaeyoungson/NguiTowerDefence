using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

// 구글 스프레드시트로 부터 테이블을 생성하는 클래스
// MiniJSON 사용해서 Json데이터를 파싱
// MiniJSON : https://gist.github.com/darktable/1411710

// 메뉴에서 사용하기위해 LocalReq추가

public class TableWWW : MonoBehaviour
{
	const string TableKey = @"194X_HYWMnZC0hcApuBq5rCtsG1kpZrXScrZSCPGYW9E";
	const string strURLBase = @"http://spreadsheets.google.com/a/google.com/tq?key={0}&gid={1}";

	public static void LocalReq<T>(int a_nTableID, List<T> a_refContainer, System.Action<bool> a_fpCallback) where T : TableBase
	{
		ServicePointManager.ServerCertificateValidationCallback = Validator;

		string url = string.Format(strURLBase, TableKey, a_nTableID);

		byte[] data = new WebClient().DownloadData(url);
		MemoryStream ms = new MemoryStream(data);
		StreamReader reader = new StreamReader(ms);

// 		WebRequest req = WebRequest.Create(url);
// 		req.Credentials = CredentialCache.DefaultCredentials;
// 		HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
// 		StreamReader reader = new StreamReader(resp.GetResponseStream());

		string d = reader.ReadToEnd();

		Debug.LogError(d);

		bool bResult = SetData<T>(d, a_refContainer);

		if(a_fpCallback != null)
		{
			a_fpCallback(bResult);
		}

		reader.Close();
		Console.ReadKey();
	}

	public static bool Validator(object sender, X509Certificate certificate, X509Chain chain,
								  SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}

	public void Req<T>(int a_nTableID, List<T> a_refContainer, System.Action<bool> a_fpCallback) where T : TableBase
	{
		StartCoroutine(Request<T>(a_nTableID, a_refContainer, a_fpCallback));
	}

	IEnumerator Request<T>(int a_nTableID, List<T> a_refContainer, System.Action<bool> a_fpOnResponse) where T : TableBase
	{
		bool bResult = true;
		string url = string.Format(strURLBase, TableKey, a_nTableID);

		WWW www = new WWW(url);

		while( www.isDone == false )
		{
			yield return null;
		}

		if( string.IsNullOrEmpty(www.error) == false )
		{
			if( a_fpOnResponse != null )
			{
				Debug.LogError(string.Format("network error - {0}", www.error));
				a_fpOnResponse(false);
				yield break;
			}
		}

		string d = www.text;

		bResult = SetData<T>(d, a_refContainer);
		
		if ( a_fpOnResponse != null )
		{
			a_fpOnResponse(bResult);
		}
	}

	static bool SetData<T>(string s, List<T> a_refContainer) where T : TableBase
	{
		bool bResult = true;

		try
		{
			// 필요없는 문자열 제거
			int nStart = s.IndexOf("(");
			int nEnd = s.IndexOf(");");
			++nStart;

			string data = s.Substring(nStart, nEnd - nStart);

			// 실제 값 파싱
			List<string> liValueName = new List<string>(); // 변수명
			List<List<string>> liValues = new List<List<string>>(); // 변수값

			var mapParsed = MiniJSON.Json.Deserialize(data) as Dictionary<string, object>;
			var map = (Dictionary<string, object>)mapParsed["table"];

			var liName = (List<object>)map["cols"];
			var liVal = (List<object>)map["rows"];

			// 임시 캐싱 : 테이블명
			for (int i = 0; i < liName.Count; ++i)
			{
				var m1 = (Dictionary<string, object>)liName[i];
				liValueName.Add((string)m1["label"]);
			}

			// 임시 캐싱 : 각 로우의 값들
			for (int i = 0; i < liVal.Count; ++i)
			{
				var m2 = (Dictionary<string, object>)liVal[i];
				var li = (List<object>)m2["c"];

				liValues.Add(new List<string>());

				for (int j = 0; j < li.Count; ++j)
				{
					var v = (Dictionary<string, object>)li[j];

					string value = string.Empty;

					if (v.ContainsKey("f") == true)
					{
						value = (string)v["f"];
					}
					else
					{
						value = (string)v["v"];
					}

					if (value == string.Empty)
					{
						Debug.LogError("table error");
					}

					liValues[i].Add(value);
				}
			}

			// 캐싱한 값으로부터 클래스 생성
			int nValCount = liValues.Count;

			for (int i = 0; i < nValCount; ++i)
			{
				T val = (T)GetInstance(typeof(T).FullName, liValues[i].ToArray());
				a_refContainer.Add(val);
			}
		}
		catch (Exception e)
		{
			Debug.LogError(e.Message);
			Debug.LogError("Table Value error");
			bResult = false;
		}

		return bResult;
	}

	// 이름으로부터 클래스 생성 : 이 클래스에는 필요없어서 주석
	// 	public static object GetInstance(string strFullyQualifiedName)
	// 	{
	// 		Type type = Type.GetType(strFullyQualifiedName);
	// 		if (type != null)
	// 			return Activator.CreateInstance(type);
	// 
	// 		foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
	// 		{
	// 			type = asm.GetType(strFullyQualifiedName);
	// 			if (type != null)
	// 				return Activator.CreateInstance(type);
	// 		}
	// 
	// 		return null;
	// 	}

	// 이름, 인자의 string배열로 클래스 생성
	public static object GetInstance(string strFullyQualifiedName, string[] arrArg)
	{
		Type type = Type.GetType(strFullyQualifiedName);
		if (type != null)
			return Activator.CreateInstance(type, arrArg);

		foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
		{
			type = asm.GetType(strFullyQualifiedName);
			if (type != null)
				return Activator.CreateInstance(type, arrArg);
		}

		return null;
	}
}
