using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    public Slider hungerSlider;

    public float maxHunger = 100f;
    public float currentHunger;
    public float decayHunger = 0.25f;

    public static Hunger instance;

    public void Start()
    {
        currentHunger = maxHunger;

        if (hungerSlider != null)
        {
            hungerSlider.maxValue = maxHunger;
            hungerSlider.value = currentHunger;
        }

        if (instance == null)
        {
            instance = this;
        }
    }

    public void Update()
    {
        if (currentHunger > 0)
        {
            currentHunger -= decayHunger * Time.deltaTime;
        }

        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);

        if (hungerSlider != null)
        {
            hungerSlider.value = currentHunger;
        }

        if (currentHunger <= 0)
        {
            Debug.Log("El jugador ha muerto por hambre o sed.");
        }
    }

    public void AddHunger(float amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
    }
    IEnumerator recover(float amount)
    {

        float vidaObjetivo = hungerSlider.value + amount;
        if (vidaObjetivo < 0)
        {
            vidaObjetivo = 0;
        }

        while (hungerSlider.value > vidaObjetivo)
        {

            hungerSlider.value += 1f;
            yield return new WaitForSeconds(0.03f);
        }

        hungerSlider.value = vidaObjetivo;

    }
}