using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{

    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject bulletFab;

    [SerializeField] private float BulletForce = 20f;
    [SerializeField] private float time = 2.0f;



    void Update()
    {

            if (Input.GetButtonDown("Fire1"))
            {
                //SoundManager.instance.PlaySound(Attack);
                Shoot();
            }

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletFab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * BulletForce, ForceMode2D.Impulse);

        RaycastHit2D hitInfo = Physics2D.Raycast(firepoint.position, firepoint.right);


    }
}
