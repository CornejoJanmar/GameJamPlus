using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float attackCooldown;
    private float cooldownTimer;
    [SerializeField] private bool firstAttack;
    private string hitColliderTag;

    [Header("HitBox")]
    [SerializeField] private float range;
    [SerializeField] private float hbHeight;
    [SerializeField] BoxCollider2D hitBox;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Reference")]
    [SerializeField] private GameObject bulletPrefab;

    private Dictionary<Transform, GameObject> createdBullets = new Dictionary<Transform, GameObject>();
    [SerializeField] private List<Transform> enemiesInHitBox = new List<Transform>();
    
    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
        enemyLayer = LayerMask.GetMask("enemy");
    }

    // Update is called once per frame
    void Update()
    {
        EnemyRangeAttack();
    }

    private bool EnemyInsideHitBox()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(hitBox.bounds.center,
            new Vector3(hitBox.bounds.size.x * range, hitBox.bounds.size.y * hbHeight, hitBox.bounds.size.z),
            0, Vector2.right, 0, enemyLayer);

        // Clear the list of enemies
        enemiesInHitBox.Clear();

        foreach (var hit in hits)
        {
            hitColliderTag = hit.collider.gameObject.tag;

            if ((hitColliderTag == "Prowler" || hitColliderTag == "Glutts") && !enemyHasBullet(hit.transform))
            {
                enemiesInHitBox.Add(hit.transform);
            }
        }

        return enemiesInHitBox.Count > 0;
    }

    private bool enemyHasBullet(Transform enemy)
    {
        // Check if a bullet has already been created for this enemy
        return createdBullets.ContainsKey(enemy);
    }

    private void EnemyRangeAttack()
    {
        if (EnemyInsideHitBox())
        {
            if (firstAttack == false)
            {
                CreateBullet();
                firstAttack = true;
            }

            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= attackCooldown)
            {
                CreateBullet();
                cooldownTimer = 0;
            }
        }
    }

    private void CreateBullet()
    {
        foreach (var enemyTransform in enemiesInHitBox)
        {
            // Check if a bullet has already been created for this enemy
            if (!enemyHasBullet(enemyTransform))
            {
                GameObject newObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                PlantBullet plantBullet = newObject.GetComponent<PlantBullet>();
                if (plantBullet != null)
                {
                    plantBullet.enemyTransform = enemyTransform;
                    plantBullet.plantManager = this.gameObject.GetComponent<PlantManager>();
                }

                // Mark this enemy as having a bullet
                createdBullets[enemyTransform] = newObject;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(hitBox.bounds.center,
        new Vector3(hitBox.bounds.size.x * range, hitBox.bounds.size.y * hbHeight, hitBox.bounds.size.z));
    }
}
