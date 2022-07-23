using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : CharacterInstance
{    
    public NavMeshAgent agent;

    public override void FallEnter()
    {
        agent.enabled = false;
        base.FallEnter();
    }
    #region Idle

    public override void IdleAction()
    {
        agent.enabled = false;
        float randomvalue = Random.Range(0f, 0.7f);
        if (randomvalue < 0.7f)
        {
            targetItem = currentMap.GetNearestIntance(typeCharacter, tf);
            if (targetItem == null || !targetItem.gameObject.activeSelf)
            {
                ChangeState(IdleState.Instance);
            }
            else
            {
                ChangeState(FindState.Instance);
            }
            //OnChangeAnim(ConstantManager.ANIM_RUN);
        }
        else
        {
            ChangeState(NextStageState.Instance);
        }
    }
    #endregion
    #region Find
    public override void FindAction()
    {
        base.FindAction();
        if (targetItem == null || !targetItem.gameObject.activeSelf || targetItem.IsLooted)
        {
            ChangeState(IdleState.Instance);
        }
        else
        {
            tf.LookAt(targetItem.tf);
            agent.SetDestination(targetItem.tf.position);
        }
    }

    public override void FindEnter()
    {
        if (targetItem == null || !targetItem.gameObject.activeSelf || targetItem.IsLooted)
        {
            ChangeState(IdleState.Instance);
        }
        agent.enabled = true;
        
        //OnChangeAnim(ConstantManager.ANIM_RUN);
        base.FindEnter();
    }
    #endregion
    #region Next Stage
    public override void NextStageExecute() { }
    public override void NextStageEnter() 
    {
        
    }
    public override void NextStageExit() { }
    #endregion
}
