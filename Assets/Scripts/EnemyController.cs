using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public float health;

    public Transform healthBar;
    public Transform[] patrolPoints;
    public Vector3 hitOffset;
    public float hearingCapacity;
    public float fovAngle;
    public float fov;
    public float distanceOfInterest;
    public float patrolSpeed;
    public float chaseSpeed;
    public float attackDistance;
    public event Action<bool> OnAttackEndEvent;
    public event Action<bool> OnHitEvent;
    [HideInInspector] public Animator animator;
    public GameObject hitEffect;
    public float damage;

    private UnityEngine.UI.Slider healthSlider;
    private StateMachine stateMachine;
    [HideInInspector] public NavMeshAgent agent = null;
    private GameObject hitEffectObj;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        healthSlider = healthBar.GetComponent<UnityEngine.UI.Slider>();
        healthSlider.maxValue = health;
        healthSlider.value = health;
        agent = GetComponent<NavMeshAgent>();
        stateMachine = new StateMachine();
        stateMachine.Initialize(new PatrolState(this));
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.LookAt(Camera.main.transform);
        stateMachine.Update();
    }

    public void ChangeState(IState state)
    {
        stateMachine.TransitionTo(state);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        healthSlider.value = health;
        Debug.Log(amount);
    }

    public void OnHit()
    {
        OnHitEvent?.Invoke(true);
    }

    public void InstantiateHitEffect(Vector3 pos)
    {
        hitEffectObj = Instantiate(hitEffect, pos, Quaternion.identity);
    }

    public void AttackEnd()
    {
        Destroy(hitEffectObj);
        OnAttackEndEvent?.Invoke(true);
    }
}
