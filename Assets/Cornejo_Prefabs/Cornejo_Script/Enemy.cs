using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Public variables
    public float movementSpeed = 5f;
    public float attackRange = 5f;
    public float attackCooldown = 2f;
    public int maxHealth = 50; // Health variable

    // Private variables
    public Transform targetTurret;
    private float lastAttackTime;
    private int currentHealth; // Current health variable
    private PlantTurret[] activeTurrets;
    private GameObject player; // Player reference

    public void Start()
    {
        lastAttackTime = Time.time;
        FindNearestTurret();
        currentHealth = maxHealth; // Initialize health
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player
    }

    public void Update()
    {
        if (currentHealth <= 0)
        {
            // Implement enemy death or removal logic here
            return;
        }

        if (targetTurret == null || !targetTurret.gameObject.activeSelf)
        {
            FindNearestTurret();
            return;
        }

        float distanceToTurret = Vector3.Distance(transform.position, targetTurret.position);
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (Random.Range(0f, 1f) < 0.5f)
        {
            // 50% chance to attack the player
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            // 50% chance to attack the nearest turret
            if (distanceToTurret <= attackRange)
            {
                AttackTurret();
            }
            else
            {
                MoveTowardsTurret();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    void MoveTowardsTurret()
    {
        Vector3 direction = (targetTurret.position - transform.position).normalized;
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    protected virtual void AttackPlayer()
    {
        // Implement your attack logic on the player
        lastAttackTime = Time.time;
    }

    protected virtual void AttackTurret()
    {
        // Implement your attack logic on the nearest turret
        lastAttackTime = Time.time;
    }

    void FindNearestTurret()
    {
        activeTurrets = FindObjectsOfType<PlantTurret>();

        float closestDistance = float.MaxValue;

        foreach (PlantTurret turret in activeTurrets)
        {
            float distance = Vector3.Distance(transform.position, turret.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetTurret = turret.transform;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
