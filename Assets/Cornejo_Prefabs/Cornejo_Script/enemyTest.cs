using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTest : Enemy
{
    public float dashSpeed = 10f;
    public float dashCooldown = 3f;

    private bool isDashing = false;
    private float lastDashTime;

    void Update()
    {
        if (isDashing)
        {
            Dash();
        }
        else
        {
            base.Update();

            if (Time.time - lastDashTime >= dashCooldown)
            {
                if (CanDash())
                {
                    StartDash();
                }
            }
        }
    }

    bool CanDash()
    {
        return targetTurret != null && Vector3.Distance(transform.position, targetTurret.position) <= attackRange;
    }

    void StartDash()
    {
        isDashing = true;
        lastDashTime = Time.time;
    }

    void Dash()
    {
        Vector3 direction = (targetTurret.position - transform.position).normalized;
        transform.Translate(direction * dashSpeed * Time.deltaTime);

        float dashDistance = Vector3.Distance(transform.position, targetTurret.position);
        if (dashDistance <= 0.1f)
        {
            isDashing = false;
        }
    }
}
