using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //private avriables
    public float speed = 20.0f;
    private float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;

    private Rigidbody playerRb;

    public GameObject projectilePrefab; // Assign your projectile prefab here
    public Transform shootingPoint; // The point from where the projectile is shot (e.g., gun barrel or player's front)
    public float projectileSpeed = 20f; // Speed of the projectile
    private bool canShoot = false;

    private int badObjectHitCount = 0; // Tracks the number of "Bad" objects hit
    public int maxBadHits = 3;

    public GameObject powerUpEffect;
    public GameObject starEffect;

    public ParticleSystem particleEffect;
    public ParticleSystem explosionParticle;

    // Audio
    public AudioClip startUpSound;
    public AudioClip idleSound;
    public AudioClip crashSound;
    public AudioClip shootSound;
    public AudioClip powerUpSound;

    private AudioSource playerAudio;



    // Reference to the particle system

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        playerAudio.PlayOneShot(startUpSound, 1.4f);

        // Set up the idle sound to loop, but don’t start playing it immediately
        playerAudio.clip = idleSound;
        playerAudio.loop = true;
        playerAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //this is where we get player input 
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //turns input into vehicle movement
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);



        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            Shoot();
        }


        if (IsOffRoad())
        {
            GameOver();
        }

    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);

        // Get the Rigidbody component of the instantiated projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Check if the Rigidbody is found
        if (rb != null)
        {
            // Add velocity to the projectile to make it move forward
            rb.velocity = shootingPoint.forward * projectileSpeed;
            playerAudio.PlayOneShot(shootSound, 1.4f);
        }

        //Destroy object after some time
        Destroy(projectile, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bad"))
        {
            Destroy(collision.gameObject);
            badObjectHitCount++; // Increase the hit counter
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.4f);

            Debug.Log("Bad Obstacle Hit! Lives Remaining: " + (3-badObjectHitCount));

            // Check if the player has hit 3 bad objects
            if (badObjectHitCount >= maxBadHits)
            {
                Debug.Log("Game Over! Player hit 3 Bad objects.");
                GameOver(); // Stop the game when the player hits 3 bad objects
            }

        }
        else if (collision.gameObject.CompareTag("Good"))
        {
            Debug.Log("Power Up Attained!");

            playerAudio.PlayOneShot(powerUpSound, 1.4f);

            canShoot = true;
            Destroy(collision.gameObject);

            powerUpEffect.SetActive(true);

            StartCoroutine(PowerUpDuration(5f));
        }
        else if (collision.gameObject.CompareTag("Star"))
        {
            Debug.Log("Star Collected!");
            starEffect.SetActive(true);
            Destroy(collision.gameObject);

            //not implemented yet but player will have to deposit star by contacting it with dumpster at the end of the road

        }
    }
    private void GameOver()
    {
        // Stop time to freeze the game
        Time.timeScale = 0f;
        explosionParticle.Play();
        // Optionally display a Game Over message
        Debug.Log("Game Over");
    }

    private System.Collections.IEnumerator PowerUpDuration(float duration)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Disable shooting ability after the duration ends
        canShoot = false;
        powerUpEffect.SetActive(false);
        Debug.Log("Power Up Expired!");
    }

    private bool IsOffRoad()
    {
        if(playerRb.position.y < -10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
