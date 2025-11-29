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

    // --- FUNCIÓN PARA AÑADIR/RESTAURAR SED (NUEVA) ---
    public void AddThirst(float amount)
    {
        currentThirst += amount;
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);
    }
}