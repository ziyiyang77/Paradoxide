using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class CSVReader
{
    public static List<Dictionary<string, object>> Read(string file)
    {
        var result = new List<Dictionary<string, object>>();
        var csvFile = Resources.Load<TextAsset>(file);
        var lines = csvFile.text.Split('\n');

        if (lines.Length <= 1) return result;

        var headers = lines[0].Split(',');

        for (var i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i])) continue;

            var values = lines[i].Split(',');
            var entry = new Dictionary<string, object>();

            for (var j = 0; j < headers.Length; j++)
            {
                entry[headers[j]] = values[j];
            }

            result.Add(entry);
        }


        return result;
    }
}
