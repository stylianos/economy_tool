using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSVReader_Dictionary_Demo : MonoBehaviour
{

    void Awake()
    {

        Dictionary<string, Dictionary<string, object>> data = CSVReader_Dictionary.Read("page_1");
        Dictionary<string, Dictionary<string, object>> data_2 = CSVReader_Dictionary.Read("page_2");
        for (var i = 0; i < data.Count; i++)
        {
            //print("name " + data[i]["name"] + " " +
            //       "age " + data[i]["age"] + " " +
            //       "speed " + data[i]["speed"] + " " +
            //       "desc " + data[i]["description"]);
       }

    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
