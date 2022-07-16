using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : Singleton<FallState>, IState<CharacterInstance>
{
    public void OnEnter(CharacterInstance state)
    {
        state.FallEnter();
    }

    public void OnExecute(CharacterInstance state)
    {
        state.FallExecute();
    }

    public void OnExit(CharacterInstance state)
    {
    }
}
