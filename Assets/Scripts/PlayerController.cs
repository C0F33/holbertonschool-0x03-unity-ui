using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5f; // Movement speed
    public int Score = 0;   // Player's score
    public int health = 5; // Player's health

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            Debug.Log("You Win!");
        }
        if (other.CompareTag("Trap"))
        {
            health--;
            Debug.Log("Health: " + health);
        }
        // Check if the colliding object is tagged as "Pickup"
        if (other.CompareTag("Pickup"))
        {
            // Increment the score
            Score++;

            // Log the new score to the console
            Debug.Log("Score: " + Score);

            // Disable or destroy the coin object
            other.gameObject.SetActive(false); // Option 1: Disable
            // Destroy(other.gameObject);      // Option 2: Destroy
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Game Over!");
        }
    }

    void FixedUpdate()
    {
        // Get input from WASD/Arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            // Double the movement speed
            rb.linearVelocity = movement * Speed * 2;
        }
        else
        {
            // Apply normal movement speed to the Rigidbody
            rb.linearVelocity = movement * Speed;
        }
    }
}