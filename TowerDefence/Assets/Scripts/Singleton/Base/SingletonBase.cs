using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBase<T> : MonoBehaviour
                                where T : class,new()
{
    static protected T _instance = null;

    public static T Ins
    {
        get
        {

            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if(_instance ==null)
                {
                    _instance = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>();

                    var v = _instance as SingletonBase<T>;
                    v.InitBeforeAwake();
                }

            } 
            return _instance;
        }
    }

    protected void Awake()
    {
        InitAftwerAwake();
    }

    virtual public void InitBeforeAwake()
    {
        _instance = FindObjectOfType(typeof(T)) as T;
    }

    virtual public void InitAftwerAwake()
    {
        _instance = FindObjectOfType(typeof(T)) as T;
    }
    
}
