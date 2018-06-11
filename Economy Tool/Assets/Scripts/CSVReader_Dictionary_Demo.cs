using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CSVReader_Dictionary_Demo : MonoBehaviour
{
    public GameObject m_DropdownList;
    Dropdown m_Dropdown;
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
        foreach (KeyValuePair<string, Dictionary<string,object>> entry in data)
        {
            m_Dropdown.options.Add(new Dropdown.OptionData(entry.Key));
        }

    }

    // Update is called once per frame
    void Update()
    {

  
    }

    private void DropdownValueChangedHandler(Dropdown target)
    {
        Debug.Log("selected: " + target.captionText.text);
    }
}
