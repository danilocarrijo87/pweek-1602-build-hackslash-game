using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private PlayerController player;
        
    public AttackState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animator.SetBool("isAttacking",true);
        player.OnAttackEndEvent += OnAttackEnd;
    }

    private void OnAttackEnd(bool end)
    {
        player.animator.SetBool("isAttacking",false);
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