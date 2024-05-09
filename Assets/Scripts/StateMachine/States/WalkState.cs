using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{
    private readonly PlayerController player;
    private Transform enemy;
    private static readonly int Velocity = Animator.StringToHash("velocity");

    public WalkState(PlayerController player, Transform enemy = null)
    {
        this.player = player;
        this.enemy = enemy;
    }

    private void OnMouseClick(RaycastHit hit)
    {
        player.agent.destination = hit.point;
        enemy = hit.transform.tag.Equals("Enemy") ? hit.transform : null;
        player.ChangeState(new WalkState(player));
    }

    public void Enter()
    {
        player.OnMouseClick += OnMouseClick;
        player.agent.isStopped = false;
    }
        
    public void Update()
    {
        if (!player.agent.pathPending)
        {
            player.animator.SetFloat(Velocity,Mathf.Clamp(player.agent.velocity.sqrMagnitude, 0, 1));
            if (enemy != null)
            {
                if (player.agent.remainingDistance <= player.agent.stoppingDistance + 2f)
                {
                    player.agent.isStopped = true;
                    player.agent.ResetPath();
                    player.transform.LookAt (new Vector3 (enemy.transform.position.x, player.transform.position.y, enemy.transform.position.z));
                    player.ChangeState(new AttackState(player));
                }
            }
            else
            {
                if (player.agent.remainingDistance <= player.agent.stoppingDistance + .5f)
                {
                    if (!player.agent.hasPath || player.agent.velocity.sqrMagnitude == 0f)
                    {
                        player.ChangeState(new IdleState(player));
                    }
                } 
            }
        }
    }

    public void Exit()
    {
        player.OnMouseClick -= OnMouseClick;
    }
}