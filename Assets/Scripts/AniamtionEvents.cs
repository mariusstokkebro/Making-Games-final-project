using UnityEngine;
using UnityEngine.SceneManagement;

public class AniamtionEvents : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
}
