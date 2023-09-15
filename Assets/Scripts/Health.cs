using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100;

    [SerializeField]
    private float currentHealth;

    public string deathAnimationTrigger = "Die";
    public string hitAnimationTrigger = "Hit";

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger(hitAnimationTrigger);
        }
    }

    public void Die()
    {
        Destroy(this, 2);
        animator.SetTrigger(deathAnimationTrigger);
    }
}
