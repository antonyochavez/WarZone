using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // game configuration
    [Header("Player")]
    [SerializeField] float movementSpeed = 12f; //player movement speed
    [SerializeField] float padding = 0.8f; //spacing between screen borders
    [SerializeField] float health = 500f; //player starting health
    [SerializeField] GameObject deathVFX; // explosion animation
    [SerializeField] float explosionDuration = 0.15f; // how long explosion lasts
    [SerializeField] AudioClip deathSFX; // death sound effects
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.2f; // death sound volume

    [Header("Bullets")]
    [SerializeField] GameObject playerBullet; //link to player bullet using Unity
    [SerializeField] float bulletSpeed = 15f; //speed of player bullet
    [SerializeField] float bulletFiringPeriod = 0.1f; //interval between firing lasers
    [SerializeField] AudioClip bulletSFX; // bullet sound effects
    [SerializeField] [Range(0, 1)] float bulletSFXVolume = 0.05f; // bullet sound volume
    float bulletPadding = 0.5f;

    Coroutine shootingCoroutine;

    // position variables 
    float xMin; 
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        MoveBoundaries();
    }



    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    private void OnTriggerEnter2D(Collider2D collision) // method triggered upon collision
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>(); 
        if (!damageDealer) { return; } // debug/defence purposes
        HitTillDeath(damageDealer);
    }

    private void HitTillDeath(DamageDealer damageDealer) // method for getting hit and dying
    {
        health -= damageDealer.GetDamage(); // makes player lose health upon collision
        damageDealer.Hit(); // destroys damage dealing object (projectiles)
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death() // death sequence
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    private void MoveBoundaries() // method for limiting movement to screen boundaries
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move() // method for movement
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed; /* registers movement 
        from left/right or a/d, and ensures speed is kept consistent for different devices */
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed; /* registers movement 
        in similar manner to deltaX but for vertical movement (up/down , w/s) */      

        var xPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax); //updates x position
        var yPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax); //updates y position
        transform.position = new Vector2(xPos, yPos); //sets new position 
    }

    private void Shoot() // method for shooting bullets
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shootingCoroutine = StartCoroutine(ConstantFire());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(shootingCoroutine);
        }
    }   

    IEnumerator ConstantFire() // coroutine that allows player to hold shoot button and keep shooting
    {
        while (true)
        {
            var bulletYPosition = transform.position.y + bulletPadding; // y-position of bullets
            var bulletPosition = new Vector2(transform.position.x, bulletYPosition); /* position that
            bullets will be instatiated at which is in front of the player*/           
            GameObject laser = Instantiate(
                playerBullet,
                bulletPosition,
                Quaternion.identity) as GameObject; // creates lasers 
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed); // adds velocity to lasers
            AudioSource.PlayClipAtPoint(bulletSFX, Camera.main.transform.position, bulletSFXVolume);
            yield return new WaitForSeconds(bulletFiringPeriod);
        }

    }

    public float GetHealth()
    {
        return health;
    }

}
