using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    public float moveSpeed;
    private Transform target;

    public float health;

    public float knockBackTime = .5f;
    private float knockBackCounter;

    private Vector3 PositiveVectorOne = new Vector3(1, 1, 1);
    private Vector3 NegativeVectorOne = new Vector3(-1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;

        if(direction.x < 0)
        {
            transform.localScale = PositiveVectorOne;
        }
        else
        {
            transform.localScale = NegativeVectorOne;
        }
        rigidBody2D.velocity = (target.position - transform.position).normalized * moveSpeed;

        if(knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if(moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            if(knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * 0.5f);
            }
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageTotake, bool shouldKnockback)
    {
        TakeDamage(damageTotake);

        if (shouldKnockback)
        {
            knockBackCounter = knockBackTime;
        }

    }
}
