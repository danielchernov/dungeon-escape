using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField]
    GameObject acidPrefab;

    [SerializeField]
    Transform spawnPoint;

    public override void Init()
    {
        base.Init();
    }

    public override void Damage()
    {
        Health--;
        if (Health < 1)
        {
            isDead = true;
            enemyAnimator.SetTrigger("Dead");
        }
    }

    public override void Movement() { }

    public void Attack()
    {
        Instantiate(acidPrefab, spawnPoint.position, Quaternion.identity);
    }
}
