using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTemplate", menuName = "ScriptableObjects/Items")]
public class Items : ScriptableObject
{
    public RESOURCES[] itemTemplate;
}