using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<TextMeshProUGUI> nameList;
    public List<Image> spriteList;

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
    public bool drawSprites(Sprite sprt)
    {
        Debug.Log(sprt);
        foreach (Image IMG in spriteList)
        {
            if (IMG == null)
            {
                continue;
            }
            if (IMG.sprite == null)
            {
                IMG.sprite = sprt;
                IMG.color = Color.white;
                IMG.enabled = true;
                return true;
            }
        }
        return false;
    }
}
