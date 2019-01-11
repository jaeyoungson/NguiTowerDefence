using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;
using System;
using Global_Define;

public class TableMenu : MonoBehaviour
{
	const string strMenu = "Table";

	static Dictionary<Type, Tuple<eTable, object, System.Action<bool>>> m_mapDownloadList = new Dictionary<Type, Tuple<eTable, object, Action<bool>>>();

	[MenuItem(strMenu + "/Download Table")]
	#region TABLE_DOWNLOAD
	static public void DownLoadTable()
	{
		Debug.ClearDeveloperConsole();

		m_mapDownloadList.Clear();

		int nDownloadCount = 0;
		int nSuccessCount = 0;

		DirectoryInfo di = new DirectoryInfo(Global_Define.Path.Config_Root);

		if( di.Exists == false )
		{
			di.Create();
		}

		// Config Table ---------------------------------------------------------------------------------
		AddTable<TbDataIO<ConfigTb>>(eTable.Config,
		(a_bSuccess) =>
		{
			++nDownloadCount;

			if (a_bSuccess == true)
			{
				Debug.LogError(string.Format("success - {0}", "ConfigTb"));

				++nSuccessCount;

				var li = TbDataIO<ConfigTb>.m_liTb;
				for (int i = 0; i < li.Count; ++i)
				{
					Table<eConfig, ConfigTb>.SetTb((eConfig)Enum.Parse(typeof(eConfig), li[i].strConfigID), li[i]);
				}
			}
		});

        AddTable<TbDataIO<TowerTb>>(eTable.Tower,
        (a_bSuccess) =>
        {
            ++nDownloadCount;

            if (a_bSuccess == true)
            {
                Debug.LogError("success");
                ++nSuccessCount;

                var li = TbDataIO<TowerTb>.m_liTb;

                for (int i = 0; i < li.Count; ++i)
                {
                    Table<string, TowerTb>.SetTb(li[i].towerName, li[i]);
                }
            }
        });

        //SpawnTowerTb

        AddTable<TbDataIO<SpawnTowerTb>>(eTable.SpawnTower,
        (a_bSuccess) =>
        {
            ++nDownloadCount;

            if (a_bSuccess == true)
            {
                Debug.LogError("success");
                ++nSuccessCount;

                var li = TbDataIO<SpawnTowerTb>.m_liTb;

                for (int i = 0; i < li.Count; ++i)
                {
                    Table<int, SpawnTowerTb>.SetTb(li[i].upgradeLevel, li[i]);
                }
            }
        });



        //MonsterTable
        AddTable<TbDataIO<MonsterTb>>(eTable.Monster,
        (a_bSuccess) =>
        {
            ++nDownloadCount;

            if (a_bSuccess == true)
            {
                Debug.LogError("success");
                ++nSuccessCount;

                var li = TbDataIO<MonsterTb>.m_liTb;

                for (int i = 0; i < li.Count; ++i)
                {
                    Table<MonsterId, MonsterTb>.SetTb(li[i].id, li[i]);
                }
            }
        });

        AllRequest();

		while( nDownloadCount != m_mapDownloadList.Count ) { }

		if( nDownloadCount != nSuccessCount ) { Debug.LogError("download error"); return; }

		AllFileSave();
	}

	static void AddTable<Tb>(eTable a_eTb, System.Action<bool> a_refRequestCallback) where Tb : new()
	{
		m_mapDownloadList.Add(typeof(Tb), new Tuple<eTable, object, System.Action<bool>>(a_eTb, new Tb(), a_refRequestCallback));
	}

	static void AllRequest()
	{
		foreach (var val in m_mapDownloadList.Values)
		{
			ITbIO io = (ITbIO)val.Item2;
			io.LocalReq(val.Item3);
		}

		Debug.LogError("AllRequest");
	}

	static void AllFileSave()
	{
		foreach (var val in m_mapDownloadList.Values)
		{
			ITbIO io = (ITbIO)val.Item2;
			io.FileWrite();
		}

		Debug.LogError("AllFileSave");
	}

	#endregion TABLE_DOWNLOAD

}
