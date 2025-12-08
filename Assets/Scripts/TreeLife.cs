using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TreeLife : MonoBehaviour
{
    public Slider lifeSlider;
    public Transform playerTransform;
    public float spawnDistance = 2f;
    public GameObject itemPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
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
            spawnItem();
        }
    }
    public void spawnItem()
    {
        if (itemPrefab != null)
        {
            Vector3 targetPosition = playerTransform.position + playerTransform.forward * spawnDistance;
            Quaternion targetRotation = playerTransform.rotation;

            GameObject nuevoItem = Instantiate(itemPrefab, targetPosition, targetRotation);
            //nuevoItem.transform.localScale = Vector3.one;
            nuevoItem.SetActive(true);
            Rigidbody rb = nuevoItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            }
        }
    }
}
