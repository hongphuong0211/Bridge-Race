using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInstance : GameUnit
{
    public int typeCharacter;
    public int pointCharacter;
    public Animator animator;
    public Rigidbody rb;
    protected MapManager currentMap;
    public SkinnedMeshRenderer renderCharacter;
    public MapManager MapManager
    {
        set { currentMap = value; }
    }
    protected ItemInstance targetItem;
    protected IState<CharacterInstance> curState;

    public virtual void Setup(int type)
    {
        typeCharacter = type;
        renderCharacter.material.color = LevelManager.Instance.Settings.enemyColors[type];
    }
    private void Update()
    {
        animator.SetFloat("velocity", rb.velocity.magnitude * 10f);
        if (curState != null)
        {
            curState.OnExecute(this);
        }
    }
    #region Idle
    public virtual void IdleExecute()
    {
        IdleAction();
    }
    public virtual void IdleEnter()
    {
        animator.SetBool(ConstantManager.ANIM_THROW, false);
    }

    public virtual void IdleAction()
    {

    }
    #endregion
    #region Find
    public virtual void FindExecute() {
        FindAction();
    }
    
    public virtual void FindEnter() {
    }
    public virtual void FindAction()
    {
        
    }
    #endregion
    #region Next Stage
    public virtual void NextStageExecute() { }
    public virtual void NextStageEnter() { }
    public virtual void NextStageExit() { }
    #endregion

    public void ChangeState(IState<CharacterInstance> newState)
    {
        if(curState != null)
        {
            curState.OnExit(this);
        }
        curState = newState;
        if(curState != null)
        {
            curState.OnEnter(this);
        }
    }
    public bool IsState(IState<CharacterInstance> state)
    {
        return curState == state;
    }
    private CounterTime counter = new CounterTime();
    public void FallEnter()
    {
        pointCharacter = 0;
        OnChangeAnim(ConstantManager.ANIM_THROW);
        counter.CounterStart(null, Respawn, 2f);
    }
    public void FallExecute()
    {
        counter.CounterExecute();
    }

    private void Respawn()
    {
        animator.SetBool(ConstantManager.ANIM_THROW, false);
        ChangeState(IdleState.Instance);
    }
    public void OnChangeAnim(string animName)
    {
        animator?.SetTrigger(animName);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            CharacterInstance otherCInstance = LevelManager.Instance.GetCharacterInstance(collision.collider);
            if (otherCInstance.pointCharacter > pointCharacter)
            {
                ChangeState(FallState.Instance);
            }
        }
    }
}
