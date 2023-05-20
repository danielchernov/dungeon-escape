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
    protected int currentTarget;

    protected bool isHit = false;

    protected Player player;

    protected bool isDead = false;

    public int Health { get; set; }

    public virtual void Init()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            enemyRenderer.flipX = true;
        }
        else if (currentTarget == 1)
        {
            enemyRenderer.flipX = false;
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
        if (distance > 5)
        {
            enemyAnimator.SetBool("InCombat", false);
            isHit = false;
        }

        Vector3 direction = player.transform.position - transform.position;
        if (enemyAnimator.GetBool("InCombat"))
        {
            if (direction.x > 0)
            {
                enemyRenderer.flipX = false;
            }
            else
            {
                enemyRenderer.flipX = true;
            }
        }
    }

    public virtual void Damage()
    {
        Health--;
        enemyAnimator.SetTrigger("Hit");
        enemyAnimator.SetBool("InCombat", true);
        isHit = true;

        if (Health < 1)
        {
            isDead = true;
            enemyAnimator.SetTrigger("Dead");
        }
    }
}
