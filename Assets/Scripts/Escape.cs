using UnityEngine;

public class Escape : MonoBehaviour
{
    public SceneChanger sceneManager;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("KeyItem"))
        {
            sceneManager.escaped();
        }
    }
}
