using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour
{
    public Slider lifeSlider;


    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Weapon"))
        {
            triggerDamage(25);
        }

    }

    public void triggerDamage(float damage)
    {
        if (lifeSlider.value <= 0) return;
        StartCoroutine(bajarVida(damage));
    }

    IEnumerator bajarVida(float damage)
    {

        float vidaObjetivo = lifeSlider.value - damage;
        if (vidaObjetivo < 0)
        {
            vidaObjetivo = 0;
        }

        while (lifeSlider.value > vidaObjetivo)
        {

            lifeSlider.value -= 1f;
            yield return new WaitForSeconds(0.03f);
        }

        lifeSlider.value = vidaObjetivo;

        if (lifeSlider.value == 0)
        {

            // Destroy(gameObject);

        }
    }

}