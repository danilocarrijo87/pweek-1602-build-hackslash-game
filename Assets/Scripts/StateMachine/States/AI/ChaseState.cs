using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    private readonly EnemyController controller;
    private readonly Transform player;
    
    public ChaseState(EnemyController controller, Transform player)
    {
        this.controller = controller;
        this.player = player;
    }
    
    public void Enter()
    {
        controller.agent.isStopped = false;
        controller.agent.speed = controller.chaseSpeed;
        this.controller.agent.SetDestination(player.position);  
    }

    public void Update()
    {
        if (controller.agent.remainingDistance <= controller.agent.stoppingDistance + 2f)
        {
            controller.agent.isStopped = true;
            controller.agent.ResetPath();
            controller.transform.LookAt (new Vector3 (player.position.x, controller.transform.position.y, player.position.z));
            controller.ChangeState(new EnemyAttackState(controller, player));
        }
        else
        {
            this.controller.agent.SetDestination(player.position);  
        }
        if (Vector3.Distance(player.transform.position, controller.transform.position) > controller.distanceOfInterest)
        {
            controller.ChangeState(new PatrolState(controller));
        }
    }

    public void Exit()
    {
    }
}
