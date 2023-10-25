using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] int damage = 50;
    Prowler_AI enemy1;
    Glutts enemy2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.tag == "enemy")
        {
            enemy1 = collision.gameObject.GetComponent<Prowler_AI>();
            enemy2 = collision.gameObject.GetComponent<Glutts>();

            enemy1.TakeDamage(damage);
            enemy2.TakeDamage(damage);

            Destroy(gameObject);
        }
        Destroy(gameObject);
        if(collision.gameObject.tag == "env")
        {
            Destroy(gameObject);

        }


    }
}
