using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public TextMeshProUGUI objectName;
    public Image spriteImage;


    public int inventoryIndex;
    public GameObject ItemUI;

    private void Awake()
    {
        Instance = this;
    }

    public void Display(Items item)
    {
        objectName.text = item.itemTemplate[inventoryIndex].name;
    }

    public string GetProductName(Items item)
    {
        return item.itemTemplate[inventoryIndex].name;
    }
}
