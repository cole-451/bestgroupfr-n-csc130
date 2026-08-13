using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    public float templateHeight = 70f;
    private List<HighScoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        highscoreEntryList = new List<HighScoreEntry>()
        {
            new HighScoreEntry{score = 517182, name = "AAA"},
            new HighScoreEntry{score = 5171690, name = "TAC"},
            new HighScoreEntry{score = 515182, name = "CC"},
            new HighScoreEntry{score = 527182, name = "CN"},
            new HighScoreEntry{score = 517152, name = "ASS"},
            new HighScoreEntry{score = 517382, name = "FCK"},
            new HighScoreEntry{score = 617182, name = "FN"},
            new HighScoreEntry{score = 517122, name = "RAH"}
        };

        //for now this is a good base. we can add the saving and loading later
        //https://www.youtube.com/watch?v=iAbaqGYdnyI

        //sort entry list by score
        for (int i = 0; i < highscoreEntryList.Count; i++)
        {
            for (int j = i +1; j < highscoreEntryList.Count; j++)
            {
                if (highscoreEntryList[j].score > highscoreEntryList[i].score)
                {
                    //swap
                    HighScoreEntry tmp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tmp;
                }

            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach(HighScoreEntry highscoreEntry in highscoreEntryList)
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
        int score = highscoreEntry.score;
        entryTransform.Find("InitialsText").GetComponent<TMP_Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("ScoreText").GetComponent<TMP_Text>().text = name;

        transformList.Add(entryTransform);
    }
    /*
     * represents a single high score entry
     */
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
}
