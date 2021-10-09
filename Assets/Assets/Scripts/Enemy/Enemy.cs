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
    protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected bool isHit = false;
    protected bool isDead = false;

    protected Player player;

    public virtual void Init()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Awake()
    {
        Init();
    }

    public virtual void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !animator.GetBool("InCombat")) return;

        if(!isDead) Movement();
    }

    public virtual void Movement()
    {
        spriteRenderer.flipX = currentTarget == pointA.position ? true : false;

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            animator.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            animator.SetTrigger("Idle");
        }

        if (!isHit) transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        //If you are far set combat false
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if (distance > 3.0)
        {
            isHit = false;
            animator.SetBool("InCombat", false);
        }

        //Flip attack
        Vector3 direction = player.transform.localPosition - transform.localPosition;

        if (direction.x > 0 && animator.GetBool("InCombat"))
            spriteRenderer.flipX = false;

        else if (direction.x < 0 && animator.GetBool("InCombat"))
            spriteRenderer.flipX = true;

    }

    //public virtual void Attack()
    //{
    //    Debug.Log("Attack");
    //}

}
