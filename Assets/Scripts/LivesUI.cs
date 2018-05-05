using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Text livesText;
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerStats.Lives == 1)
        {
            livesText.text = PlayerStats.Lives.ToString() + " LIFE";
        }
        else
        {
            livesText.text = PlayerStats.Lives.ToString() + " LIVES";
        }
        
	}
}
