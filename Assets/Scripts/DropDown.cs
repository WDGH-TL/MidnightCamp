using UnityEngine;
using TMPro;


public class DropDown : MonoBehaviour
{

    public TMP_Dropdown dropdownCraft;

    public void selectOption()
    {
        int index = dropdownCraft.value;
        string selectedOption = dropdownCraft.options[index].text;
        Debug.Log("Seleccionaste el índice: " + index + " " + selectedOption);
    }

    public void fill(string newItem)
    {
        Debug.Log("Adding item to dropdown: " + newItem);
        dropdownCraft.options.Add(item: new TMP_Dropdown.OptionData(text: newItem));
        dropdownCraft.RefreshShownValue();
    }

    public void delete()
    {
        int index = 0;
        dropdownCraft.options.RemoveAt(index);

        if (dropdownCraft.value == index)
        {
            dropdownCraft.value = 0;
        }

        dropdownCraft.RefreshShownValue();
    }

    public int findIndex(string text)
    {
        for (int i = 0; i < dropdownCraft.options.Count; i++)
        {
            if (dropdownCraft.options[i].text == text)
            {

                return i;
            }
        }


        return -1;
    }


    public string getSelectedOption()
    {
        int index = dropdownCraft.value;
        return dropdownCraft.options[index].text;
    }
}