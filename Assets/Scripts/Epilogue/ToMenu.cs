using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour
{
    [SerializeField] string gameScene = "Menu";

    public void GoToMenu()
    {
        SceneManager.LoadScene(gameScene);
    }
}
