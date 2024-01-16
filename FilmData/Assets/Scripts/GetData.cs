using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GetData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tempText;

  //  public List<string[]> films = new List<string[]>();
    [SerializeField] string[] releaseDate;
    [SerializeField] string[] title;
    [SerializeField] string[] overview;
    [SerializeField] string[] popularity;
    [SerializeField] string[] voteCount;
    [SerializeField] string[] voteAverage;
    [SerializeField] string[] originalLanguage;
    [SerializeField] string[] genre;
    [SerializeField] string[] PosterUrl;

    private void Awake()
    {
        string fileData = File.ReadAllText(Application.streamingAssetsPath + "/movies.csv");
        string[] dataLines = fileData.Split('\n');
        releaseDate = new string[dataLines.Length];
        title = new string[dataLines.Length];
        overview = new string[dataLines.Length];
        popularity = new string[dataLines.Length];
        voteCount = new string[dataLines.Length];
        voteAverage = new string[dataLines.Length];
        originalLanguage = new string[dataLines.Length];
        genre = new string[dataLines.Length];
        PosterUrl = new string[dataLines.Length];

        for (int i = 1; i < dataLines.Length; i++)
        {
            string[] data = dataLines[i].Split('"');
            List<string> info = new List<string>();
            for (int d = 0; d < data.Length; d++)
            {
                
                if(data.Length > 1)
                {
                    if(d == 1 || d == 3)
                    {
                        info.Add(data[d]);
                    }
                    else
                    {
                        string[] infoSplit = data[d].Split(',');
                        foreach (string s in infoSplit)
                        {
                            if(s != "")
                            info.Add(s);
                        }
                    }
                }
                else
                {
                    string[] infoSplit = data[d].Split(',');
                    foreach (string s in infoSplit)
                    {
                        if (s != "")
                            info.Add(s);
                    }
                }
            }
            for (int k = 0; k < info.Count; k++)
            {
                // print(info[k]);
                if (k == 0)
                    releaseDate[i] = info[k];
                if (k == 1)
                    title[i] = info[k];
                if (k == 2)
                    overview[i] = info[k];
                if (k == 3)
                    popularity[i] = info[k];
                if (k == 4)
                    voteCount[i] = info[k];
                if (k == 5)
                    voteAverage[i] = info[k];
                if (k == 6)
                    originalLanguage[i] = info[k];
                if (k == 7)
                    genre[i] = info[k];
                if (k == 8)
                    PosterUrl[i] = info[k];
            }
        }
    }
}
