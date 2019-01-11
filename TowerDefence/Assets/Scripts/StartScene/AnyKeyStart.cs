using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
public class AnyKeyStart : MonoBehaviour {
    
	void Update () {
		if(Input.anyKeyDown)
        {
            SceneMrg.Ins.ChangeScene(enumScene.SelectScene);
        }
	}
}
