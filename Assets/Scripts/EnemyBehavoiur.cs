using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavoiur : MonoBehaviour
{
    public float enemyHealth = 100f;

    [Header("Speed")]
    public float speed = 2f;
    public float speedX;

    [Header("Limites")]
    public float limiteX = 4f;

    [Header("Disparo")]
    public GameObject prefabDisparo;
    public float disparoSpeed = 2f;
    public float shootingInterval = 6f;
    public float timeDisparoDestroy = 2f;

    private float _shootingTimer;
    private bool canShoot = true;
    private float disableTimer = 0f;

    public Transform weapon1;
    public Transform weapon2;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        _shootingTimer = Random.Range(0f, shootingInterval);

        speedX = Random.Range(-0.5f, 0.5f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, speed);
    }

    // Update is called once per frame
    void Update()
    {
        StartFire();
        DestroyEnemy();

        if (!canShoot)
        {
            disableTimer -= Time.deltaTime;
            if (disableTimer <= 0f)
            {
                canShoot = true;
            }
        }
    }

    public void StartFire()
    {
        if (canShoot)
        {
            _shootingTimer -= Time.deltaTime;
            if (_shootingTimer <= 0f)
            {
                _shootingTimer = shootingInterval;
                GameObject disparoInstance = Instantiate(prefabDisparo);
                disparoInstance.transform.SetParent(transform.parent);
                disparoInstance.transform.position = weapon1.position;

                disparoInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance, timeDisparoDestroy);

                GameObject disparoInstance2 = Instantiate(prefabDisparo);
                disparoInstance2.transform.SetParent(transform.parent);
                disparoInstance2.transform.position = weapon2.position;

                disparoInstance2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance2, timeDisparoDestroy);
            }
        }
    }

    public void DestroyEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "disparoPlayer")
        {
            Destroy(otherCollider.gameObject); 
            GotHit();
        }
        else if (otherCollider.tag == "Player")
        {
            Destroy(otherCollider.gameObject);
        }
    }

    public void GotHit()
    {
        enemyHealth -= 10f;

        if (enemyHealth <= 0)
        {
            Die();
        }
        else
        {
            canShoot = false;
            disableTimer = 3f;
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

