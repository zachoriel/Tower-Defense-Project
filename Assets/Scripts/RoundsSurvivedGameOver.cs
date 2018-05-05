using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvivedGameOver : MonoBehaviour
{
    public Text roundsText;

    void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSecondsRealtime(.7f);

        while (round < PlayerStats.Rounds - 1)
        {
            round++;
            roundsText.text = round.ToString();

            yield return new WaitForSecondsRealtime(.05f);
        }
    }
}
