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
    public Stack<ItemInstance> stackBrick = new Stack<ItemInstance>();
    public Transform stackBrickTF;
    [SerializeField] Transform stepRayUpper;
    [SerializeField] Transform stepRayLower;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 2f;
    [SerializeField] float lengthRay = 10f;
    public MapManager MapManager
    {
        get { return currentMap; }
        set { currentMap = value; }
    }
    protected ItemInstance targetItem;
    protected IState<CharacterInstance> curState;

    public virtual void Setup(int type)
    {
        typeCharacter = type;
        renderCharacter.material.color = LevelManager.Instance.Settings.enemyColors[type];
        stepRayUpper.position = new Vector3(stepRayUpper.position.x, stepHeight, stepRayUpper.position.z);
    }

    void stepClimb()
    {
        
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.position, tf.TransformDirection(Vector3.forward), out hitLower, lengthRay,16, QueryTriggerInteraction.Collide))
        {
            Debug.Log("normal");
            RaycastHit hitUpper;
            //if (!Physics.Raycast(stepRayUpper.position, tf.TransformDirection(Vector3.forward), out hitUpper, 0.7f))
            {
                rb.position -= new Vector3(0f, -stepSmooth * Time.fixedDeltaTime, 0f);
            }
        }
        
        RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.position, tf.TransformDirection(1.5f, 0, 1), out hitLower45, lengthRay, 16, QueryTriggerInteraction.Collide))
        {
            Debug.Log(45);
            RaycastHit hitUpper45;
            //if (!Physics.Raycast(stepRayUpper.position, tf.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.7f))
            {
                rb.position -= new Vector3(0f, -stepSmooth * Time.fixedDeltaTime, 0f);
            }
        }
        
        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.position, tf.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, lengthRay, 16, QueryTriggerInteraction.Collide))
        {
            Debug.Log("Minus45");
            RaycastHit hitUpperMinus45;
            //if (!Physics.Raycast(stepRayUpper.position, tf.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.7f))
            {
                rb.position -= new Vector3(0f, -stepSmooth * Time.fixedDeltaTime, 0f);
            }
        }
    }
    protected void Update()
    {
        animator.SetFloat("velocity", rb.velocity.magnitude * 10f);
        if (curState != null)
        {
            curState.OnExecute(this);
        }
        stepClimb();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(stepRayLower.position, tf.TransformDirection(0, 0, 1).normalized * lengthRay);
        Gizmos.DrawRay(stepRayLower.position, tf.TransformDirection(1.5f, 0, 1).normalized * lengthRay);
        Gizmos.DrawRay(stepRayLower.position, tf.TransformDirection(-1.5f, 0, 1).normalized * lengthRay);
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
    public virtual void FallEnter()
    {
        int countStack = stackBrick.Count;
        for (int i = 0; i < countStack; i++)
        {
            ItemInstance brick = stackBrick.Pop();
            brick.tf.parent = SimplePool.Root;
            brick.rb.isKinematic = false;
            brick.rb.AddForce(-brick.tf.forward * 50f + brick.tf.right * Random.Range(-50f, 50f));
            counter.CounterStart(null, () => SimplePool.Despawn(brick), 1f);
        }
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

    public ItemInstance BuildBridge()
    {
        pointCharacter--;
        ItemInstance brick = stackBrick.Pop();
        brick.tf.parent = SimplePool.Root;
        return brick;
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
