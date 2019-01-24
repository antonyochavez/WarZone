using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float health = 100; // enemy starting health
    [SerializeField] GameObject deathVFX; // explosion animation
    [SerializeField] float explosionDuration = 0.15f; // how long explosion lasts
    [SerializeField] AudioClip deathSFX; // death sound effects
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.2f; // death sound volume
    [SerializeField] int scoreValue = 100;

    [Header("Enemy Shooting")]
    [SerializeField] float shotCounter = 0f; // timer before firing next bullet
    [SerializeField] float minShotTime = 0.2f; // min time before firing next bullet
    [SerializeField] float maxShotTime = 0.6f; // max time befor firing next bullet
    [SerializeField] GameObject projectile; // link to enemy bullet using Unity
    [SerializeField] float projectileSpeed = 10f; // speed of enemy bullet
    [SerializeField] AudioClip bulletSFX; // bullet sound effects
    [SerializeField] [Range(0, 1)] float bulletSFXVolume = 0.05f; // bullet sound volume
    float bulletPadding = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minShotTime, maxShotTime); /* random
        factor which randomizes rate of fire such that time between firing is between
        minShotTime and maxShotTime */      
    }

    // Update is called once per frame
    void Update()
    {
        CountAndShoot(); 
    }

    private void CountAndShoot() // method that counts down and shoots 
    {
        shotCounter -= Time.deltaTime; // counts down to shotCounter 
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minShotTime, maxShotTime); /* random
            factor which randomizes rate of fire per frame */           
        }
    }

    private void Fire() // method that fires a bullet 
    {
        var bulletYPosition = transform.position.y - bulletPadding; // y-position of bullets
        var bulletPosition = new Vector2(transform.position.x, bulletYPosition); /* position that
            bullets will be instatiated at which is in front of the enemy */
        GameObject bullet = Instantiate(
            projectile,
            bulletPosition,
            Quaternion.identity) as GameObject; // instantiates enemy bullet
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        // adds speed to enemy bullet
        AudioSource.PlayClipAtPoint(bulletSFX, Camera.main.transform.position, bulletSFXVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision) // method that occurs upon collision
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; } // debug/defence purposes
        HitTillDeath(damageDealer);
    }

    private void HitTillDeath(DamageDealer damageDealer) // method for getting hit and dying
    {
        health -= damageDealer.GetDamage(); // makes enemy lose health upon collision
        damageDealer.Hit(); // destroys player projectile upon collision
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death() // death sequence
    {
        FindObjectOfType<ScoreTracker>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
