using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
using UnityEngine.SceneManagement;

public class SceneMrg : MonoBehaviour
{
    #region SINGLETON
    public static bool destroyThis = false;

    static SceneMrg _instance = null;

    public static SceneMrg Ins
    {
        get
        {
            if(_instance==null)
            {
                _instance = FindObjectOfType(typeof(SceneMrg)) as SceneMrg;
                if(_instance ==null)
                {
                    _instance = new GameObject("SceneMrg", typeof(SceneMrg)).GetComponent<SceneMrg>();

                }
            }

            return _instance;
        }
    }

    private void Awake()
    {

        DontDestroyOnLoad(this);
    }
    #endregion

    Scene refScene = null;


    public enumScene CurrentScene
    {
        get { return refScene.m_Scene; }
    }

    public void ChangeScene(enumScene changeScene)
    {
        if(refScene == null) { return;}

        refScene = null;

        System.GC.Collect();

        SceneManager.LoadScene((int)changeScene);
    }

    public void SetScene(Scene a_refScene)
    {
        System.GC.Collect();
        refScene = a_refScene;
    }


}