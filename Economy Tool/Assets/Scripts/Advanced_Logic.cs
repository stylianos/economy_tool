using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Advanced_Logic : MonoBehaviour
{
    public GameObject       m_DropdownList;
    public GameObject       m_InputPair;
    public RectTransform    m_Content;

    string      m_ActiveOption;
    Dropdown    m_Dropdown;
    Dictionary<string, Dictionary<string, object>> data, data_2;
    Dictionary<string, GameObject> m_NameDictionary, m_InputDictionary;

    void Awake()
    {

        data = CSVReader_Dictionary.Read("page_1");
        data_2 = CSVReader_Dictionary.Read("page_2");
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
        foreach (KeyValuePair<string, Dictionary<string,object>> entry in data_2)
        {
            m_Dropdown.options.Add(new Dropdown.OptionData(entry.Key));
            GameObject new_Element = Object.Instantiate(m_InputPair, m_Content);
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
        object result;
  
    }

    public void UpdateValues()
    {
        Debug.Log("I updated the values");
    }
}
