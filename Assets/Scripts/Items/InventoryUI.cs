using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public List<TextMeshProUGUI> nameList;

    public bool drawNames(string name)
    {
        Debug.Log(name);
        foreach(TextMeshProUGUI TMP in nameList)
        {
            if (TMP == null)
            {
                continue;
            }
            if (string.IsNullOrWhiteSpace(TMP.text))
            {
                TMP.text = name;
                return true;
            }
        }
        return false;
    }
}
