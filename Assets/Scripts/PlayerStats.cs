using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    WaveSpawner waves;

    public static int Money;
    public int startMoney = 250;

    public static int Lives;
    public int startLives = 10;

    public static int Score;
    public int startScore;
     
    public static int Energy;
    public int startEnergy = 100;

    public static int Rounds;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;
        PlayerPrefs.GetInt("Score"); 
        Energy = startEnergy;
        Rounds = 0;        
    }

    void MoneyCheat()
    {
        Money += 100;
    }

    void BigMoneyCheat()
    {
        Money += 10000000;
    }

    void LivesCheat()
    {
        Lives += 1;
    }

    void BigLivesCheat()
    {
        Lives += 10;
    }

    void DamageCheat()
    {
        Lives -= 1;
    }

    void EnergyCheat()
    {
        Energy += 25;
    }

    void LessEnergyCheat()
    {
        Energy -= 25;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            MoneyCheat();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            BigMoneyCheat();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LivesCheat();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            BigLivesCheat();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            DamageCheat();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            EnergyCheat();
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            LessEnergyCheat();
        }
    }
}
