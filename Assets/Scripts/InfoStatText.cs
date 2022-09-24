using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoStatText : MonoBehaviour {

    [SerializeField]
    private Text nameOfGear, mainStat;
    [SerializeField]
    private Text[] subStat;
    [SerializeField]
    private GameObject sprite, sprite1;
    [SerializeField]
    private Borders borders;

    private GameObject socket;
    

    private Gear gearForPanel, gearForInventary;
    private bool isAdditional;
    public Text t_b_Plus;

    public Text t_b_SwapOrSell;

    private bool indexTypePanel;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private POIC_Controller poicController;


    void Awake()
    {
        //Clean();
    }

    public void SetLock()
    {
        gearForPanel.isLocked = !gearForPanel.isLocked;
    }

    public void Creating(Gear gear, int index, Gear gearInventary)
    {
        //Clean();
        if (index == 0)
        {
            t_b_SwapOrSell.text = "Sell";
        }
        else if (index == 1)
        {
            t_b_SwapOrSell.text = "Swap";
        }
        else
        {
            t_b_SwapOrSell.text = "Unequip";
        }

        sprite.GetComponent<Image>().sprite = borders.border[gear.rarity];
        sprite1.SetActive(true);
        sprite1.GetComponent<Image>().sprite = gear.sprite;
        

        gearForPanel = gear;
        gearForInventary = gearInventary;

        CreateInfoStat();
        CreateAddStat();
        
    }

    public void Creating(TypesOfItem.TypeGear typeGear, GameObject socketing, Gear gearOfP1)
    {
        Clean();
        t_b_SwapOrSell.text = "Equip";
        sprite.GetComponent<Image>().sprite = borders.spritesSockets[(int)typeGear];
        socket = socketing;
        sprite1.SetActive(false);
        //sprite1.GetComponent<Image>().sprite = null;
        gearForPanel = gearOfP1;

        CreateAddStat();

    }

    public void Clean()
    {
        //nameOfGear.text = "";
        mainStat.text = "";
        for (int i = 0; i < subStat.Length; i++) { subStat[i].text = ""; }
    }

    public void CreateInfoStat()
    {
        Clean();
        //nameOfGear.text = gear.type + " lv. " + gear.level;
        mainStat.text = CreateText(gearForPanel.mainStat, gearForPanel.mainValue, gearForPanel.element);
        for (int i = 0; i < gearForPanel.rarity; i++) { subStat[i].text = CreateText(gearForPanel.subStat[i], gearForPanel.subValue[i], gearForPanel.elements[i]); }
        
    }

    public float[] stats;
    private List<float> actualStats;
    private List<TypesOfItem.Type> actualTypes;
    private List<TypesOfItem.Element> actualElements;

    public void CreateAddStat()
    {
        stats = new float[9];

        stats[IndexStat(gearForPanel.mainStat, gearForPanel.element)] = gearForPanel.mainValue;
        for (int i = 0; i < gearForPanel.subStat.Length; i++)
        {
            stats[IndexStat(gearForPanel.subStat[i], gearForPanel.elements[i])] += gearForPanel.subValue[i];
        }

        actualStats = new List<float>();
        actualTypes = new List<TypesOfItem.Type>();
        actualElements = new List<TypesOfItem.Element>();

        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i] != 0 && i != (int)gearForPanel.mainStat)
            {
                actualStats.Add(stats[i]);
                actualTypes.Add((TypesOfItem.Type)(i < 5 ? i : 5));
                if (i > 5) { actualElements.Add((TypesOfItem.Element)(i - 5)); }
                else { actualElements.Add(TypesOfItem.Element.Fire); }

            }
        }
    }

    public float[] GetStats()
    {
        return stats;
    }

    private string CreateText(TypesOfItem.Type typeStat, float value, TypesOfItem.Element element)
    {
        switch (typeStat)
        {
            case TypesOfItem.Type.Damage:
                return "Damage: +" + value;
            case TypesOfItem.Type.CD:
                return "Cool Down: " + value + "s.";
            case TypesOfItem.Type.Mana:
                return "Mana: +" + value;
            case TypesOfItem.Type.ManaRegen:
                return "Mana Regen: +" + value;
            case TypesOfItem.Type.Effection:
                return "Effection: +" + value + "%";
            case TypesOfItem.Type.ElementalEff:
                return "El.(" + element + ") Effection: +" + value + "%";
            default:
                return typeStat + " + " + value;
        }
    }

    public void ButtonAdditional()
    {
        if (!isAdditional)
        {
            CreateAddText();
            t_b_Plus.text = "-";

        }
        else
        {
            CreateInfoStat();
            t_b_Plus.text = "+";
        }

        isAdditional = !isAdditional;
    }

    

    public void CreateAddText()
    {

        Clean();

        mainStat.text = CreateText(gearForPanel.mainStat, stats[(int)gearForPanel.mainStat], gearForPanel.element);

        for (int i = 0; i < actualTypes.Count; i++)
        {
            //int index = IndexStat(gearForPanel.subStat[i], gearForPanel.elements[i]);
            subStat[i].text = CreateText(actualTypes[i], actualStats[i], actualElements[i]);
        }

    }

    private int IndexStat(TypesOfItem.Type typeStat, TypesOfItem.Element element)
    {
        return typeStat != TypesOfItem.Type.ElementalEff ? (int)typeStat : (int)typeStat + (int)element;
    }

    public void ButtonEventMain()
    {
        if (t_b_SwapOrSell.text == "Sell")
        {
            gameController.RemoveGear();
            
        }
        else if (t_b_SwapOrSell.text == "Swap")
        {
            gameController.SwapGear(gearForPanel, gearForInventary, poicController.ip_1.stats);
        }
        else if (t_b_SwapOrSell.text == "Equip")
        {
            Debug.Log(socket.GetComponent<socketFix>().index);
            gameController.RemoveGear(gearForPanel, socket.GetComponent<socketFix>(), stats);
        }
        else
        {
            gameController.AddGear(gearForPanel, gearForPanel.GetComponentInParent<socketFix>().index);
        }

        poicController.MeUnenable();
    }


    void OnMouseDown()
    {
        
    }

    public void CalculationDifference(float[] otherStats)
    {
        float[] difference = new float[9];
        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i] != 0)
            {
                difference[i] = stats[i] - otherStats[i];
            }
        }

        
        mainStat.text += AddText(difference[IndexStat(gearForPanel.mainStat, gearForPanel.element)]);

        for (int i = 0; i < actualTypes.Count; i++)
        {
            subStat[i].text += AddText(difference[IndexStat(actualTypes[i], actualElements[i])]);
        }

    }

    private string AddText(float x)
    {
        if (x > 0)
        {
            return " <color=#00ff00ff>(+" + x.ToString() + ")</color>";
        }
        else if (x < 0)
        {
            return " <color=#ff0000ff>(" + x.ToString() + ")</color>";
        }
        else
        {
            return "";
        }
    }

}
