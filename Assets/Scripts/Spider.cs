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
        if (isDead)
        {
            return;
        }

        Health--;
        if (Health < 1)
        {
            isDead = true;
            enemyAnimator.SetTrigger("Dead");
            GameObject spawnedGem = Instantiate(
                gemToSpawn,
                transform.position,
                Quaternion.identity
            );
            spawnedGem.GetComponent<Diamond>().DiamondValue = base.gems;
        }
    }

    public override void Movement() { }

    public void Attack()
    {
        Instantiate(acidPrefab, spawnPoint.position, Quaternion.identity);
    }
}
