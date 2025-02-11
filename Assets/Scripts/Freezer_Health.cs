using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer_Health : MonoBehaviour
{
    [SerializeField]
    public float currentHealth;
    [SerializeField]
    public float maxHealth = 100;
    [SerializeField]
    private int damageDelt = 1;
    [SerializeField]
    private float damageTimer = 1.0f;
    List<GameObject> collidedObjects = new List<GameObject>();
    [SerializeField]
    private int zombiesAttacking = 0;
    private int music = 1;
    [SerializeField] private AudioSource calmMusic;
    [SerializeField] private AudioSource panikMusic;


    public HealthBar_Freezer healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Data.freezerTransform = transform;
        Data.gameLost = false;
        ResetHealth();
        calmMusic.loop = true;
        calmMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        damageTimer -= Time.deltaTime;
        if (Data.reset)
        {
            ResetHealth();
        }
        else if(damageTimer <= 0)
        {
            damageTimer = 1f;
            var nearby = Physics2D.OverlapCircleAll(transform.position, 12);
            float damage = 0f;
            foreach(var near in nearby)
            {
                if(near.CompareTag("Zombie"))
                {
                    damage += near.GetComponent<ZombieController>().damage;
                }
            }
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            if(currentHealth <= 0)
            {
                panikMusic.Stop();
                Data.gameLost = true;
            }
        }

        if((music == 1) && (currentHealth < 34))
        {
            music++;
            LowHealthMusic();
        }
    }

    private void LowHealthMusic()
    {
        calmMusic.Stop();
        panikMusic.Play();
        panikMusic.loop = true;
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}