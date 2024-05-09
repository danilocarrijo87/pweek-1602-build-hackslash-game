using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent = null;
    [HideInInspector] public Animator animator;
    private Camera cam = null;
    private StateMachine stateMachine;
    public event Action<RaycastHit> OnMouseClick;
    public float maxHealth;
    public event Action<bool> OnAttackEndEvent;
    public BoolEventChannel boolEventChannel;
    public FloatEventChannel playerHealthChannel;

    private float currHealth;
        
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        playerHealthChannel.Invoke(currHealth);
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine();
        stateMachine.Initialize(new IdleState(this));
    }

    public void ChangeState(IState state)
    {
        stateMachine.TransitionTo(state);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        
        if (Input.GetMouseButtonDown(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out var _hit, 100))
            {
                OnMouseClick?.Invoke(_hit);
                //agent.destination = _hit.point;
            }
        }
    }

    public void FootR()
    {
        
    }

    public void FootL()
    {
        
    }

    public void TakeDamage(float amount)
    {
        currHealth -= amount;
        playerHealthChannel.Invoke(currHealth);
    }

    public void SwordSlash()
    {
        boolEventChannel.Invoke(true);
    }

    public void AttackEnd()
    {
        OnAttackEndEvent?.Invoke(true);
        boolEventChannel.Invoke(false);
    }
}
