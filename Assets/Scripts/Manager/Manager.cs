using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager<T> : Monosingleton<Manager<T>>
{
    protected abstract void Awake();
    protected abstract void Start();
    protected virtual void Update() { }
    protected virtual void LateUpdate() { }
}
