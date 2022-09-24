using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat", menuName = "Stat", order = 51)]
public class Stats : ScriptableObject {

    // 0 - Damage, Mana, ManaRegen
    // 1 - CD, Effection
    // 2 - Elemental
    public int whatIsOption; 

    public float minDifference, maxDifference;

    public float[] statValue;
    public float[,] statValueResult, statSubValueResult;

    public TypesOfItem.Type type;
    public TypesOfItem.Element element;

    void Awake()
    {
        CalculateValues();
    }

    public void CalculateValues()
    {
        statValueResult = new float[statValue.Length, 2];

        if (whatIsOption == 0)
        {
            for (int i = 0; i < statValue.Length; i++)
            {
                statValueResult[i, 0] = (float)Math.Round(statValue[i] * minDifference, 2);
                statValueResult[i, 1] = (float)Math.Round(statValue[i] * maxDifference, 2);
            }
        }
        else
        {
            for (int i = 0; i < statValue.Length; i++)
            {
                statValueResult[i, 0] = (float)Math.Round(statValue[i] - minDifference, 2);
                statValueResult[i, 1] = (float)Math.Round(statValue[i] + minDifference, 2);
            }
        }

        CalculateSubValue();
    }

    public void CalculateSubValue()
    {
        statSubValueResult = new float[statValue.Length, 2];

        if (type == TypesOfItem.Type.CD)
        {
            return;
        }

        float disc;

        switch (type)
        {
            case TypesOfItem.Type.Damage:
            case TypesOfItem.Type.Mana:
            case TypesOfItem.Type.ManaRegen:
                disc = 2;
                break;
            case TypesOfItem.Type.Effection:
            case TypesOfItem.Type.ElementalEff:
                disc = 8;
                break;
            default:
                disc = 1;
                break;               
        }



        for (int i = 0; i < statValue.Length; i++)
        {
            statSubValueResult[i, 0] = (float)Math.Round(statValueResult[i - Disc(i), 0] / disc, 2);
            statSubValueResult[i, 1] = (float)Math.Round(statValueResult[i, 1] / disc, 2);
        }
    }

    public float GetStat(int level, bool isMain)
    {
        float result;
        CalculateValues();

        if (isMain)
        {
            result = UnityEngine.Random.Range(statValueResult[level, 0], statValueResult[level, 1]);
        }
        else
        {
            result = UnityEngine.Random.Range(statSubValueResult[level, 0], statSubValueResult[level, 1]);
        }

        result = (float)Math.Round(result, 2);

        return result;


    }

    int Disc(int level)
    {
        switch (level)
        {
            case 0: return 0;
            case 1: return 1;
            case 2: return 2;
            default: return 3;
        }
    }

    public TypesOfItem.Element TakeElement()
    {
        return (TypesOfItem.Element)UnityEngine.Random.Range(0, 4);
    }

}

