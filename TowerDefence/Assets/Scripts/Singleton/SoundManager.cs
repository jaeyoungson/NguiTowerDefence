using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
public class SoundManager : SingletonBase<SoundManager>
{
    public List<AudioClip> audioList = new List<AudioClip>();
    public float mainMusicVolume = 1.0f;
    public float effectVolume = 1.0f;
    public float uiVolume = 1.0f;

    public override void InitBeforeAwake()
    {
        base.InitBeforeAwake();
    }
    
    public void AudioSetVolume(AudioKinds kinds,float set)
    {
        if(set>1.0f)
        {
            set = 1.0f;
        }

        switch (kinds)
        {
            case AudioKinds.MainMusicVolume:
                mainMusicVolume = set;
                break;
            case AudioKinds.UIVolume:
                uiVolume = set;
                break;
            case AudioKinds.EffectVolume:
                effectVolume = set;
                break;
            default:
                break;
        }
    }

}
