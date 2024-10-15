using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private avriables
    private float speed = 20.0f;
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

    // Start is called before the first frame update
    void Start()
    {
        
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

            canShoot = true;
            Destroy(collision.gameObject);

            powerUpEffect.SetActive(true);

            StartCoroutine(PowerUpDuration(5f));
        }
    }
    private void GameOver()
    {
        // Stop time to freeze the game
        Time.timeScale = 0f;

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

}
