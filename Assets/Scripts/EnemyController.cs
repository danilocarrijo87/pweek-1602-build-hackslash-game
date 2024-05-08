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

    private UnityEngine.UI.Slider healthSlider;
    [HideInInspector] public NavMeshAgent agent = null;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider = healthBar.GetComponent<UnityEngine.UI.Slider>();
        healthSlider.maxValue = health;
        healthSlider.value = health;
        agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Length == 0)
        {
            Debug.LogError($"Enemy {name} does not have patrol points");
        }
        agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.LookAt(Camera.main.transform);
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance + .5f)
            {
                agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
            }
        }
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
}
