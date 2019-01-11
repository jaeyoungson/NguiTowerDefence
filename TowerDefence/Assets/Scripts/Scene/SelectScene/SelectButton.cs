using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
public class SelectButton : MonoBehaviour {

    public void ClickStartGameButton()
    {
        UIMrg.Ins.StartGame();

    }
   
    public void ContinueGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
