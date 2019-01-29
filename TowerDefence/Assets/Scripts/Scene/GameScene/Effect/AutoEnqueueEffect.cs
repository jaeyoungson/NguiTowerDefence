using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
public class AutoEnqueueEffect : MonoBehaviour
{
    private Effect effect;
    bool enumeratorOn;
    private void Update()
    {
        if(gameObject.activeInHierarchy==true&& enumeratorOn==false)
        {
            StartCoroutine(EffectEnqueue());
        }
    }

    private IEnumerator EffectEnqueue()
    {
        //파티클의 지속시간이 끝나면 queue에  자동으로 들어감
        effect = gameObject.GetComponent<Effect>();
        enumeratorOn =true;
         yield return new WaitForSeconds(effect.particle.main.duration);
        if(effect.particleType == 0)
        {
            PoolManager.Ins.minipools[PoolType.ColorEffect].Enqueue(gameObject);
        }
        else
        {
            PoolManager.Ins.minipools[PoolType.IceEffect].Enqueue(gameObject);
        }
        enumeratorOn = false;
        gameObject.SetActive(false);        
    }
}
