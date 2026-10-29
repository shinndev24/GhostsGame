using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] string gameScene = "Prologue";

    public void Play()
    {
        SceneManager.LoadScene(gameScene);
    }
}
