using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllStatText : MonoBehaviour {

    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private Text[] texts;

    public void CreateAllStatText()
    {
        texts[0].text = "Damage: " + Calc(0).ToString();
        texts[1].text = "Mana: " + Calc(1).ToString();
        texts[2].text = "Mana Regen: " + Calc(2).ToString();
        texts[3].text = "Cool Down: " + Calc(3).ToString() + "s.";
        texts[4].text = "Effection: " + Calc(4).ToString() + "%";
        texts[5].text = "Ell.(Fire): " + Calc(5).ToString() + "%";
        texts[6].text = "Ell.(Ice): " + Calc(6).ToString() + "%";
        texts[7].text = "Ell.(Earth): " + Calc(7).ToString() + "%";
        texts[8].text = "Ell.(Lighting): " + Calc(8).ToString() + "%";


    } 

    private float Calc(int i)
    {
        float sum = 0;
        for (int j = 0; j < 6; j++) { sum += gameController.stats[i, j]; }
        return sum;
    }


}
