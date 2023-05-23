using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField]
    GameObject acidPrefab;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    private bool goRight = false;

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
            AudioManager.Instance.PlaySFX(sfxAudios[0], 0.5f);

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
        GameObject acid = Instantiate(acidPrefab, spawnPoint.position, Quaternion.identity);
        acid.GetComponent<AcidEffect>().goRight = goRight;
    }
}
