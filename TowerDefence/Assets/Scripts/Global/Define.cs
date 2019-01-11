using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.U2D;
using System.IO;

namespace Global_Define
{
    static public class Path
    {
        public static readonly string Config_Root;
        public static readonly string GameAtlas_Root;
        public static readonly string Prefabs_Root;
        static Path()
        {
            Config_Root = Directory.GetCurrentDirectory() + "/Configs";
            GameAtlas_Root = "/Resources/GemResources/GameGemAtlas";
            Prefabs_Root = "/Prefabs";
        }
    }
    static public class PlayerKey
    {
        public static readonly string PlayerMoney;

        static PlayerKey()
        {
            PlayerMoney = "PlayerMoney";
        }


    }

    

    // const 값, enum  -----------------------------------------------------------
    
    public enum eConfig
    {
        [Description("TableVersion")]
        TableVersion,
    }

    //public interface IAtlas
    //{
    //    SpriteAtlas GetAtlas(eAtlas a_eAtlas);
    //    Sprite      GetSprite(eAtlas a_eAtlas, string a_strSpriteName);
    //}
    public enum CurrentGameTurn
    {
        None = 0,
        StoneIns,
        SelectKeep,
        MonsterIns,
    }
    public enum GameLogic
    {
        Ready,
        TowerSpawn,
        Place,
        Keep,
        Battle,
        Victory,
        Lose,
    }
    public enum eTable
    {
        [Description("-")]
        None = -1,
        [Description("0")]
        Config,
        [Description("2129960155")]
        Tower,
        [Description("38125113")]
        SpawnTower,
        [Description("135211309")]
        Monster,

        Max,
    }

    public enum AttackType
    {
        None = 0,
        [Description("Ground")]
        Ground =1,
        [Description("Sky")]
        Sky = 2,
        [Description("All")]
        All = 3
    }

    public enum PoolType
    {
        Monster,
        Bullet,
        HpBar,
        Damage,
        ColorEffect,
        IceEffect

    }

    public enum ParticleType
    {
        ColorType,
        Ice,
    }


    public enum MonsterId
    {
        [Description("rk")]
        rk = 1,
        [Description("sk")]
        sk,
        [Description("ek")]
        ek,
        [Description("fk")]
        fk,
        [Description("ak")]
        ak,
        [Description("qk")]
        qk,
        [Description("tk")]
        tk,
        [Description("ak")]
        dk,
        [Description("wk")]
        wk,
        [Description("ck")]
        ck,
        [Description("zk")]
        zk,
        [Description("xk")]
        xk,
        [Description("vk")]
        vk,
        [Description("gk")]
        gk,
        [Description("dl")]
        dl,
        [Description("djf")]
        djf,
        [Description("tks")]
        tks,
        [Description("tm")]
        tm,
        [Description("dh")]
        dh,
        [Description("dldl")]
        dldl,
        [Description("flfl")]
        flfl,
        [Description("oath")]
        oath,
    }

    public enum MonsterType
    {
        None = 0,
        Ground,
        Sky,
    }


    public enum enumTower
    {
        [Description("Stone")]
        Stone = 0,
        [Description("ChipBlack")]
        ChipBlack = 1,
        [Description("ChipGreen")]
        ChipGreen,
        [Description("ChipRed")]
        ChipRed,
        [Description("ChipSky")]
        ChipSky,
        [Description("ChipYellow")]
        ChipYellow,
        [Description("ChipWhite")]
        ChipWhite,
        [Description("FlawedBlack")]
        FlawedBlack = 11,
        [Description("FlawedGreen")]
        FlawedGreen,
        [Description("FlawedRed")]
        FlawedRed,
        [Description("FlawedSky")]
        FlawedSky,
        [Description("FlawedYellow")]
        FlawedYellow,
        [Description("FlawedWhite")]
        FlawedWhite,
        [Description("NormalBlack")]
        NormalBlack = 21,
        [Description("NormalGreen")]
        NormalGreen,
        [Description("NormalRed")]
        NormalRed,
        [Description("NormalSky")]
        NormalSky,
        [Description("NormalYellow")]
        NormalYellow,
        [Description("NormalWhite")]
        NormalWhite,
        [Description("PerfectBlack")]
        PerfectBlack = 31,
        [Description("PerfectGreen")]
        PerfectGreen,
        [Description("PerfectRed")]
        PerfectRed,
        [Description("PerfectSky")]
        PerfectSky,
        [Description("PerfectYellow")]
        PerfectYellow,
        [Description("PerfectWhite")]
        PerfectWhite,
    }

    public enum enumScene
    {
        StartScene = 0,
        SelectScene = 1,
        GameScene = 2,
    }


    public enum GameSceneUIState
    {
        None = 0,
        PlaceStone = 1,
        SelectGemKeep = 2,
        StartRound = 3,
    }

    public enum GemColor
    {
        [Description("Black")]
        Black = 0,
        [Description("Green")]
        Green,
        [Description("Red")]
        Red,
        [Description("Sky")]
        Sky,
        [Description("Yellow")]
        Yellow,
        [Description("White")]
        White,
    }


    public enum GemLevel
    {
        [Description("Chip")]
        Chip = 0,
        [Description("Flawed")]
        Flawed,
        [Description("Normal")]
        Normal,
        [Description("Perfect")]
        Perfect
    }

    public enum AtlasName
    {
        [Description("Bg")]
        Bg = 0,
        [Description("GemAtlas")]
        GemAtlas,
    }

    public enum GameAtlasName
    {
        [Description("GameGemAtlas")]
        GameGemAtlas,
        [Description("BulletAtlas")]
        BulletAtlas,
        [Description("MonsterAtlas")]
        MonsterAtlas,
    }
    
    public enum AudioKinds
    {
        MainMusicVolume,
        UIVolume,
        EffectVolume,
    }

    public enum GameState
    {
        Idle = 0,
        StoneMove,
    }
 //-------------------------------------------interface----------------------------------------------------

}
