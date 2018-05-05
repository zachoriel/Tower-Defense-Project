using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameManager manager;

    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float startHealth = 100f;
    private float health;
    public float healthRegeneration = 0f;

    public float startShield = 100f;
    private float shield;
    public float shieldRegeneration = 0f;

    private bool isDead = false;

    public int moneyValue = 10;
    public int bonus;

    public int scoreValue = 10;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;
    public Image shieldBar;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
        shield = startShield;
    }

    public void TakeDamage(float amount)
    {
        shield -= amount;

        if (shield <= 0)
        {
            shield = 0;
            health -= amount;
        }

        if (health >= startHealth)
        {
            health = startHealth - amount;
        }

        if (shield >= startShield)
        {
            shield = startShield;
        }

        //healthBar.fillAmount = health / startHealth;
        //shieldBar.fillAmount = shield / startShield;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (1f - amount);
    }

    void Update()
    {
        float dist = Vector3.Distance(manager.startPos.transform.position, gameObject.transform.position);
        bonus = Mathf.RoundToInt(dist) / 2;

        health += healthRegeneration * Time.deltaTime;
        healthBar.fillAmount = health / startHealth;

        shield += shieldRegeneration * Time.deltaTime;
        shieldBar.fillAmount = shield / startShield;
    }

    void Die()
    {
        isDead = true;

        PlayerStats.Money += moneyValue + bonus;

        PlayerStats.Score += scoreValue;
        PlayerPrefs.SetInt("Score", PlayerStats.Score);

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }
}
