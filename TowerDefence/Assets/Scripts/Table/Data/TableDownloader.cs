using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Global_Define;
using System;

public class TableDownloader : MonoBehaviour
{
	#region SINGLETON
	public static bool destroyThis = false;

	static TableDownloader _instance = null;

	public static TableDownloader Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(TableDownloader)) as TableDownloader;
				if (_instance == null)
				{
					_instance = new GameObject("TableDownloader", typeof(TableDownloader)).GetComponent<TableDownloader>();
					
				}
			}

			return _instance;
		}
	}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	#endregion

	int m_nDownloadCount = 0;
	int m_nSuccessCount = 0;
	
	TbDataIO<ConfigTb> m_ConfigTable = new TbDataIO<ConfigTb>();
	Dictionary<Type, Tuple<eTable, object, System.Action<bool>, System.Action<string>>> m_mapDownloadList = new Dictionary<Type, Tuple<eTable, object, Action<bool>, Action<string>>>();

	FileStream fs;
	StreamWriter sw;
	DirectoryInfo di = new DirectoryInfo(Global_Define.Path.Config_Root);
	TableWWW m_TbWWW = null;

	public float fPercent { get { return (float)m_nDownloadCount / (int)eTable.Max; } }
	public int nAllCount { get { return m_mapDownloadList.Count + 1; } }

    public bool tableLoadSuccess=false;

	public bool Download()
	{
		if( m_TbWWW == null )
		{
			m_TbWWW = _instance.gameObject.AddComponent<TableWWW>();
		}

		if ( m_mapDownloadList.Count == 0 ||
			(m_mapDownloadList.Count + 1) != m_nSuccessCount)
		{
			m_mapDownloadList.Clear();


            AddTable<TbDataIO<TowerTb>>(eTable.Tower,
            (a_bSuccess) =>
            {
                ++m_nDownloadCount;

                if (a_bSuccess == true)
                {
                    Debug.LogError("success");
                    ++m_nSuccessCount;

                    var li = TbDataIO<TowerTb>.m_liTb;

                    for (int i = 0; i < li.Count; ++i)
                    {
                        Table<string, TowerTb>.SetTb(li[i].towerName, li[i]);
                    }
                }
            }
            ,
            (strReadLine) =>
            {
                TowerTb towerBase = new TowerTb(strReadLine.Split(','));
                Table<string, TowerTb>.SetTb(towerBase.towerName, towerBase);
            }
            );

            //SpawnTowerTb

            AddTable<TbDataIO<SpawnTowerTb>>(eTable.SpawnTower,
            (a_bSuccess) =>
            {
                ++m_nDownloadCount;

                if (a_bSuccess == true)
                {
                    Debug.LogError("success");
                    ++m_nSuccessCount;

                    var li = TbDataIO<SpawnTowerTb>.m_liTb;

                    for (int i = 0; i < li.Count; ++i)
                    {
                        Table<int, SpawnTowerTb>.SetTb(li[i].upgradeLevel, li[i]);
                    }
                }
            },
              (strReadLine) =>
            {
                SpawnTowerTb spawnBase = new SpawnTowerTb(strReadLine.Split(','));
                Table<int, SpawnTowerTb>.SetTb(spawnBase.upgradeLevel, spawnBase);
            });



            //MonsterTable
            AddTable<TbDataIO<MonsterTb>>(eTable.Monster,
            (a_bSuccess) =>
            {
                ++m_nDownloadCount;

                if (a_bSuccess == true)
                {
                    Debug.LogError("success");
                    ++m_nSuccessCount;

                    var li = TbDataIO<MonsterTb>.m_liTb;

                    for (int i = 0; i < li.Count; ++i)
                    {
                        Table<MonsterId, MonsterTb>.SetTb(li[i].id, li[i]);
                    }
                }
            },
              (strReadLine) =>
            {
                MonsterTb monsterBase = new MonsterTb(strReadLine.Split(','));
                Table<MonsterId, MonsterTb>.SetTb((MonsterId)monsterBase.id, monsterBase);
            });



            // Download Start ----------------------------------------------------------------
            StartCoroutine(Down());
			return true;
		}

		return false;
	}

	void AddTable<Tb>(eTable a_eTb, System.Action<bool> a_refRequestCallback, System.Action<string> a_refReadLineCallback) where Tb : new()
	{
		m_mapDownloadList.Add(typeof(Tb), new Tuple<eTable, object, System.Action<bool>, System.Action<string>>(a_eTb, new Tb(), a_refRequestCallback, a_refReadLineCallback));
	}
	
	IEnumerator Down()
	{
		if (!di.Exists)
		{
			di.Create();
			Debug.Log("folder nothing");

			m_ConfigTable.Req(m_TbWWW, 
				(a_bSuccess)=>
				{
					++m_nDownloadCount;

					if( a_bSuccess == true ) { ++m_nSuccessCount; }
				}
			);

			while (m_nDownloadCount != 1)
				yield return null;

			AllRequest();

			while (m_nDownloadCount != nAllCount)
				yield return null;
                        
			m_ConfigTable.FileWrite();
			AllFileSave();
            tableLoadSuccess = true;
        }
		else
		{
			Debug.Log("folder exist");

			string oldConfig;
			m_ConfigTable.Req(m_TbWWW,
				(a_bSuccess) =>
				{
					++m_nDownloadCount;

					if( a_bSuccess == true ) { ++m_nSuccessCount; }
				}
			);

			while (m_nDownloadCount != 1)
				yield return null;

			fs = new FileStream(m_ConfigTable.strFileName, FileMode.Open);
			StreamReader sr = new StreamReader(fs);
			oldConfig = sr.ReadLine();
			sr.Close(); sr = null;
			fs.Close(); fs = null;

			//문자열 비교

			var tbData = TbDataIO<ConfigTb>.m_liTb[(int)eConfig.TableVersion];
			string strCheck = string.Format("{0},{1}", tbData.strConfigID, tbData.fValue);
			
			if ( oldConfig != strCheck )
			{
				Debug.Log("config version change");
				AllRequest();

				while (m_nDownloadCount != nAllCount)
					yield return null;

				m_ConfigTable.FileWrite();
				AllFileSave();

				Debug.Log("config version update success");
                tableLoadSuccess = true;
			}
			else
			{
				Debug.Log("Version same");
				AllFileRead();

				m_nSuccessCount = nAllCount;
				m_nDownloadCount = nAllCount;
			}
		}
	}

	void AllRequest()
	{
		foreach (var val in m_mapDownloadList.Values)
		{
			ITbIO io = (ITbIO)val.Item2;

			io.Req(m_TbWWW,
				val.Item3
			);
		}
	}

	void AllFileSave()
	{
		foreach(var val in m_mapDownloadList.Values)
		{
			ITbIO io = (ITbIO)val.Item2;
			io.FileWrite();
		}
	}

	public void AllFileRead()
	{
		foreach (var val in m_mapDownloadList.Values)
		{
			ITbIO io = (ITbIO)val.Item2;

			io.FileRead(val.Item4);
		}
        Debug.Log("All File Load");
        tableLoadSuccess = true;
	}

}
