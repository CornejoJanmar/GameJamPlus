using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTestRange : Enemy
{
    public float slowSpeed = 2f;
    public int maxHealth = 100;
    
    private int currentHealth;

    void Start()
    {
        base.Start();

        currentHealth = maxHealth;
        movementSpeed = slowSpeed;
    }

    void Update()
    {
        base.Update();
    }
}
