using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class Glutts : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    [SerializeField] private int remainingDistance;
    [SerializeField] private int speed = 5;

    [Header("Health")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [Header("Attack")]
    [SerializeField] private float damage;
    [SerializeField] private int attackCooldown;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer;
    private PlayerHealthManager playerHealthManager;
    private PlantHealthManager plantHealthManager;
    [SerializeField] private bool enemyFirstAttack;
    private string hitColliderTag;
    private Transform plant;
    [SerializeField] private GameObject rocksPrefab;
    [SerializeField] private Transform rocksSpawner;
    [SerializeField] private GameObject parentEnemyPrefab;

    [Header("HitBox")]
    [SerializeField] private float range;
    [SerializeField] private float hbHeight;
    [SerializeField] BoxCollider2D hitBox;

    // Start is called before the first frame update
    void Start()
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
        playerLayer = LayerMask.GetMask("player");

        cooldownTimer = 0;
        enemyFirstAttack = false;
    }

    private void Update()
    {

        if (plantHealthManager == null && playerHealthManager == null)
        {
            enemyFirstAttack = false;
            FollowPlayer();
        }

        if (agent.remainingDistance <= remainingDistance)
        {
            EnemyMeleeAttack();
            agent.speed = 0;
        }
        else
        {
            EnemyRangeAttack();
            agent.speed = speed;
        }
    }

    private void EnemyRangeAttack()
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


    private void FollowPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    private void EnemyMeleeAttack()
    {
        if (EnemyInsideHitBox())
        {
            if (hitColliderTag == "Player")
            {
                if (enemyFirstAttack == false)
                {
                    enemyFirstAttack = true;
                    playerHealthManager.DamagePlayer(damage);
                }
                else
                {
                    cooldownTimer += Time.deltaTime;
                    if (cooldownTimer >= attackCooldown)
                    {
                        cooldownTimer = 0;
                        playerHealthManager.DamagePlayer(damage);
                    }
                }
            }
            else if (hitColliderTag == "Plant" && plant != null)
            {
                // set the agent destination to plant
                agent.SetDestination(plant.position);
                if (enemyFirstAttack == false)
                {
                    enemyFirstAttack = true;
                    plantHealthManager.DamagePlant(damage);
                }
                else
                {
                    cooldownTimer += Time.deltaTime;
                    if (cooldownTimer >= attackCooldown)
                    {
                        cooldownTimer = 0;
                        plantHealthManager.DamagePlant(damage);
                    }
                }
            }

        }
    }

    private bool EnemyInsideHitBox()
    {
        RaycastHit2D hit = Physics2D.BoxCast(hitBox.bounds.center,
        new Vector3(hitBox.bounds.size.x * range, hitBox.bounds.size.y * hbHeight, hitBox.bounds.size.z)
        , 0, Vector2.right, 0, playerLayer);

        if (hit.collider != null)
        {
            hitColliderTag = hit.collider.gameObject.tag;

            if (hitColliderTag == "Player")
            {
                hitColliderTag = "Player";
                playerHealthManager = hit.transform.GetComponent<PlayerHealthManager>();
            }

            if (hitColliderTag == "Plant")
            {
                hitColliderTag = "Plant";
                plant = hit.transform;
                plantHealthManager = hit.transform.GetComponent<PlantHealthManager>();
            }
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitBox.bounds.center,
        new Vector3(hitBox.bounds.size.x * range, hitBox.bounds.size.y * hbHeight, hitBox.bounds.size.z));
    }

}
