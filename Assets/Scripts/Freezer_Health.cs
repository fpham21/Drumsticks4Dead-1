using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer_Health : MonoBehaviour
{
    [SerializeField]
    public int currentHealth;
    [SerializeField]
    public int maxHealth = 100;
    [SerializeField]
    private int damageDelt = 1;
    [SerializeField]
    private float damageTimer = 1.0f;
    List<GameObject> collidedObjects = new List<GameObject>();
    [SerializeField]
    private int zombiesAttacking = 0;


    public HealthBar_Freezer healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Data.freezerTransform = transform;
        Data.gameLost = false;
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if(Data.reset)
        {
            ResetHealth();
        }
        else if(damageTimer >= 0)
        {
            damageTimer -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            zombiesAttacking++;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie") && damageTimer <= 0)
        {
            freezerTakeDamage();

        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            zombiesAttacking--;
        }
    }

    void freezerTakeDamage()
    {
        damageTimer = 1.0f;
        int damageTaking = (zombiesAttacking * damageDelt);
        currentHealth -= damageTaking;
        healthBar.SetHealth(currentHealth);
        freezerDestroyed();
    }

    void freezerDestroyed()
    {
        if (currentHealth < 1)
        {
            Data.gameLost = true;
        }
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}