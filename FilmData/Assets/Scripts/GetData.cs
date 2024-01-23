using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class GetData : MonoBehaviour
{
    [SerializeField] string[] releaseDate;
    [SerializeField] string[] title;
    [SerializeField] string[] overview;
    [SerializeField] string[] popularity;
    [SerializeField] string[] voteCount;
    [SerializeField] string[] voteAverage;
    [SerializeField] string[] originalLanguage;
    [SerializeField] string[] genre;
    [SerializeField] string[] PosterUrl;

    [SerializeField] List<int> moviesToShow;
    [SerializeField] TMP_InputField inputField;
    [SerializeField]string[] diffenrentInfo;
    [SerializeField]string[] infoToCheck = new string[0];

    [SerializeField] Transform startPosition;
    [SerializeField] GameObject movieHolderPrefab;
    [SerializeField] int MaxMoviesToShow;
    [SerializeField] GameObject movieParent;
    [SerializeField] Slider slider;
    [SerializeField] float sliderMultiplier;

    [SerializeField] float plusOnX;
    [SerializeField] float plusOnY;

    List<GameObject> movieHolders = new List<GameObject>();
    

    private void Awake()
    {
        GetFileData();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            CheckTroughMovies();
        }
        takeCareOfSLider();
    }
    void takeCareOfSLider()
    {
        float sliderY = slider.value * sliderMultiplier;
        movieParent.transform.position = new Vector3(0, sliderY);
    }
    void GetFileData()
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

                if (data.Length > 1)
                {
                    if (d == 1 || d == 3)
                    {
                        info.Add(data[d]);
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

    
    void CheckTroughMovies()
    {
        bool haveSortedOnce = false;
        moviesToShow = new List<int>();
        string text = inputField.text;
        string[] splitText = text.Split(' ');
        

        foreach (string s in splitText)
        {
            if(s != "")
            {
                string[] splitSplitText = s.Split(':');

                int index = 20;
                for (int i = 0; i < diffenrentInfo.Length; i++)
                {
                    if (splitSplitText[0].Contains(diffenrentInfo[i]))
                    {
                        index = i;
                        if (index == 0)
                            infoToCheck = releaseDate;
                        if (index == 1)
                            infoToCheck = title;
                        if (index == 2)
                            infoToCheck = overview;
                        if (index == 3)
                            infoToCheck = popularity;
                        if (index == 4)
                            infoToCheck = voteCount;
                        if (index == 5)
                            infoToCheck = voteAverage;
                        if (index == 6)
                            infoToCheck = originalLanguage;
                        if (index == 7)
                            infoToCheck = genre;
                        if (index == 8)
                            infoToCheck = PosterUrl;
                        break;
                    }
                }
                if (!haveSortedOnce)
                {
                    for (int i = 1; i < infoToCheck.Length - 1; i++)
                    {
                        if (infoToCheck[i].Contains(splitSplitText[1]))
                        {
                            moviesToShow.Add(i);
                        }

                    }

                    haveSortedOnce = true;
                }
                else
                {
                    List<int> newlist = new List<int>();
                    for (int i = 0; i < moviesToShow.Count; i++)
                    {
                        if (infoToCheck[moviesToShow[i]].Contains(splitSplitText[1]))
                        {

                            newlist.Add(moviesToShow[i]);
                        }
                    }
                    moviesToShow = newlist;

                }
            }
            
        }
        placeMovies();

    }
    void placeMovies()
    {
        foreach(GameObject g in movieHolders)
        {
            Destroy(g);
        }
        slider.value = 0;
        takeCareOfSLider();

        movieHolders = new List<GameObject>();
        int numberOfMoviesToShow = moviesToShow.Count;
        if (moviesToShow.Count > MaxMoviesToShow)
            numberOfMoviesToShow = MaxMoviesToShow;

        float x = startPosition.position.x;
        float y = startPosition.position.y;

        for (int i = 0; i < numberOfMoviesToShow; i++)
        {
            GameObject g = Instantiate(movieHolderPrefab, new Vector2(x, y), Quaternion.identity);
            g.transform.parent = movieParent.transform;
            g.GetComponentInChildren<TextMeshProUGUI>().text = title[moviesToShow[i]];
            movieHolders.Add(g);

            x += plusOnX;
            if(x >= startPosition.position.x + (4 * plusOnX))
            {
                Debug.Log(x);
                x = startPosition.position.x;
                y -= plusOnY;
            }

        }
    }
}
