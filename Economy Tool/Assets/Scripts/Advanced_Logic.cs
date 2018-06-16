using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Advanced_Logic : MonoBehaviour
{
    public GameObject       m_DropdownList;
    public GameObject       m_DropdownLabel;
    public GameObject       m_InputPair;
    public RectTransform    m_Content;
    public Text m_Output;

    private bool m_OptionSet;
    string      m_ActiveOption;
    Dropdown    m_Dropdown;
    SortedDictionary<string, Dictionary<string, int>> data, data_2;
    Dictionary<string, GameObject> m_NameDictionary, m_InputDictionary;

    void Awake()
    {

        data = CSVReader_Dictionary.ReadStringSorted("page_1");
        data_2 = CSVReader_Dictionary.ReadStringSorted("page_2");
        m_NameDictionary = new Dictionary<string, GameObject>();
        m_InputDictionary = new Dictionary<string, GameObject>();
        m_OptionSet = false; 
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
            //Set the first value
            if (!m_OptionSet)
            {
                m_DropdownLabel.GetComponent<Text>().text = entry.Key;
                m_OptionSet = true;
                m_ActiveOption = entry.Key; 
            }
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
        m_Output.color = Color.white;
        m_Output.text = "I updated the values";
    }

    public void CheckForLocks()
    {
        m_Output.text = "";
        m_Output.color = Color.yellow;
        foreach (KeyValuePair<string, Dictionary<string, int>> entry in data_2)
        {
            if ( data_2[entry.Key][entry.Key] != 0 )
            {
                m_Output.text = m_Output.text + " DeadLock detected !! " + entry.Key + " requires itself " + "\n";
            
            }
        }
    }
}
