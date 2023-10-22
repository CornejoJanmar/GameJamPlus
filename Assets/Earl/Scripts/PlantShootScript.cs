using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantShootScript : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 5;

    private void Start()
    {
        currentHealth = maxHealth;
    }
}
