using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Thirst : MonoBehaviour
{
    public Slider thirstSlider;

    public float maxThirst = 100f;
    public float currentThirst;
    public float decayThirst = 0.75f;

    public static Thirst instance;

    public void Start()
    {
        currentThirst = maxThirst;

        if (thirstSlider != null)
        {
            thirstSlider.maxValue = maxThirst;
            thirstSlider.value = currentThirst;
        }

        if (instance == null)
        {
            instance = this;
        }
    }

    public void Update()
    {
        if (currentThirst > 0)
        {
            currentThirst -= decayThirst * Time.deltaTime;
        }

        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);

        if (thirstSlider != null)
        {
            thirstSlider.value = currentThirst;
        }

        if (currentThirst <= 0)
        {
            Debug.Log("El jugador ha muerto por hambre o sed.");
        }
    }

    public void AddThirst(float amount)
    {
        currentThirst += amount;
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);
    }
    IEnumerator recover(float amount)
    {

        float vidaObjetivo = thirstSlider.value + amount;
        if (vidaObjetivo < 0)
        {
            vidaObjetivo = 0;
        }

        while (thirstSlider.value > vidaObjetivo)
        {

            thirstSlider.value += 1f;
            yield return new WaitForSeconds(0.03f);
        }

        thirstSlider.value = vidaObjetivo;

    }
}