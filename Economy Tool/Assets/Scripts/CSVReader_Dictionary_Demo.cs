using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CSVReader_Dictionary_Demo : MonoBehaviour
{
    public GameObject m_DropdownList;
    public GameObject m_Level;
    public GameObject m_Minutes;
    public GameObject m_Number;

    InputField  m_InputLevel;
    InputField  m_InputMinutes;
    InputField  m_InputNumber;
    string      m_ActiveOption;
    Dropdown    m_Dropdown;
    Dictionary<string, Dictionary<string, object>> data, data_2;

    void Awake()
    {

       data = CSVReader_Dictionary.Read("page_1");
       data_2 = CSVReader_Dictionary.Read("page_2");
    }

    // Use this for initialization
    void Start()
    {
        m_Dropdown      = m_DropdownList.GetComponent<Dropdown>();
        m_InputLevel    = m_Level.GetComponent<InputField>();
        m_InputMinutes  = m_Minutes.GetComponent<InputField>();
        m_InputNumber   = m_Number.GetComponent<InputField>();

        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChangedHandler(m_Dropdown);
        });
        foreach (KeyValuePair<string, Dictionary<string,object>> entry in data)
        {
            m_Dropdown.options.Add(new Dropdown.OptionData(entry.Key));
        }

    }

    // Update is called once per frame
    void Update()
    {

  
    }
    //Function notifying that the selection changed
    private void DropdownValueChangedHandler(Dropdown target)
    {
        Debug.Log("selected: " + target.captionText.text);
        m_ActiveOption = target.captionText.text;
        UpdateSelectedOption();
    }

    private void UpdateSelectedOption( )
    {
        object result;
        if (data[m_ActiveOption].TryGetValue("Level", out result))
        {
            m_InputLevel.text = result.ToString();
        }
        if (data[m_ActiveOption].TryGetValue("Minutes", out result))
        {
            m_InputMinutes.text = result.ToString();
        }
        if (data[m_ActiveOption].TryGetValue("Number", out result))
        {
            m_InputNumber.text = result.ToString();
        }
    }

    public void UpdateValues()
    {

        data[m_ActiveOption]["Level"]   = m_InputLevel.text;
        data[m_ActiveOption]["Minutes"] = m_InputMinutes.text;
        data[m_ActiveOption]["Number"]  = m_InputNumber.text;
        Debug.Log("I updated the values");
    }
}
