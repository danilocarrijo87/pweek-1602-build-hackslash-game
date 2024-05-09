using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private readonly EnemyController controller;
    
    public PatrolState(EnemyController controller)
    {
        this.controller = controller;
    }
    
    public void Enter()
    {
        controller.agent.isStopped = false;
        controller.agent.speed = controller.patrolSpeed;
        if (controller.patrolPoints.Length == 0)
        {
            Debug.LogError($"Enemy {controller.name} does not have patrol points");
        }
        else
        {
            controller.agent.destination = controller.patrolPoints[Random.Range(0, controller.patrolPoints.Length)].position;
        }
    }

    public void Update()
    {
        var targetsInFOV = Physics.OverlapSphere(
            controller.transform.position, controller.fov);
        foreach (var c in targetsInFOV)
        {
            if (!c.CompareTag("Player")) continue;
            var transform = controller.transform;
            var signedAngle = Vector3.Angle(
                transform.forward,
                c.transform.position - transform.position);
            if (Mathf.Abs(signedAngle) < controller.fovAngle / 2)
            {
                controller.ChangeState(new ChaseState(controller, c.transform));
            }
            break;
        }
        if (!controller.agent.pathPending)
        {
            if (controller.agent.remainingDistance <= controller.agent.stoppingDistance + .5f)
            {
                controller.agent.destination = controller.patrolPoints[Random.Range(0, controller.patrolPoints.Length)].position;
            }
        }
    }

    public void Exit()
    {
    }
}
