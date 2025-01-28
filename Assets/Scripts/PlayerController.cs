using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for the Text component
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject WinLoseBG;
    public Text WinLoseText;
    public Text healthText; //link the healthText to UI elemnt in the Inspector
    public Text scoreText; // Link the scoreText UI element in the Inspector
    public float Speed = 5f; // Movement speed
    public int health = 5; // Player's health

    private int score; // Player's score
    private Rigidbody rb; // Rigidbody component reference
    IEnumerator LoadScene(float seconds)
    {
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(seconds);

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        score = 0; // Initialize score
        SetScoreText(); // Update the scoreText UI
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            WinLoseText.text = "You Win!";
            WinLoseText.color = Color.black;
            WinLoseBG.GetComponent<Image>().color = Color.green;
            WinLoseBG.SetActive(true);
            StartCoroutine(LoadScene(3));
            //Debug.Log("You Win!");
        }

        if (other.CompareTag("Trap"))
        {
            health--; // Decrease health
            SetHealthText();
            //Debug.Log("Health: " + health);
        }

        if (other.CompareTag("Pickup"))
        {
            score++; // Increment the score
            SetScoreText(); // Update the scoreText UI
            //Debug.Log("score: " + score); // Log the updated score

            other.gameObject.SetActive(false); // Disable the Pickup object
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            WinLoseText.text = "Game Over!";

            // Change text color to white
            WinLoseText.color = Color.white;

            // Change background color to red
            WinLoseBG.GetComponent<Image>().color = Color.red;

            // Activate the WinLoseBG GameObject
            WinLoseBG.SetActive(true);
            
            //Debug.Log("Game Over!");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the scene
            StartCoroutine(LoadScene(3));
        }
    }

    void FixedUpdate()
    {
        // Get input from WASD/Arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Adjust movement speed for sprinting
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            rb.linearVelocity = movement * Speed * 2; // Double speed
        }
        else
        {
            rb.linearVelocity = movement * Speed; // Normal speed
        }
    }

    void SetScoreText()
    {
        // Update the scoreText UI with the current score
        scoreText.text = "Score: " + score.ToString();
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }
}
