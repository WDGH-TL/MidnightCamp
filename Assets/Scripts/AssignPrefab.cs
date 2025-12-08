using UnityEngine;

public class AssignPrefab : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;
    public GameObject prefab5;

    public GameObject assignedPrefab(string itemName)
    {
        GameObject prefab = null;
        
        switch(itemName)
        {
            case "Crowbar":
                prefab = prefab1;
                break;
        }

        return prefab;
    }
}
