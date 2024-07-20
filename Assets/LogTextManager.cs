using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogTextManager : MonoBehaviour
{
    public TMP_Text[] textLines;

    public List<string> gameplayHints;
    public List<string> tier2Hints;
    public List<string> tier3Hints;

    public static LogTextManager instance;
    private void Awake()
    {
        instance = this;
        ClearLog();
    }


    public void ClearLog()
    {
        foreach (TMP_Text textLine in textLines)
        {
            textLine.text = "";
        }
    }

    public void ShowTierHint(int tierLevel)
    {
        if (tierLevel == 2)
        {
            AddHintToLog(tier2Hints);
        }
        else if (tierLevel == 3)
        {
            AddHintToLog(tier3Hints);
        }
    }

    public void ShowGameplayHint(int listedHint = -1)
    {
        if (listedHint == -1)
        {
            AddHintToLog(gameplayHints);
        }
        else
        {
            AddSpecificHintToLog(gameplayHints[listedHint]);
        }
    }

    private void AddHintToLog(List<string> hints)
    {
        if (hints.Count > 0)
        {
            string randomHint = hints[Random.Range(0, hints.Count)];
            AddSpecificHintToLog(randomHint);
        }
    }

    public void AddSpecificHintToLog(string hint)
    {
        // Shift all lines up
        for (int i = 0; i < textLines.Length - 1; i++)
        {
            textLines[i].text = textLines[i + 1].text;
        }
        // Add new hint to the last line
        textLines[textLines.Length - 1].text = hint;
    }

}
