using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5;
    [SerializeField] private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void DamagePlant(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Plant Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
