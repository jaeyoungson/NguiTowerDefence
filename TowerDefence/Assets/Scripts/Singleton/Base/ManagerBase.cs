using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase<T> : SingletonBase<T>
                              where T: class,new()
{
    new protected void Awake()
    {
        DontDestroyOnLoad(this);

        base.Awake();
    }
}
