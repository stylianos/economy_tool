using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Basic_Logic : MonoBehaviour
{
    public GameObject m_DropdownList;
    public GameObject m_DropdownLabel;
    public GameObject m_InputPair;
    public InputField m_InputLevel;
    public InputField m_InputMinutes;
    public InputField m_InputNumber;
    public RectTransform m_Content;
    public Text m_Output;
    private bool m_OptionSet;
    string m_ActiveOption;
    Dropdown m_Dropdown;
    SortedDictionary<string, Dictionary<string, int>> data;
    GameObject m_Reader;

    void Awake()
    {

        //data = CSVReader_Dictionary.ReadStringSorted("page_1");
        m_OptionSet = false;
    }

    // Use this for initialization
    void Start()
    {
        m_Reader = GameObject.FindWithTag("Reader");
        //Get the appropriate data
        data = m_Reader.GetComponent<Reader>().data;
        m_Dropdown = m_DropdownList.GetComponent<Dropdown>();
        //Adding the appropriate delegates for the dropdown events
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChangedHandler(m_Dropdown);
        });
      
        foreach (KeyValuePair<string, Dictionary<string, int>> entry in data)
        {

            m_Dropdown.options.Add(new Dropdown.OptionData(entry.Key));
            //Set the first value
            if (!m_OptionSet)
            {
                m_DropdownLabel.GetComponent<Text>().text = entry.Key;
                m_OptionSet = true;
                m_ActiveOption = entry.Key;
            }
        }
        m_DropdownList.GetComponent<Dropdown>().value = 0;
        UpdateSelectedOption();

    }

    // Update is called once per frame
    void Update()
    {

    }
    //Function notifying that the selection changed
    private void DropdownValueChangedHandler(Dropdown target)
    {
        //Debug.Log("selected: " + target.captionText.text);
        m_ActiveOption = target.captionText.text;
        UpdateSelectedOption();
    }

    private void UpdateSelectedOption()
    {
        Debug.Log("This is the selected option " + m_ActiveOption);
        foreach (KeyValuePair<string, int> entry in data[m_ActiveOption])
        {
            //Get one by one the appropriate input fields and update their values
            if ( entry.Key.ToString() == "Level" )
            {
                m_InputLevel.text = entry.Value.ToString();
            }
            if (entry.Key.ToString() == "Minutes")
            {
               m_InputMinutes.text = entry.Value.ToString();
            }
            if (entry.Key.ToString() == "Number")
            {
               m_InputNumber.text = entry.Value.ToString();
            }
        }
    }

    public void UpdateValues()
    {
        //You cannot use a for each loop in order to change values, I am putting every key in a list and iterate that list instead. 

        try
        {
            data[m_ActiveOption]["Number"] = Int32.Parse(m_InputNumber.text);
        }
        catch (FormatException)
        {
            m_Output.color = Color.yellow;
            m_Output.text = "Wrong format exception!! Mistake at  " + m_InputNumber.text;
            return;
        }
        catch (OverflowException)
        {
            m_Output.color = Color.yellow;
            m_Output.text = "Overflow exception!!" + data[m_ActiveOption]["Number"];
            return;
        }
        try
        {
            data[m_ActiveOption]["Level"] = Int32.Parse(m_InputLevel.text);
        }
        catch (FormatException)
        {
            m_Output.color = Color.yellow;
            m_Output.text = "Wrong format exception!! Mistake at  " + m_InputLevel.text;
            return;
        }
        catch (OverflowException)
        {
            m_Output.color = Color.yellow;
            m_Output.text = "Overflow exception!!" + data[m_ActiveOption]["Level"];
            return;
        }
        try
        {
            data[m_ActiveOption]["Minutes"] = Int32.Parse(m_InputMinutes.text);
        }
        catch (FormatException)
        {
            m_Output.color = Color.yellow;
            m_Output.text = "Wrong format exception!! Mistake at  " + m_InputMinutes.text;
        }
        catch (OverflowException)
        {
            m_Output.color = Color.yellow;
            m_Output.text = "Overflow exception!!" + data[m_ActiveOption]["Minutes"];
            return;
        }

        Debug.Log("I updated the values");
        m_Output.color = Color.white;
        m_Output.text = "I updated the values";
        //CalculateTimeToBuild();
    }
}
