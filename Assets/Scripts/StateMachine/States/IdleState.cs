using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private PlayerController player;
        
    public IdleState(PlayerController player)
    {
        this.player = player;
    }

    private void OnMouseClick(RaycastHit hit)
    {
        player.agent.destination = hit.point;
        var enemy = hit.transform.tag.Equals("Enemy") ? hit.transform : null;
        Debug.Log(hit.transform.tag);
        player.ChangeState(new WalkState(player, enemy));
    }

    public void Enter()
    {
        player.agent.isStopped = false;
        player.OnMouseClick += OnMouseClick;
        player.animator.SetFloat("velocity",0);
    }
        
    public void Update()
    {
    }

    public void Exit()
    {
        player.OnMouseClick -= OnMouseClick;
    }
}