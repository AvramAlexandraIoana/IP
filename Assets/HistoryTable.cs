using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.IO;


public class HistoryTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;
    [SerializeField] private History _History;


    private void Awake()
    {
        entryContainer = transform.Find("HistoryEntryContainer");
        entryTemplate = entryContainer.Find("HistoryEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        //AddHighScoreEntry(223459, "asd");
        /*
        Debug.Log(Application.persistentDataPath);
        Debug.Log(System.IO.File.Exists(Application.persistentDataPath + "/TableData.json"));*/
        if (System.IO.File.Exists(Application.persistentDataPath + "/TableData.json"))
        {

            string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + "/TableData.json");
            History history = JsonUtility.FromJson<History>(jsonString);

            Debug.Log("Ioana");

            Debug.Log(history);
            for (int i = 0; i < history.highScoresEntryList.Count; i++)
            {
                for (int j = i + 1; j < history.highScoresEntryList.Count; j++)
                {
                    if (history.highScoresEntryList[j].score > history.highScoresEntryList[i].score)
                    {
                        HistoryEntry temp = history.highScoresEntryList[i];
                        history.highScoresEntryList[i] = history.highScoresEntryList[j];
                        history.highScoresEntryList[j] = temp;
                    }
                }

            }
            highScoreEntryTransformList = new List<Transform>();
            var index = 0;
            foreach (HistoryEntry historyEntry in history.highScoresEntryList)
            {
                index += 1;
                if (index <= 10)
                {
                    CreateHighScoresEntryTemplate(historyEntry, entryContainer, highScoreEntryTransformList);
                }
            }


        }



        /*HighScores highScores = new HighScores { highScoresEntryList = highScoresEntryList };
        _HighScores = new HighScores { highScoresEntryList = highScoresEntryList };
        
        string potion = JsonUtility.ToJson(_HighScores);
        Debug.Log(potion);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/TableData.json", potion);
        var elem = System.IO.File.ReadAllText(Application.persistentDataPath + "/TableData.json");
        var exista = System.IO.File.Exists(Application.persistentDataPath + "/TableData.json");
        Debug.Log(elem);
        Debug.Log(exista);



       
        string json = JsonUtility.ToJson(highScores);

        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highScoreTable"));*/




    }
   

    private void CreateHighScoresEntryTemplate(HistoryEntry historyEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
      

        int score = historyEntry.score;
        entryTransform.Find("dateText").GetComponent<Text>().text = score.ToString();

        //string name = highScoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = "Player" + rank;

        //Set background visible odds and evens
        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);

       
        transformList.Add(entryTransform);
    }
    /*

    private void AddHighScoreEntry(int score, string name)
    {
        //Create HighScoreEntry
        HistoryEntry historyEntry = new HistoryEntry { score = score, name = name };

        //Load saved HighScores
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + "/TableData.json");
        History highScores = JsonUtility.FromJson<History>(jsonString);

        //Add new entry
        highScores.highScoresEntryList.Add(historyEntry);


        _History = new History { highScoresEntryList = highScores.highScoresEntryList };

        string list = JsonUtility.ToJson(_History);
        Debug.Log(list);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/TableData.json", list);

    }*/

    [System.Serializable]
    private class History
    {
        public List<HistoryEntry> highScoresEntryList;
    }

    [System.Serializable]
    private class HistoryEntry
    {
        public int score;
        public string name;
    }
}
