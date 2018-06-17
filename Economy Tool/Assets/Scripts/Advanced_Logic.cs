using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Advanced_Logic : MonoBehaviour
{
    public GameObject       m_DropdownList;
    public GameObject       m_DropdownListMinutes;
    public GameObject       m_DropdownListSessions;
    public GameObject       m_DropdownLabel;
    public GameObject       m_InputPair;
    public RectTransform    m_Content;
    public Text             m_Output;
    public Text             m_LevelRequired;
    public Text             m_TimeToBuild;
    public Text             m_TimeToBuildDays;
    public Text             m_NumberofSessions;
    GameObject              m_Reader;

    private bool m_OptionSet;
    string      m_ActiveOption;
    Dropdown    m_Dropdown;
    Dropdown    m_Minutes;
    Dropdown    m_SessionsPerDay;
    SortedDictionary<string, Dictionary<string, int>> data, data_2;
    Dictionary<string, GameObject> m_NameDictionary, m_InputDictionary;

    void Awake()
    {

        m_NameDictionary = new Dictionary<string, GameObject>();
        m_InputDictionary = new Dictionary<string, GameObject>();
        m_OptionSet = false; 
        
    }

    // Use this for initialization
    void Start()
    {
        m_Reader = GameObject.FindWithTag("Reader");
        //Get the appropriate data
        data    = m_Reader.GetComponent<Reader>().data;
        data_2  = m_Reader.GetComponent<Reader>().data2;
        m_Dropdown          = m_DropdownList.GetComponent<Dropdown>();
        m_Minutes           = m_DropdownListMinutes.GetComponent<Dropdown>();
        m_SessionsPerDay    = m_DropdownListSessions.GetComponent<Dropdown>();
        //Adding the appropriate delegates for the dropdown events
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChangedHandler(m_Dropdown);
        });
        m_SessionsPerDay.onValueChanged.AddListener(delegate {
            CalculateTimeToBuild();
        });
        m_Minutes.onValueChanged.AddListener(delegate {
            CalculateTimeToBuild();
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
        CalculateTimeToBuild();
    }

    public void UpdateValues()
    {
        //You cannot use a for each loop in order to change values, I am putting every key in a list and iterate that list instead. 
        List<string> keyList = new List<string>(data_2[m_ActiveOption].Keys);
        for (var i = 0; i < keyList.Count; i++)
        {
            try
            {
                data_2[m_ActiveOption][keyList[i]] = Int32.Parse(m_InputDictionary[keyList[i]].GetComponent<InputField>().text);
            }
            catch (FormatException)
            {
                m_Output.color = Color.yellow;
                m_Output.text = "Wrong format exception!! Mistake at  " + keyList[i];
                return;
            }
            catch (OverflowException)
            {
                m_Output.color = Color.yellow;
                m_Output.text = "Overflow exception!!" + data_2[m_ActiveOption][keyList[i]];
                return;
            }
            
        }
        Debug.Log("I updated the values");
        m_Output.color = Color.white;
        m_Output.text = "I updated the values";
        CalculateTimeToBuild();
    }

    public void CheckForLocks()
    {
        m_Output.text = "";
        m_Output.color = Color.yellow;
        foreach (KeyValuePair<string, Dictionary<string, int>> entry in data_2)
        {
            if ( data_2[entry.Key][entry.Key] != 0 )
            {
                m_Output.text = m_Output.text + " DeadLock detected! " + entry.Key + " requires itself " + "\n";
            
            }
            foreach (KeyValuePair<string, int> entry_specific in entry.Value)
            {
                if ( entry_specific.Value < 0)
                {
                    m_Output.text = m_Output.text + " Negative number detected!" + entry.Key + " requires " + entry_specific.Value + " of " + entry_specific.Key + "\n";
                }
            }
        }

    }

    public void CalculateTimeToBuild()
    {
        float time_to_build = 0 ;
        float level_required = 0;
        float days = 0;
        //grab everything with that key to find how many minutes it takes to build something. 
        foreach (KeyValuePair<string, int> entry in data_2[m_ActiveOption])
        {
            //Basically get the minutes required per resource and multiply by how many resources are needed 
            if ( entry.Value > 0)
            {
                time_to_build += data[entry.Key]["Minutes"] * entry.Value;
                if (data[entry.Key]["Level"] > level_required)
                {
                    level_required = data[entry.Key]["Level"];
                }
            }
           
        }

        //Add the time to build the item itself !!
        time_to_build += data[m_ActiveOption]["Minutes"];
        if ( data[m_ActiveOption]["Level"] > level_required )
        {
            level_required = data[m_ActiveOption]["Level"];
        }

        //Debug.Log("This is teh currrnet selection frot the minutes per sessions" + m_Minutes.captionText.text);
        //Debug.Log("This is the otehr one" + m_SessionsPerDay.captionText.text);
        m_TimeToBuild.text = time_to_build.ToString() + " of which " + data[m_ActiveOption]["Minutes"].ToString() + " for the item itself";
        m_LevelRequired.text = level_required.ToString();
        int minutes_per_sessions = Int32.Parse(m_Minutes.captionText.text);
        int sessions_per_day = Int32.Parse(m_SessionsPerDay.captionText.text);

        days = time_to_build / (minutes_per_sessions * sessions_per_day);
        m_TimeToBuildDays.text = days.ToString();
    }
}
