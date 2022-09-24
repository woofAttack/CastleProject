using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player", order = 54)]

public class PlayerStat : ScriptableObject {

    public float damage;
    public float mana;
    public float mana_regen;
    public float CD;

    public GameObject[] e_sockets;
    public GameObject[] e_runes;

    
    public List<Gear> gears;

    public void Starts()
    {
        GameObject panel = GameObject.Find("w2");
        e_sockets = new GameObject[panel.transform.childCount];
        for (int i = 0; i < e_sockets.Length; i++)
        {
            e_sockets[i] = panel.transform.GetChild(i).gameObject;
        }
    }

    private void ZeroStat()
    {

        damage = 0;
        mana = 0;
        mana_regen = 0;
        CD = 0;

    }

    /*public void GetESockets()
    {
        ZeroStat();

        for (int i = 0; i < e_sockets.Length; i++)
        {
            if (e_sockets[i].transform.childCount > 0)
            {
                Gear _tokeGear = e_sockets[i].transform.GetChild(0).GetComponent<Gear>();
                damage += GetStat("Damage", _tokeGear);
                mana += GetStat("Mana", _tokeGear);
                mana_regen += GetStat("Mana Regen", _tokeGear);
                CD += GetStat("CD", _tokeGear);
            }
        }

    }

    private float GetStat(string nameOfStat, Gear gear)
    {
        float stat = 0;

        if (gear.mainStat == nameOfStat) { stat += gear.mainValue; }

        for (int i = 0; i < gear.subStat.Length; i++)
        {
            if (gear.subStat[i] == nameOfStat) { stat += gear.subValue[i]; }
        }


        return stat;
    }*/

}
