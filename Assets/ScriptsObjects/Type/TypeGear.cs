using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gear", menuName = "TypeGear", order = 52)]
public class TypeGear : ScriptableObject {

    public TypesOfItem.TypeGear nameOfType;
    public Stats main;
    public List<Stats> sub;
    public List<Sprite> sprites;

    public List<float> chances;
    public float sumChances;

   public float TakeMain(int lev)
    {

        return main.GetStat(lev, true);

    }

   public float TakeSub(int lev, int index)
    {

        return sub[index].GetStat(lev, false);

    }

    public void FixList()
    {
        for (int i = sub.Count; i < chances.Count; i++)
        {
            chances.RemoveAt(i);
        }
    }

    public void SummFix()
    {
        sumChances = 0;
        foreach (float item in chances)
        {
            sumChances += item;
        }


    }

}
