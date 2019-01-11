using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class UIMrg : MonoBehaviour {

    #region SINGLETON
    public static bool destroyThis = false;

    static UIMrg _instance = null;

    public static UIMrg Ins
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(UIMrg)) as UIMrg;
                if (_instance == null)
                {
                    _instance = new GameObject("UIMrg", typeof(UIMrg)).GetComponent<UIMrg>();

                }
            }

            return _instance;
        }
    }
    
    #endregion

    private bool loading;
    private bool loadingStart;


    private SelectButton selectSceneButton;
    public GameObject bottonSet;
    public GameObject loadingImage;
    public UISprite loadingBar;
    public GameObject warning;
    public UILabel centerLabel;
    private void Update()
    {
        if(loadingStart)
        {
            loadingBar.fillAmount = TableDownloader.Ins.fPercent;
            if (TableDownloader.Ins.fPercent >= (1f - float.Epsilon) &&
            loading == false)
            {
                loading = true;
                SceneMrg.Ins.ChangeScene(enumScene.GameScene);
            }
        }
        
    }
    public void UIMrgSelectSceneSet()
    {
       
    }

    public void StartGame()
    {
        if(PlayerPrefs.HasKey(PlayerKey.PlayerMoney))
        {
            if (PlayerPrefs.GetInt(PlayerKey.PlayerMoney) == 0)
            {
                loadingImage.SetActive(true);
                TableDownloader.Ins.Download();
                loadingStart = true;
            }
            else
            {
                warning.SetActive(true);
            }
        }
        else
        {
            loadingImage.SetActive(true);
            TableDownloader.Ins.Download();
            loadingStart = true;
        }
    }

    public void Continue()
    {
        if(PlayerPrefs.HasKey(PlayerKey.PlayerMoney))
        {
            loadingImage.SetActive(true);
            TableDownloader.Ins.Download();
            loadingStart = true;
        }
        else
        {
            centerLabel.text = "Have not Save Date";
            centerLabel.gameObject.SetActive(true);
            StartCoroutine( AutoSetActiveFalse(centerLabel.gameObject,0.3f));            
        }

    }

    IEnumerator AutoSetActiveFalse(GameObject target,float duration)
    {
        yield return new WaitForSeconds(duration);
        target.SetActive(false);
    }

    public void SecondGameStart()
    {
        PlayerPrefs.SetInt(PlayerKey.PlayerMoney, 0);
        loadingImage.SetActive(true);
        TableDownloader.Ins.Download();
        loadingStart = true;
    }

    public void SecondGameStartNo()
    {
        warning.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
