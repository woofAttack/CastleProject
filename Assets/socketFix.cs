using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class socketFix : MonoBehaviour {

    public TypesOfItem.TypeGear nameOfSocket;
    public Gear currentGear;
    public bool isSocketPanel;
    public int index;

    public GameController gameController;
    public POIC_Controller panelItemController;
    Gear gear;

    void Awake()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        panelItemController = gameController.panelItemController;
        //panelItemController.MeUnenable();
        //
    }

    public void OnMouseDown()
    {
        if (gameObject.transform.childCount > 0)
        {
            gear = gameObject.transform.GetChild(0).GetComponent<Gear>();

            //Debug.Log("w3");

            

            gameController.selectedItem = gameObject;
            gameController.isCanLookItem = true;

        }
    }

    public void OnMouseUp()
    {
        if (gameController.isCanLookItem)
        {

            //panelItemController.MeEnable();

            if (!gear.isEquiped)
            {
                if (gear.type == TypesOfItem.TypeGear.Ring)
                    panelItemController.CreatingPanels3(gear);
                else
                    panelItemController.CreatingPanels2(gear);
            }
            else
            {
                panelItemController.CreatingPanels1(gear);
            }
        }

        gameController.isCanLookItem = false;


    }






}




