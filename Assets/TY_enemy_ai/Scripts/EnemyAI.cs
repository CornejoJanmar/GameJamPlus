using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class EnemyAI : MonoBehaviour
{
    // AI // 
    private NavMeshAgent agent;
    protected GameObject player;
    protected int remainingDistance;
    protected int speed = 5;

    // Health // 
    protected int maxHealth = 5;
    private int currentHealth;

    // Attack // 
    protected float damage;
    [SerializeField] BoxCollider2D hitBox;
    protected int attackCooldown;
    private LayerMask playerLayer;
    private float cooldownTimer;
    protected PlayerHealthManager playerHealthManager;
    private bool enemyFirstAttack;
    protected GameObject rocksPrefab;
    protected Transform rocksSpawner;

    [Header("Manager")]
    [SerializeField] private float range;
    [SerializeField] private float hbHeight;
    [SerializeField] protected GameObject parentEnemyPrefab;

    public virtual void Start() 
    {
        // Set Health
        currentHealth = maxHealth;

        // Reference agent
        agent = GetComponent<NavMeshAgent>();
        hitBox = GetComponent<BoxCollider2D>();

        // Set Agent Values
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Find Player and Components
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealthManager = player.GetComponent<PlayerHealthManager>();
        playerLayer = LayerMask.GetMask("player");

        cooldownTimer = 0;
        enemyFirstAttack = false;
    }

    public virtual void Update() 
    {
        FollowPlayer();
    }

    protected virtual void EnemyMeleeAttack()
    {
        if (EnemyInsideHitBox())
        {
            if (enemyFirstAttack == false)
            {
                enemyFirstAttack = true;
                playerHealthManager.DamagePlayer(damage);
            }
            else
                MeleeAttackCooldownTimer();
        }
    }

    protected virtual void EnemyRangeAttack(int distance)
    {
        if (agent.remainingDistance > distance)
        {
            if (enemyFirstAttack == false)
            {
                enemyFirstAttack = true;
                parentEnemyPrefab = GameObject.Find("ParentEnemyHolder");
                GameObject newObject = Instantiate(rocksPrefab, rocksSpawner.transform.position, Quaternion.identity);
                newObject.transform.parent = parentEnemyPrefab.transform;
            }
            else
                RangeAttackCooldownTimer();
        }
    }

    public virtual void FixedUpdate()
    {
        if(agent.remainingDistance <= remainingDistance)
            agent.speed = 0;
        else
            agent.speed = speed;
    }

    protected virtual void FollowPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    protected virtual void MeleeAttackCooldownTimer()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            playerHealthManager.DamagePlayer(damage);
        }
    }
    protected virtual void RangeAttackCooldownTimer()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
        {
            parentEnemyPrefab = GameObject.Find("ParentEnemyHolder");
            GameObject newObject = Instantiate(rocksPrefab, rocksSpawner.transform.position, Quaternion.identity);
            newObject.transform.parent = parentEnemyPrefab.transform;
            cooldownTimer = 0;
        }
    }

    protected virtual bool EnemyInsideHitBox()
    {
        RaycastHit2D hit = Physics2D.BoxCast(hitBox.bounds.center,
        new Vector3(hitBox.bounds.size.x * range, hitBox.bounds.size.y * hbHeight, hitBox.bounds.size.z)
        , 0, Vector2.right, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealthManager = hit.transform.GetComponent<PlayerHealthManager>();
        }
        return hit.collider != null;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitBox.bounds.center,
        new Vector3(hitBox.bounds.size.x * range, hitBox.bounds.size.y * hbHeight, hitBox.bounds.size.z));
    }
}
