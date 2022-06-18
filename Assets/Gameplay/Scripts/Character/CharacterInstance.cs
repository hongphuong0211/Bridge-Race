using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInstance : MonoBehaviour
{
    public int typeCharacter;
    public int pointCharacter;
    public Animator animator;
    private StateCharacter curState;

    protected virtual void OnInit()
    {

    }
    protected virtual void OnEnable()
    {

    }
    protected virtual void OnDisable()
    {

    }
    private void Update()
    {
        if(curState == StateCharacter.Find)
        {
            Find();
        }else if(curState == StateCharacter.NextStage)
        {
            NextStage();
        }
    }
    protected virtual void Find() { }
    protected virtual void NextStage() { }
    protected virtual void StartFind() { }
    protected virtual void StartNextStage() { }
    protected virtual void StartThrowBack() 
    {
    
    }
    public void ChangeState(StateCharacter newState)
    {
        if(curState != newState)
        {
            switch (newState)
            {
                case StateCharacter.Find:
                    StartFind();
                    break;
                case StateCharacter.NextStage:
                    StartNextStage();
                    break;
                case StateCharacter.ThrowBack:
                    StartThrowBack();
                    break;
            }
            curState = newState;
        }
    }
    public bool IsState(StateCharacter state)
    {
        return curState == state;
    }

}
