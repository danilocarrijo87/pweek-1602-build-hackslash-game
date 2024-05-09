using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private readonly PlayerController player;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    public AttackState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animator.SetBool(IsAttacking,true);
        player.OnAttackEndEvent += OnAttackEnd;
    }

    private void OnAttackEnd(bool end)
    {
        player.animator.SetBool(IsAttacking,false);
        player.ChangeState(new IdleState(player));
    }
        
    public void Update()
    {
    }

    public void Exit()
    {
        player.OnAttackEndEvent -= OnAttackEnd;
    }
}