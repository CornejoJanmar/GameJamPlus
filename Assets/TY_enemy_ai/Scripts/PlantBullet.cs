using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float speed = 3;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] public PlantManager plantManager;
    [SerializeField] int damage = 1;

    [Header("HitBox")]
    [SerializeField] private float range;
    [SerializeField] private float hbHeight;
    [SerializeField] BoxCollider2D hitBox;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Reference")]
    [SerializeField] public Prowler_AI prowler;
    [SerializeField] public Glutts glutss;

    public Transform enemyTransform;

    private bool isDamage;
    private string hitColliderTag;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyLayer = LayerMask.GetMask("enemy");

        isDamage = false;

        if (enemyTransform == null)
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (enemyTransform == null)
        {
            Destroy(gameObject);
        }
        
        if (plantManager != null)
        {
            moveDirection = (enemyTransform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Prowler")
        {
            prowler = other.gameObject.GetComponent<Prowler_AI>();
            if (!isDamage)
            {
                //prowler.TakeDamage(damage);
                isDamage = true;
                Destroy(gameObject);
            }
        }

        if (other.gameObject.tag == "Glutts")
        {
            glutss = other.gameObject.GetComponent<Glutts>();
            if (!isDamage)
            {
                //glutss.TakeDamage(damage);
                isDamage = true;
                Destroy(gameObject);
            }
        }
    }

    // private bool EnemyInsideHitBox()
    // {
    //     RaycastHit2D hit = Physics2D.BoxCast(hitBox.bounds.center,
    //     new Vector3(hitBox.bounds.size.x * range, hitBox.bounds.size.y * hbHeight, hitBox.bounds.size.z)
    //     , 0, Vector2.right, 0, enemyLayer);

    //     if (hit.collider != null)
    //     {
    //         Debug.Log("prow!");
    //         hitColliderTag = hit.collider.gameObject.tag;
    //         if (hitColliderTag == "Prowler")
    //         {
    //             prowler = hit.transform.GetComponent<Prowler_AI>();
    //             prowler.TakeDamage(damage);
    //             Destroy(gameObject);
    //         }

    //         if (hitColliderTag == "Glutts")
    //         {
    //             glutss = hit.transform.GetComponent<Glutts>();
    //             glutss.TakeDamage(damage);
    //             Destroy(gameObject);

    //         }
    //     }
    //     return hit.collider != null;
    // }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireCube(hitBox.bounds.center,
    //     new Vector3(hitBox.bounds.size.x * range, hitBox.bounds.size.y * hbHeight, hitBox.bounds.size.z));
    // }

}
