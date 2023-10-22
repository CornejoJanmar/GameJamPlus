using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantB : MonoBehaviour
{
    [Header("HitBox")]
    [SerializeField] private float range;
    [SerializeField] private float hbHeight;
    [SerializeField] BoxCollider2D hitBox;
    [SerializeField] private LayerMask enemyLayer;
    private string hitColliderTag;
    [SerializeField] private int explosionCooldown;
    private float cooldownTimer;

    private void Start()
    {
        enemyLayer = LayerMask.GetMask("enemy");
    }

    private void Update()
    {
        EnemyInsideHitBox();
    }

    private void EnemyInsideHitBox()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(hitBox.bounds.center,
            new Vector3(hitBox.bounds.size.x * range, hitBox.bounds.size.y * hbHeight, hitBox.bounds.size.z),
            0, Vector2.right, 0, enemyLayer);

        foreach (var hit in hits)
        {
            hitColliderTag = hit.collider.gameObject.tag;

            if (hitColliderTag == "Prowler" || hitColliderTag == "Glutts")
            {
                cooldownTimer += Time.deltaTime;
                if (cooldownTimer >= explosionCooldown)
                {
                    Destroy(hit.collider.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitBox.bounds.center,
        new Vector3(hitBox.bounds.size.x * range, hitBox.bounds.size.y * hbHeight, hitBox.bounds.size.z));
    }
}
