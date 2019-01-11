using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopUI : MonoBehaviour
{
    public UILabel roundUI;
    public UILabel lifeUI;
    public GameObject pauseButton;
    public GameObject settingPanel;
    
    public void PauseOpen()
    {
        Time.timeScale = 0;
        settingPanel.gameObject.SetActive(true);
    }
}
