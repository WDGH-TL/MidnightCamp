using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform playerTransform;
    public float spawnDistance = 2f;
    public GameObject itemPrefab;

    public void spawnItem()
    {
        if (itemPrefab != null)
        {
            Vector3 targetPosition = playerTransform.position + playerTransform.forward * spawnDistance;
            Quaternion targetRotation = playerTransform.rotation;

            GameObject nuevoItem = Instantiate(itemPrefab, targetPosition, targetRotation);
            nuevoItem.transform.localScale = Vector3.one;
            nuevoItem.SetActive(true);
            Rigidbody rb = nuevoItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            }
        }
    }
}
