using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindState : Singleton<FindState>, IState<CharacterInstance>
{
    public void OnEnter(CharacterInstance state)
    {
        state.FindEnter();
    }

    public void OnExecute(CharacterInstance state)
    {
        state.FindExecute();
    }

    public void OnExit(CharacterInstance state)
    {
    }
}
