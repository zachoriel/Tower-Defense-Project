using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public ObjectSpawner objects;

    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]                                                                                                                                                                    // Creates a header in the inspector so that things are a little more organized
    public float range = 15f;
    
    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public ParticleSystem muzzleEffect;
    public AudioSource fireAudio;
    public float fireRate = 1f;                                                                                                                                                               // x shots per second
    private float fireCountdown = 0f;                                                                                                                                                         // Default 0 so we can fire immediately

    [Header("Use Laser")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowAmount = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    public AudioSource laserSound;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public Transform firePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);                                                                                                                                            // Repeatedly runs the UpdateTarget() method once at the start (0f), and then twice/frame (0.5f seconds)
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);                                                                                                                   // Finds all objects with the given tag and stores them in this array
        float shortestDistance = Mathf.Infinity;                                                                                                                                              // Default shortest distance is infinity (If we haven't targeted an enemy yet, then there's no distance to it!)
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);                                                                                           // For each enemy in the enemies array, the distance from the turret to the enemy is calculated by subtracting the enemy's transform position from the turret's
            if (distanceToEnemy < shortestDistance)                                                      
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)                                                                                                                                // Enemy is found and is within turret range
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }

    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    laserSound.Stop();
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }

            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void LockOnTarget()
    {
        //Target lock-on
        Vector3 dir = target.position - transform.position;                                                                                                                                    // Direction is our position subtracted from the target's position (B - A)
        Quaternion lookRotation = Quaternion.LookRotation(dir);                                                                                                                                // Quaternions are how Unity deals with rotation. Don't worry about it too much for now
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;                                                                       // x, y, and z rotation are euler angles. We stored our quaternion information in an euler so that we can rotate on a specific axis on the next line. Lerping is a method that helps to smoothen out transitions, such as suddenly locking onto a new target. In order: we want to smoothen out our current rotation to our new rotation, over time, dictated by our defined turn speed. 
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);                                                                                                                         // Rotates solely on the y axis, now handled by a euler which is handled by a quaternion
    }

    void Laser()
    {
        if (GameManager.GameIsOver == true)
        {
            laserSound.Stop();
        }

        if (GameManager.GameIsPaused == true)
        {
            laserSound.Pause();
        }
        else
        {
            laserSound.UnPause();
        }

        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            laserSound.Play();
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();                                                                                                                                       // References our bullet script so that we can use our Seek() method

        if (bullet != null)
        {
            fireAudio.Play();
            muzzleEffect.Play();
            bullet.Seek(target);
            objects.CasingEffect();
        }
    }

    void OnDrawGizmosSelected ()                                                                                                                                                               // Gizmos are tools that allow us to see things we're doing in our code graphically
    {                                                                                                     
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);                                                                                                                                      // In this case, in Unity we'll see a red wire sphere detailing our range variable
    }
}
