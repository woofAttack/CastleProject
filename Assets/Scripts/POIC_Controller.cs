using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class POIC_Controller : MonoBehaviour
{
    //public bool isCanClose;
    public GameObject p_1, p_2, p_3;
    public InfoStatText ip_1, ip_2, ip_3;
    public GameController gameController;
    private bool f_1, f_2, f_3, f_plus;

    public Text t_b_Plus;

    [SerializeField]
    private GameObject[] sockets;

    public void MeEnable()
    {
        gameObject.SetActive(true);
    }

    public void MeUnenable()
    {
        p_1.SetActive(false);
        f_1 = false;
        p_2.SetActive(false);
        f_2 = false;
        p_3.SetActive(false);
        f_3 = false;
        f_plus = false;
        t_b_Plus.text = "+";

        gameObject.SetActive(false);
    }

	public void OnMouseUp()
    {
        //if (isCanClose)
        MeUnenable();
        //isCanClose = true;
    }

    public void CreatingPanels1(Gear gear)
    {

        MeEnable();

        p_1.SetActive(true);
        p_1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        ip_1.Creating(gear, 2, gear);
        f_1 = true;


    }

    public void CreatingPanels2(Gear gear)
    {

        MeEnable();

        p_1.SetActive(true);
        p_1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -276);
        ip_1.Creating(gear, 0, gear);
        f_1 = true;

        p_2.SetActive(true);
        p_2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 276);

        Gear panelGear = gameController.gearsInPanel[(int)gear.type];
                      
        

        if (panelGear != null)
        {
            ip_2.Creating(panelGear, 1, gear);
            f_2 = true;
        }
        else
        {
            ip_2.Creating(gear.type, sockets[(int)gear.type], gear);
        }

    }

    public void CreatingPanels3(Gear gear)
    {

        MeEnable();

        p_1.SetActive(true);
        p_1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -552);
        ip_1.Creating(gear, 0, gear);
        f_1 = true;


        p_2.SetActive(true);
        p_2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        Gear panelGear1 = gameController.gearsInPanel[5];

        if (panelGear1 != null)
        {
            ip_2.Creating(panelGear1, 1, gear);
            f_2 = true;
        }
        else
        {
            ip_2.Creating(gear.type, sockets[5], gear);
        }


        p_3.SetActive(true);
        p_3.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 552);
        Gear panelGear2 = gameController.gearsInPanel[4];

        if (panelGear2 != null)
        {
            ip_3.Creating(panelGear2, 1, gear);
            f_3 = true;
        }
        else
        {
            ip_3.Creating(gear.type, sockets[4], gear);
        }

    }


    public void ButtonAdditional()
    {
        if (!f_plus)
        {
            if (f_1)
            {
                ip_1.CreateAddText();
            }
            if (f_2)
            {
                ip_2.CreateAddText();
                ip_2.CalculationDifference(ip_1.stats);
            }
            if (f_3)
            {
                ip_3.CreateAddText();
                ip_3.CalculationDifference(ip_1.stats);
            }
            f_plus = true;
            t_b_Plus.text = "-";
        }
        else
        {
            if (f_1)
            {
                ip_1.CreateInfoStat();
            }
            if (f_2)
            {
                ip_2.CreateInfoStat();
            }
            if (f_3)
            {
                ip_3.CreateInfoStat();
            }
            f_plus = false;
            t_b_Plus.text = "+";
        }
    }

}
