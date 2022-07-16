using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : Singleton<IdleState>, IState<CharacterInstance>
{
    public void OnEnter(CharacterInstance state)
    {
        state.IdleEnter();
    }

    public void OnExecute(CharacterInstance state)
    {
        state.IdleExecute();
    }

    public void OnExit(CharacterInstance state)
    {
    }
}
