using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    
    public void DamagePlayer(float amount)
    {
        if(currentHealth > 0)
        {
            currentHealth -= amount;
            Debug.Log("Player Health: " + currentHealth);
        }
        else
        {
            //gameover
        }
    }
}
