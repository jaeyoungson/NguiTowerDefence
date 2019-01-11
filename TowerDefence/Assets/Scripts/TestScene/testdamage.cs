using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class testdamage : MonoBehaviour {

    private void Start()
    {
        StartCoroutine(Wait());
    }



    IEnumerator Wait()
    {

        for (int i = 0; i < 10000; i++)
        {

            Ins();
            yield return new WaitForSeconds(0.4f);
        }
    }

    private void Ins()
    {
        GameObject damageUI=PoolManager.Ins.minipools[PoolType.Damage].Dequeue();
        DamageUI ui=damageUI.GetComponent<DamageUI>();
        damageUI.gameObject.SetActive(true);

        //StartCoroutine(ui.TweeningEnd());        
        ui.TweenStart();        
        
    }
}
