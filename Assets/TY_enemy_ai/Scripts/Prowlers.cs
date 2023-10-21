using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;

public class Prowlers : EnemyAI
{
    [Header("Prowler")]
    [SerializeField] private int prowlerMaxHealth = 5;
    [SerializeField] private int prowlerSpeed = 10;
    [SerializeField] private int prowlerAttackCooldown = 3;
    [SerializeField] private int prowlerRemainingDistance = 2;
    [SerializeField] private float prowlerDamage = 0.5f;
    public override void Start()
    {
        maxHealth = prowlerMaxHealth;
        speed = prowlerSpeed;
        attackCooldown = prowlerAttackCooldown;
        remainingDistance = prowlerRemainingDistance;
        damage = prowlerDamage;
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        EnemyMeleeAttack();
    }
}
