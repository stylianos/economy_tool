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
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChangedHandler(m_Dropdown);
        });
        foreach (KeyValuePair<string, Dictionary<string,object>> entry in data_2)
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
       
    }

    public void UpdateValues()
    {

    }
}
