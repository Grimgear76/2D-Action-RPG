using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Load your main game scene
    public void PlayGame()
    {
        Debug.Log("loading game");
        SceneManager.LoadScene("Tutorial_1"); // Change to your scene name
    }

   
}
