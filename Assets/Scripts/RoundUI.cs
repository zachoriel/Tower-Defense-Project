using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundUI : MonoBehaviour
{
    public Text roundText;

    // Update is called once per frame
    void Update()
    {
        roundText.text = "Wave " + PlayerStats.Rounds.ToString();
    }
}
