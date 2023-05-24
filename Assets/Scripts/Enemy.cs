using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int health;

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected int gems;

    [SerializeField]
    protected Transform[] waypoints;

    protected Animator enemyAnimator;
    protected SpriteRenderer enemyRenderer;
    protected Transform enemyHitbox;
    protected int currentTarget;

    protected bool isHit = false;

    protected Player player;

    protected bool isDead = false;

    public int Health { get; set; }

    [SerializeField]
    protected GameObject gemToSpawn;

    [SerializeField]
    protected AudioClip[] sfxAudios;

    private Vector3 direction;

    public virtual void Init()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyHitbox = transform.GetChild(0);
        player = GameObject.Find("Player").GetComponent<Player>();
        Health = health;
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (isDead)
        {
            Destroy(gameObject, 3f);
        }
        else if (speed != 0)
        {
            if (
                enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")
                && !enemyAnimator.GetBool("InCombat")
            )
            {
                return;
            }

            Movement();
        }
    }

    public virtual void Movement()
    {
        if (currentTarget == 0)
        {
            //enemyRenderer.flipX = true;
            if (enemyHitbox != null)
            {
                enemyHitbox.rotation = Quaternion.Euler(0, 180, enemyHitbox.rotation.eulerAngles.z);
            }
        }
        else if (currentTarget == 1)
        {
            //enemyRenderer.flipX = false;
            if (enemyHitbox != null)
            {
                enemyHitbox.rotation = Quaternion.Euler(0, 0, enemyHitbox.rotation.eulerAngles.z);
            }
        }

        if (transform.position == waypoints[0].position)
        {
            currentTarget = 1;
            enemyAnimator.SetTrigger("Idle");
        }
        else if (transform.position == waypoints[1].position)
        {
            currentTarget = 0;
            enemyAnimator.SetTrigger("Idle");
        }

        if (!isHit)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                waypoints[currentTarget].position,
                speed * Time.deltaTime
            );
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > 3)
        {
            enemyAnimator.SetBool("InCombat", false);
            isHit = false;
        }

        if (player != null)
        {
            direction = player.transform.position - transform.position;
        }

        if (enemyAnimator.GetBool("InCombat"))
        {
            if (direction.x > 0)
            {
                //enemyRenderer.flipX = false;
                if (enemyHitbox != null)
                {
                    enemyHitbox.rotation = Quaternion.Euler(
                        0,
                        0,
                        enemyHitbox.rotation.eulerAngles.z
                    );
                }
            }
            else
            {
                //enemyRenderer.flipX = true;
                if (enemyHitbox != null)
                {
                    enemyHitbox.rotation = Quaternion.Euler(
                        0,
                        180,
                        enemyHitbox.rotation.eulerAngles.z
                    );
                }
            }
        }
    }

    public virtual void Damage()
    {
        if (isDead)
        {
            return;
        }

        Health--;
        enemyAnimator.SetTrigger("Hit");
        enemyAnimator.SetBool("InCombat", true);
        isHit = true;

        AudioManager.Instance.PlaySFX(sfxAudios[0], 0.5f);

        if (Health < 1)
        {
            isDead = true;
            enemyAnimator.SetTrigger("Dead");
            AudioManager.Instance.PlaySFX(sfxAudios[1], 0.5f);

            GameObject spawnedGem = Instantiate(
                gemToSpawn,
                transform.position,
                Quaternion.identity
            );
            spawnedGem.GetComponent<Diamond>().DiamondValue = gems;
        }
    }
}
