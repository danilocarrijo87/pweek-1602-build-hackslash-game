using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    private readonly EnemyController controller;
    private readonly Transform player;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    public EnemyAttackState(EnemyController controller, Transform player)
    {
        this.controller = controller;
        this.player = player;
    }

    public void Enter()
    {
        controller.animator.SetBool(IsAttacking,true);
        controller.OnAttackEndEvent += OnAttackEnd;
        controller.OnHitEvent += OnHitEvent;
    }

    private void OnHitEvent(bool obj)
    {
        var distance = Vector3.Distance(controller.transform.position, player.position);
        if (distance < controller.attackDistance)
        {
            player.SendMessage("TakeDamage", controller.damage);
            controller.InstantiateHitEffect(player.position);  
        }
    }

    private void OnAttackEnd(bool end)
    {
        controller.animator.SetBool(IsAttacking,false);
        controller.ChangeState(new ChaseState(controller, player));
    }
        
    public void Update()
    {
    }

    public void Exit()
    {
        controller.OnAttackEndEvent -= OnAttackEnd;
    }
}