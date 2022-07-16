using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T>
{
    void OnEnter(T state);
    void OnExecute(T state);
    void OnExit(T state);
}
