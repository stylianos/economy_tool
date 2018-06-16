using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Advanced_Logic : MonoBehaviour
{
    public GameObject       m_DropdownList;
    public GameObject       m_InputPair;
    public RectTransform    m_Content;

    string      m_ActiveOption;
    Dropdown    m_Dropdown;
    Dictionary<string, Dictionary<string, int>> data, data_2;
    Dictionary<string, GameObject> m_NameDictionary, m_InputDictionary;

    void Awake()
    {

        data = CSVReader_Dictionary.ReadString("page_1");
        data_2 = CSVReader_Dictionary.ReadString("page_2");
        m_NameDictionary = new Dictionary<string, GameObject>();
        m_InputDictionary = new Dictionary<string, GameObject>();
    }

    // Use this for initialization
    void Start()
    {
        m_Dropdown      = m_DropdownList.GetComponent<Dropdown>();

        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChangedHandler(m_Dropdown);
        });
        foreach (KeyValuePair<string, Dictionary<string,int>> entry in data_2)
        {
            m_Dropdown.options.Add(new Dropdown.OptionData(entry.Key));
            GameObject new_Element = GameObject.Instantiate(m_InputPair, m_Content);
            //Get the Children of each new input field to appoint them to the appropriate dictionaries. 
            foreach (Transform child in new_Element.transform)
            {
                if ( child.name == "Name")
                {
                    m_NameDictionary.Add(entry.Key, child.gameObject);
                    child.GetComponent<Text>().text = entry.Key;
                }
                if (child.name == "Input")
                {
                    m_InputDictionary.Add(entry.Key, child.gameObject);
                    child.GetComponent<InputField>().text = "0";
                }
            }
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
        foreach (KeyValuePair<string, int> entry in data_2[m_ActiveOption])
        {
            //Get one by one the appropriate input fields and update their values
            m_InputDictionary[entry.Key].GetComponent<InputField>().text = entry.Value.ToString();
        }
    }

    public void UpdateValues()
    {
        //You cannot use a for each loop in order to change values, I am putting every key in a list and iterate that list instead. 
        List<string> keyList = new List<string>(data_2[m_ActiveOption].Keys);
        for (var i = 0; i < keyList.Count; i++)
        {
            data_2[m_ActiveOption][keyList[i]] = Int32.Parse(m_InputDictionary[keyList[i]].GetComponent<InputField>().text);
        }
        Debug.Log("I updated the values");
    }
}
