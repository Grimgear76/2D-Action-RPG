using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Load your main game scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_1"); // Change to your scene name
    }

   
}
