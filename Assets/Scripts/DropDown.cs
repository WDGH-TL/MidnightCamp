using UnityEngine;
using TMPro;


public class DropDown : MonoBehaviour
{
    public string selectedDropDown { get; set; } = "";
    public TMP_Dropdown dropdownCraft;

    public void selectOption()
    {
        int index = dropdownCraft.value;
        string selectedOption = dropdownCraft.options[index].text;
        saveDropdownValue(selectedOption);
    }

    public void fill(string newItem)
    {
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

    public void saveDropdownValue(string Name)
    {

        this.selectedDropDown = Name;

    }
}