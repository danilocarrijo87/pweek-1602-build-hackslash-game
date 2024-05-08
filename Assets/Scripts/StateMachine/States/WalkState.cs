using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{
    private PlayerController player;
    private Transform _enemy;
        
    public WalkState(PlayerController player, Transform enemy = null)
    {
        this.player = player;
        _enemy = enemy;
    }

    private void OnMouseClick(RaycastHit hit)
    {
        player.agent.destination = hit.point;
        _enemy = hit.transform.tag.Equals("Enemy") ? hit.transform : null;
        Debug.Log(hit.transform.tag);
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
            player.animator.SetFloat("velocity",Mathf.Clamp(player.agent.velocity.sqrMagnitude, 0, 1));
            if (_enemy != null)
            {
                if (player.agent.remainingDistance <= player.agent.stoppingDistance + 2f)
                {
                    player.agent.isStopped = true;
                    player.agent.ResetPath();
                    Vector3 rotation = Quaternion.LookRotation(_enemy.transform.position).eulerAngles;
                    rotation.y = 0f;
                    player.transform.rotation = Quaternion.Euler(rotation);
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