using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void jugar()
    {
        SceneManager.LoadScene("Game");
    }

    public void tutorial()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void eaten()
    {
        SceneManager.LoadScene("Eaten");
    }

    public void escaped()
    {
        SceneManager.LoadScene("Escaped");
    }

    public void salir()
    {
        Application.Quit();
    }
}
