using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
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

    public virtual void Init()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }

        Movement();
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

        transform.position = Vector3.MoveTowards(
            transform.position,
            waypoints[currentTarget].position,
            speed * Time.deltaTime
        );
    }
}
