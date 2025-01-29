using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Color defaultTrapColor;
    private Color defaultGoalColor;
    
    public Material trapMat;
    public Material goalMat;

    public Toggle colorblindMode;

    void Start()
    {
        if (colorblindMode != null)
        {
            colorblindMode.isOn = false;  // Turn off colorblind mode by default
        }

        // Store the original material colors at the start of the game
        defaultTrapColor = trapMat.color;
        defaultGoalColor = goalMat.color;
        
        // Ensure material colors are set to defaults on scene load
        ApplyMaterialColors();
    }

    public void PlayMaze()
    {
        if (colorblindMode != null && colorblindMode.isOn)
        {
            // Set the colors for colorblind mode
            trapMat.color = new Color32(255, 112, 0, 255); // Orange
            goalMat.color = Color.blue; // Blue
        }
        else
        {
            // Explicitly reset to the original colors when Colorblind Mode is off
            trapMat.color = new Color32(255, 0, 0, 255);
            goalMat.color = new Color32(0, 255, 0, 255); // Green (#00FF00FF)
        }

        // Load the maze scene
        SceneManager.LoadScene("maze"); // Ensure "maze" matches the exact scene name
    }

    void ApplyMaterialColors()
    {
        // Reapply the default colors to the materials
        trapMat.color = defaultTrapColor;
        goalMat.color = defaultGoalColor;
    }
}