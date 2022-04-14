using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 角色控制器
/// </summary>
public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    GameObject attackTarget;
    CharacterStates characterstates;
    float lastAttackTime;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        characterstates = GetComponent<CharacterStates>();
    }
    private void Start()
    {
        MouseManager.Instance.OnMouseClicked += MoveToTarget;
        MouseManager.Instance.OnEnemyClicked += EventAttack;
    }
    private void Update()
    {
        SwitchAnimation();
        lastAttackTime -= Time.deltaTime;
    }
    void Hit()//击中目标
    {
        var targetStates = attackTarget.GetComponent<CharacterStates>();
        targetStates.TakeDamage(characterstates, targetStates);
    }
    void SwitchAnimation()//持续更新动画器中的参数的值
    {
        anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }
    public void MoveToTarget(Vector3 target)//移动到目标
    {
        StopCoroutine(nameof(MoveToAttackTarget));
        agent.isStopped = false;
        agent.destination = target;
    }
    private void EventAttack(GameObject target)//攻击事件
    {
        if (target != null)
        {
            attackTarget = target;
            characterstates.isCritical = UnityEngine.Random.value < characterstates.CriticalChance;
            StartCoroutine(nameof(MoveToAttackTarget));
        }
    }
    IEnumerator MoveToAttackTarget()//移动到目标并根据条件是否攻击目标
    {
        agent.isStopped = false;
        while (Vector3.Distance(attackTarget.transform.position, transform.position) > characterstates.AttackRange)
        {
            agent.destination = attackTarget.transform.position;
            yield return null;
        }
        transform.LookAt(attackTarget.transform);
        agent.isStopped = true;
        if (lastAttackTime < 0)
        {
            anim.SetBool("Critical", characterstates.isCritical);
            anim.SetTrigger("Attack");
            lastAttackTime = characterstates.CoolDown;
        }
    }
}
