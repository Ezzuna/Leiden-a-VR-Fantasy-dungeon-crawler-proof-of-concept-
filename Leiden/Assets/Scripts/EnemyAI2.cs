using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI2 : Enemy_Stats
{

    private GameObject playerUnit;          //Get player units

    private Vector3 initialPosition;            //initial position
    private UnityEngine.AI.NavMeshAgent nav;
    public float wanderRadius;          //Walking radius, in the moving state, if it exceeds the walking radius, it will return to the birth position
    public float alertRadius;         //Alert radius, the monster will warn when the player enters, and will always face the player
    public float defendRadius;          //Self-defense radius. When the player enters, the monster will chase the player. When the distance is less than the attack distance, he will attack (or trigger a battle).
    public float chaseRadius;            //Chase radius. When the monster exceeds the chase radius, it will give up the chase and return to the starting position of chase.

    public float attackRange;            //Attack distance
    public float walkSpeed;          //Moving speed
    public float runSpeed;          //Running speed
    public float turnSpeed=0.1f;         //Turning speed
    public AudioClip WarnSound;
    private AudioSource AudioSource;

    private enum MonsterState
    {
        STAND,      
        CHECK,       
        WALK,       
        WARN,       
        CHASE,      
        RETURN      
    }
    private MonsterState currentState = MonsterState.STAND;          //Default state

    public float[] actionWeight = { 3000, 3000, 4000 };         //Set the weight of various actions in standby, the order is breathing, observation, moving
    public float actRestTme;            //Interval for changing standby command
    private float lastActTime;          //Last command time

    private float diatanceToPlayer;         //Monster to player distance
    private float diatanceToInitial;         //Monster distance from initial position
    private Quaternion targetRotation;         //Monster target orientation

    private bool is_Warned = false;
    private bool is_Running = false;
    private Animator thisAnimator;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        playerUnit = GameObject.Find("OVRCameraRig");
        thisAnimator = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //Save initial location information
        initialPosition = gameObject.GetComponent<Transform>().position;

        //检查并修正怪物设置
        //1. 自卫半径不大于警戒半径，否则就无法触发警戒状态，直接开始追击了
        defendRadius = Mathf.Min(alertRadius, defendRadius);
        //2. 攻击距离不大于自卫半径，否则就无法触发追击状态，直接开始战斗了
        attackRange = Mathf.Min(defendRadius, attackRange);
        //3. 游走半径不大于追击半径，否则怪物可能刚刚开始追击就返回出生点
        wanderRadius = Mathf.Min(chaseRadius, wanderRadius);

        //随机一个待机动作
        RandomAction();
    }

    /// <summary>
    /// 根据权重随机待机指令
    /// </summary>
    void RandomAction()
    {
        //更新行动时间
        lastActTime = Time.time;
        //根据权重随机
        float number = Random.Range(0, actionWeight[0] + actionWeight[1] + actionWeight[2]);
        if (number <= actionWeight[0])
        {
            currentState = MonsterState.STAND;
            thisAnimator.SetTrigger("Stand");
        }
        else if (actionWeight[0] < number && number <= actionWeight[0] + actionWeight[1])
        {
            currentState = MonsterState.CHECK;
            thisAnimator.SetTrigger("Stand");
        }
        if (actionWeight[0] + actionWeight[1] < number && number <= actionWeight[0] + actionWeight[1] + actionWeight[2])
        {
            currentState = MonsterState.WALK;
            //随机一个朝向
            targetRotation = Quaternion.Euler(0, Random.Range(1, 5) * 90, 0);
            thisAnimator.SetTrigger("Run");
        }
    }

    void Update()
    {
        EnemyDistanceCheck();
        switch (currentState)
        {
            //待机状态，等待actRestTme后重新随机指令
            case MonsterState.STAND:
                if (Time.time - lastActTime > actRestTme)
                {
                    RandomAction();         //随机切换指令
                }
                //该状态下的检测指令
                EnemyDistanceCheck();
                break;

            //待机状态，由于观察动画时间较长，并希望动画完整播放，故等待时间是根据一个完整动画的播放长度，而不是指令间隔时间
            case MonsterState.CHECK:
               
                    RandomAction();         //随机切换指令
               
                //该状态下的检测指令
                EnemyDistanceCheck();
                break;

            //游走，根据状态随机时生成的目标位置修改朝向，并向前移动
            case MonsterState.WALK:
               
                transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);

                if (Time.time - lastActTime > actRestTme)
                {
                    RandomAction();         //随机切换指令
                }
                //该状态下的检测指令
                WanderRadiusCheck();
                break;

            //警戒状态，播放一次警告动画和声音，并持续朝向玩家位置
            case MonsterState.WARN:
                if (!is_Warned)
                {
                    thisAnimator.SetTrigger("Stand");
                    warnSound();
                    is_Warned = true;
                }
                //持续朝向玩家位置
                targetRotation = Quaternion.LookRotation(playerUnit.transform.position - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
                //该状态下的检测指令
                WarningCheck();
                break;

            //追击状态，朝着玩家跑去
            case MonsterState.CHASE:
                
                if (!is_Running)
                {
                    thisAnimator.SetTrigger("Run");
                    is_Running = true;
                }
                //transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
                nav.destination = playerUnit.transform.position;
                //朝向玩家位置
                //targetRotation = Quaternion.LookRotation(playerUnit.transform.position - transform.position, Vector3.up);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
                //该状态下的检测指令
                ChaseRadiusCheck();
                break;

            //返回状态，超出追击范围后返回出生位置
            case MonsterState.RETURN:
                
                //朝向初始位置移动
                //targetRotation = Quaternion.LookRotation(initialPosition - transform.position, Vector3.up);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
               // transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
                nav.destination = initialPosition;
                //该状态下的检测指令
                ReturnCheck();
                break;
        }
    }

    /// <summary>
    /// 原地呼吸、观察状态的检测
    /// </summary>
    void EnemyDistanceCheck()
    {
        diatanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        if (diatanceToPlayer < attackRange)
        {
            thisAnimator.SetTrigger("Attack");
            playerUnit.GetComponent<PlayerCharacterController>().AdjustCurHealth(-10);
        }
        else if (diatanceToPlayer < defendRadius)
        {
            currentState = MonsterState.CHASE;
        }
        else if (diatanceToPlayer < alertRadius)
        {
            currentState = MonsterState.WARN;
        }
    }

    /// <summary>
    /// 警告状态下的检测，用于启动追击及取消警戒状态
    /// </summary>
    void WarningCheck()
    {
        diatanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        if (diatanceToPlayer < defendRadius)
        {
            is_Warned = false;
            currentState = MonsterState.CHASE;
        }

        if (diatanceToPlayer > alertRadius)
        {
            is_Warned = false;
            RandomAction();
        }
    }

    /// <summary>
    /// 游走状态检测，检测敌人距离及游走是否越界
    /// </summary>
    void WanderRadiusCheck()
    {
        diatanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        diatanceToInitial = Vector3.Distance(transform.position, initialPosition);

        if (diatanceToPlayer < attackRange)
        {
            thisAnimator.SetTrigger("Attack");
            playerUnit.GetComponent<PlayerCharacterController>().AdjustCurHealth(-10);
        }
        else if (diatanceToPlayer < defendRadius)
        {
            currentState = MonsterState.CHASE;
        }
        else if (diatanceToPlayer < alertRadius)
        {
            currentState = MonsterState.WARN;
        }

        if (diatanceToInitial > wanderRadius)
        {
            //朝向调整为初始方向
            targetRotation = Quaternion.LookRotation(initialPosition - transform.position, Vector3.up);
        }
    }

    /// <summary>
    /// 追击状态检测，检测敌人是否进入攻击范围以及是否离开警戒范围
    /// </summary>
    void ChaseRadiusCheck()
    {
        diatanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        diatanceToInitial = Vector3.Distance(transform.position, initialPosition);

        if (diatanceToPlayer < attackRange)
        {
            thisAnimator.SetTrigger("Attack");
            playerUnit.GetComponent<PlayerCharacterController>().AdjustCurHealth(-10);
        }
        //如果超出追击范围或者敌人的距离超出警戒距离就返回
        if (diatanceToInitial > chaseRadius || diatanceToPlayer > alertRadius)
        {
            currentState = MonsterState.RETURN;
        }
    }

    /// <summary>
    /// 超出追击半径，返回状态的检测，不再检测敌人距离
    /// </summary>
    void ReturnCheck()
    {
        diatanceToInitial = Vector3.Distance(transform.position, initialPosition);
        //如果已经接近初始位置，则随机一个待机状态
        if (diatanceToInitial < 0.5f)
        {
            is_Running = false;
            RandomAction();
        }
    }

    private void warnSound()
    {
        AudioSource.clip = WarnSound;
        AudioSource.Play();
    }

}
