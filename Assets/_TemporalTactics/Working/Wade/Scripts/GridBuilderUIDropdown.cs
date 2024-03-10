using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridBuilderUIDropdown : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] GridBuilder gridBuilder;

    void Start()
    {
        // Clear existing options
        dropdown.ClearOptions();

        // Get all enum values and convert them to strings
        string[] enumNames = System.Enum.GetNames(typeof(TileType));

        // Create a list of TMP_Dropdown.OptionData objects to hold the enum names
        var options = new List<TMP_Dropdown.OptionData>();

        // Loop through each enum name and add it to the list of options
        foreach (string enumName in enumNames)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(ConvertPascalCaseToReadable(enumName));
            options.Add(option);
        }

        // Set the dropdown options
        dropdown.AddOptions(options);

        dropdown.onValueChanged.AddListener(value =>
        {
            gridBuilder.SelectedTileType = (TileType)value;
        });
    }

    public string ConvertPascalCaseToReadable(string pascalCaseString)
    {
        // Use regular expression to add spaces between words
        return Regex.Replace(pascalCaseString, "(\\B[A-Z])", " $1");
    }
}
