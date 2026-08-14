using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    public float templateHeight = 70f;
    private List<Transform> highscoreEntryTransformList;
    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        //AddHighScoreEntry(10000, "TAC");

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        //sort entry list by score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i +1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    //swap
                    HighScoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }

            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach(HighScoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighScoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
            Debug.Log("Entry Entered");
        }
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1st"; break;
            case 2: rankString = "2nd"; break;
            case 3: rankString = "3rd"; break;
        }

        entryTransform.Find("RankText").GetComponent<TMP_Text>().text = rankString;

        //TODO: make score set to score gained from gameplay
        string name = highscoreEntry.initials;
        entryTransform.Find("InitialsText").GetComponent<TMP_Text>().text = name;

        int score = highscoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<TMP_Text>().text = score.ToString();

        transformList.Add(entryTransform);
    }

    private void AddHighScoreEntry(int score, string initials)
    {
        //Create HighscoreEntry
        HighScoreEntry highscoreEntry = new HighScoreEntry { score = score, initials = initials };
        
        //Load Saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        //Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighScoreEntry> highscoreEntryList;
    }
    /*
     * represents a single high score entry
     */
    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string initials;
    }
}
