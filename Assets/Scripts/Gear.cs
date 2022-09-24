using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;



public class Gear : MonoBehaviour
{

    public int level;
    public int rarity;
    public TypesOfItem.TypeGear type;

    public TypesOfItem.Type mainStat;
    public float mainValue;
    public TypesOfItem.Element element;

    public TypesOfItem.Type[] subStat;
    public float[] subValue;
    public TypesOfItem.Element[] elements;

    public Sprite sprite;

    public bool isEquiped = false;
    public int index;

    public bool isLocked;

    
    public Gear(TypeGear typeGear)
    {
        CreateGear(typeGear);
    }

    public Gear(Gear gear)
    {
        CreateGear(gear);
    }

    public int PrefixLevel(int choose)
    {
        float[] mass;
        int desc;

        switch (choose)
        {
            case 1:
                mass = new float[3] { 8500, 1000, 500 };
                desc = 0;
                break;
            case 2:
                mass = new float[4] { 3500, 5000, 1000, 500 };
                desc = 1;
                break;
            case 3:
                mass = new float[5] { 1250, 2750, 4500, 1000, 500 };
                desc = 2;
                break;
            case 4:
                mass = new float[4] { 1250, 2750, 5500, 500 };
                desc = 2;
                break;
            case 5:
                mass = new float[3] { 1250, 2750, 6000 };
                desc = 2;
                break;
            default:
                mass = new float[0];
                desc = 2;
                break;
        }

        float c1, c2;
        float rand = Random.Range(0, 10000);
        c1 = 0;
        c2 = 0;

        for (int i = 0; i < mass.Length; i++)
        {
            c1 += mass[i];
            if (rand >= c2 && rand <= c1) return i - desc;
            c2 = c1;
        }

        return 0;
    }

    public int GetLevel(int lev)
    {

        switch (lev)
        {
            case 1: return lev + PrefixLevel(1);
            case 2: return lev + PrefixLevel(2);
            case 29: return lev + PrefixLevel(4);
            case 30: return lev + PrefixLevel(5);
            default: return lev + PrefixLevel(3);
        }


    }



    public void CreateGear(TypeGear typeGear)
    {

        level = GetLevel(3);
        rarity = GetLevel(3) - 1;
        type = typeGear.nameOfType;


        subStat = new TypesOfItem.Type[rarity];
        subValue = new float[rarity];
        elements = new TypesOfItem.Element[rarity];

        mainStat = typeGear.main.type;
        mainValue = typeGear.TakeMain(level);
        element = typeGear.main.element;

        for (int i = 0; i < rarity; i++)
        {
            int rand = Random.Range(0, typeGear.sub.Count);
            
            subStat[i] = typeGear.sub[rand].type;
            subValue[i] = typeGear.TakeSub(level, rand);
            elements[i] = typeGear.sub[rand].element;

        }

        sprite = typeGear.sprites[level - 1];


    }

    public void CreateGear(Gear other)
    {
        level = other.level;
        rarity = other.rarity;
        type = other.type;
        mainStat = other.mainStat;
        mainValue = other.mainValue;
        subStat = other.subStat;
        subValue = other.subValue;
        sprite = other.sprite;
        index = other.index;
        elements = other.elements;
        isLocked = other.isLocked;
        isEquiped = other.isEquiped;
    }



    



}
