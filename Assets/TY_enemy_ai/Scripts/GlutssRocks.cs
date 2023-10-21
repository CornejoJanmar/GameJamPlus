using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.Rendering;
using UnityEngine;

public class GlutssRocks : MonoBehaviour
{
    [SerializeField] GameObject player;
    Rigidbody2D rb;
    [SerializeField] int speed = 3;
    [SerializeField] Vector2 moveDirection;
    [SerializeField] PlayerHealthManager playerHealthManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        moveDirection = (player.transform.position -  transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        //Destroy(gameObject, 5f);
    }
    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            playerHealthManager = other.gameObject.GetComponent<PlayerHealthManager>();
            playerHealthManager.DamagePlayer(2);
            Destroy(gameObject);
        }
    }
}
