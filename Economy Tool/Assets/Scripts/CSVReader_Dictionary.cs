﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader_Dictionary : MonoBehaviour
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static Dictionary<string,Dictionary<string, object>> Read(string file)
    {
        var list = new Dictionary<string, Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 1; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(values[0],entry);
        }
        return list;
    }

    public static Dictionary<string, Dictionary<string, int>> ReadString(string file)
    {
        var list = new Dictionary<string, Dictionary<string, int>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, int>();
            for (var j = 1; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                int finalvalue = 0;
                int n;
                //float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                /*
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }*/
                entry[header[j]] = finalvalue;
            }
            list.Add(values[0], entry);
        }
        return list;
    }

    public static SortedDictionary<string, Dictionary<string, int>> ReadStringSorted(string file)
    {
        var list = new SortedDictionary<string, Dictionary<string, int>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, int>();
            for (var j = 1; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                int finalvalue = 0;
                int n;
                //float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                /*
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }*/
                entry[header[j]] = finalvalue;
            }
            list.Add(values[0], entry);
        }
        return list;
    }
}