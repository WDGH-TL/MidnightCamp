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
            case "Water":
                prefab = prefab2;
                break;
            case "Apple":
                prefab = prefab3;
                break;
            case "Wood":
                prefab = prefab4;
                break;
            case "Rock":
                prefab = prefab5;
                break;
        }

        return prefab;
    }
}
