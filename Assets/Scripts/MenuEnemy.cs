using UnityEngine;
using UnityEngine.UI;

public class MenuEnemy : MonoBehaviour
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float startHealth = 100f;
    private float health;

    //public int moneyValue = 50; 

    //public int scoreValue = 10;

    public GameObject deathEffect;

    //[Header("Unity Stuff")]
    //public Image healthBar; 

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        //healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    //public void Slow(float amount)
    //{
    //    speed = startSpeed * (1f - amount);
    //}

    void Die()
    {
        //PlayerStats.Money += moneyValue;

        //PlayerStats.Score += scoreValue;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }
}
