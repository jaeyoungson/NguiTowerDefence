using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
using UnityEngine.U2D;
using System;

public class GameMrg :SingletonBase<GameMrg>
{
    // GameManager property
    [HideInInspector]
    public int currentRound=0;
    [HideInInspector]
    public CurrentGameTurn currentGameTurn;
    [HideInInspector]
    public int gemInstantCount=0;
    [HideInInspector]
    public List<Tower> createTower = new List<Tower>();
    [HideInInspector]
    public int currentGemUpgrade = 0;
    public int life=3;
    public int monsterCheck = 0;
    [HideInInspector]
    public int money;
    //spawnrandom
    public SpawnRandom spawnRandom = new SpawnRandom();
    //Summon stone Count
    public const int MaxGemInstance = 5;

    //Instant Gem 
    public GameObject stonePrefab;
    public Transform StoneTr;
    public GameObject stone;
    public List<GameObject> createTowerList = new List<GameObject>();

    //Combine
    public Tower selectTower;

    public bool isMoveTower;

    //monster Ins
    public Transform startPosition;

    private FsmClass<GameLogic> m_logic = new FsmClass<GameLogic>();

    public FsmClass<GameLogic> getLogic { get
        {
            return m_logic;
        }
    }

    [HideInInspector]
    public List<GameObject> monsterList = new List<GameObject>();

 

  
    public override void InitBeforeAwake()
    {
        base.InitBeforeAwake();
    }

    override public void InitAftwerAwake()
    {
        base.InitAftwerAwake();
        PlayerMoneyLoad();
        if(TableDownloader.Ins.tableLoadSuccess==false)
        {
            TableDownloader.Ins.Download();
        }
        m_logic.AddFsm(new Battle());
        m_logic.AddFsm(new TowerSpawn());
        m_logic.AddFsm(new Place());
        m_logic.AddFsm(new Ready());
        m_logic.AddFsm(new Victory());
        m_logic.AddFsm(new Lose());
        m_logic.AddFsm(new Keep());
        m_logic.SetState(GameLogic.Ready);

    }

    public void CreateTowerSort()
    {
        List<GameObject> towers = new List<GameObject>();
        
    }

    public void PlayerMoneySave()
    {
        PlayerPrefs.SetInt(PlayerKey.PlayerMoney,money);
    }

    public void PlayerMoneyLoad()
    {
       money= PlayerPrefs.GetInt(PlayerKey.PlayerMoney);
    }

    public void SetSpawnRandom()
    {
        if (spawnRandom.setRandom == false)
        {
            spawnRandom.SetTablePercentage();
        }
        else
        {
            ReSetSpawnRadom();
        }


    }

    public void ReSetSpawnRadom()
    {
        spawnRandom.ReSetTablePercentage();
    }

    private void Update()
    {
        m_logic.Update();
    }

    //InstaceGem
    public void InstanceGem()
    {
        if (GameMrg.Ins.gemInstantCount < MaxGemInstance)
        {
            stone = Instantiate(stonePrefab, StoneTr);
            gemInstantCount++;
            isMoveTower = true;
        }
        else
        {
            GameSceneUI.Ins.OverSummonCount();
        }
    }
    
    //stone에게 tower에 table값을 넣어줌
    public void StonetoTower(string name)
    {
        Tower stoneTower = stone.GetComponent<Tower>();

        stoneTower.createTowerNumber = gemInstantCount - 1;

        stoneTower.SetTowerData(ExtensionMethod.GetTowerTb(name), gemInstantCount - 1);

        //createtower 추가하고 GameSceneUi의 sprite를 바꿔준다
        createTowerList.Add(stone);
        createTower[gemInstantCount-1] = stoneTower;

        ChangeItemUiSprite();
    }

    public void ChangeItemUiSprite()
    {
        GameSceneUI.Ins.uiPlace.itemSprites[gemInstantCount - 1].gameObject.SetActive(true);
        GameSceneUI.Ins.uiPlace.itemSprites[gemInstantCount - 1].spriteName = createTower[gemInstantCount - 1].towerName;
    }


    public void Combine()
    {
        int level = selectTower.eId / 10;
        if(level>2)
        {
            GameSceneUI.Ins.centerUiLabel.text = "Maximum Level Tower";
            GameSceneUI.Ins.centerUiLabel.gameObject.SetActive(true);
            StartCoroutine( GameSceneUI.Ins.GameObjectWaitSetFalse(GameSceneUI.Ins.centerUiLabel.gameObject));
        }
        else
        {
            int towerKind = selectTower.eId % 10;

            int levelUpTowerId = ((level + 1) * 10) + towerKind;

            //leveluptower로 define에서 찾아서 todesc로 string 값변경후 selecttower의 정보변경후 나머지는 그냥 돌덩이로 바꿈
            enumTower towerId = (enumTower)levelUpTowerId;
            string towerIdToDesc = towerId.ToDesc();
            var tbData = ExtensionMethod.GetTowerTb(towerIdToDesc);
            selectTower.eId = (int)tbData.eID;
            selectTower.damage = tbData.damage;
            selectTower.attackSpeed = tbData.attackSpeed;
            selectTower.range = tbData.range;
            selectTower.towerName = tbData.towerName;
            selectTower.bulletSpeed = tbData.bulletSpeed;
            selectTower.levelUpcost = tbData.levelUpcost;
            selectTower.isCombineTower = false;
            ExtensionMethod.GameGemSpriteChange(selectTower.towerName, selectTower.gameObject);
            OtherTowerChangeStone();
            GameSceneUI.Ins.infoUI.SetInfoUI();
            GameSceneUI.Ins.uiPlace.gameObject.SetActive(false);
            StartCoroutine(SpawnMonster());
            getLogic.SetState(GameLogic.Battle);
        }

    }

    private void OtherTowerChangeStone()
    {
        for(int i=0;i<createTower.Count;i++)
        {
            if(selectTower.createTowerNumber != i&& createTower[i] !=null)
            {
                var tbData = ExtensionMethod.GetTowerTb(enumTower.Stone.ToDesc());
                createTower[i].eId = (int)tbData.eID;
                createTower[i].damage = tbData.damage;
                createTower[i].range = tbData.range;
                createTower[i].towerName = tbData.towerName;
                createTower[i].attackSpeed = tbData.attackSpeed;
                createTower[i].levelUpcost = tbData.levelUpcost;
                createTower[i].isCombineTower = false;
                ExtensionMethod.GameGemSpriteChange(createTower[i].towerName, createTower[i].gameObject);

            }
        }
    }

    public bool isCombine(Tower selectTower)
    {
        bool returnValue = false;

        if (selectTower.isCombineTower = true)
        {
            for (int i = 0; i < createTower.Count; i++)
            {
                if (selectTower.createTowerNumber != i&&createTower[i]!=null)
                {
                    if (createTower[i].eId == selectTower.eId)
                    {
                        returnValue = true;
                    }
                    else
                    {
                        continue;
                    }

                }
            }
        }
        else
        {
            returnValue = false;
        }
        return returnValue;
    }

    public void Keep()
    {
        if (selectTower != null)
        {
            OtherTowerChangeStone();
            GameSceneUI.Ins.uiPlace.gameObject.SetActive(false);
            getLogic.SetState(GameLogic.Battle);
            StartCoroutine(SpawnMonster());
        }
        else
        {
            GameSceneUI.Ins.SelectTower();
        }
    }


    public IEnumerator SpawnMonster()
    {
        for (int i = 0; i < 30; i++)
        {

           GameObject monster = PoolManager.Ins.GetPoolObject(PoolType.Monster);
           monsterList.Add(monster);
           monster.transform.position=startPosition.transform.position;

           Monster data = monster.GetComponent<Monster>();
            //몬스터 데이터 세팅
           MonsterLevelSet(data);
            
           monster.SetActive(true);

           yield return new WaitForSeconds(0.8f);
        }
    }
    
   private void MonsterLevelSet(Monster target)
   {
        MonsterTb copyData = ExtensionMethod.GetMonsterTb((MonsterId)currentRound);
        target.maxHp = copyData.hp;
        target.hp = copyData.hp;
        target.id = copyData.id;
        target.name = copyData.name;
        target.moveSpeed = copyData.moveSpeed;
        target.targetType = copyData.targetType;
        target.monsterPoint = copyData.monsterPoint;
        ExtensionMethod.MonsterSpriteChange(copyData.name, target.monsterSprite);
        if (target.isDie == true)
        {
            target.isDie = false;
        }
        Vector3Int startPosition = GridTile.Ins.GetStartPosition();
        target.transform.position = GridTile.Ins.pathfinder.nodeGroup.GetPosition(startPosition.x, startPosition.y);
        target.move.SetMove(GridTile.Ins.SetMovePath().ToArray(), 0);
    }

    public void GameMrgReSet()
    {
        StopAllCoroutines();
        getLogic.SetState(GameLogic.Ready);        
        currentRound = 0;
        gemInstantCount = 0;
        monsterCheck = 0;
        life = 30;
        createTower = null;
        currentGemUpgrade = 0;
        monsterCheck = 0;
        GameSceneUI.Ins.LifeReset();
        CreateListReset();
    }
    private void MonsterListReSet()
    {
        for(int i =0; i<monsterList.Count;i++)
        {
            PoolManager.Ins.minipools[PoolType.Monster].Enqueue(monsterList[i]);
            monsterList[i].SetActive(false);
        }
    }

    private void CreateListReset()
    {
        for(int i =0; i<createTowerList.Count; i++)
        {
            Destroy(createTowerList[i]);
        }
    }
}
