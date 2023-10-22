using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] GameObject HitEffect;
    [SerializeField] int damage = 50;
    Prowler_AI enemy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(HitEffect, transform.position, Quaternion.identity);

        if (collision.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
            Destroy(effect, 1f);
            enemy = collision.gameObject.GetComponent<Prowler_AI>();

        }

        Destroy(gameObject);
        Destroy(effect, 1f);

    }
}
