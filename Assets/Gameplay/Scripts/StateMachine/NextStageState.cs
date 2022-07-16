using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageState : Singleton<NextStageState>, IState<CharacterInstance>
{
    public void OnEnter(CharacterInstance state)
    {
        state.NextStageEnter();
    }

    public void OnExecute(CharacterInstance state)
    {
        state.NextStageExecute();
    }

    public void OnExit(CharacterInstance state)
    {
        state.NextStageExit();
    }
}
