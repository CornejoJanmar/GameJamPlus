using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;

public class Glutts : EnemyAI
{
    [Header("glutts")]
    [SerializeField] private int gluttsMaxHealth = 5;
    [SerializeField] private int gluttsSpeed = 10;
    [SerializeField] private int gluttsAttackCooldown = 3;
    [SerializeField] private int gluttsRemainingDistance = 5;
    [SerializeField] private int gluttsDamage = 1;
    [SerializeField] private GameObject _rocksPrefab;
    [SerializeField] private Transform _rocksSpawner;

    public override void Start()
    {
        maxHealth = gluttsMaxHealth;
        speed = gluttsSpeed;
        attackCooldown = gluttsAttackCooldown;
        remainingDistance = gluttsRemainingDistance;
        damage = gluttsDamage;
        rocksPrefab = _rocksPrefab;
        rocksSpawner = _rocksSpawner;
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        EnemyRangeAttack(15);
        EnemyMeleeAttack();
    }


}
