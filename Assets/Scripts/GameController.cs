using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Camera cam;
    public Gear gear;
    public Borders borders;

    public TypeGear[] typeGear;
    public PlayerStat playerStat;

    public List<Gear> gears;
    public Gear[] gearsInPanel;

    public InventaryGear inventaryGear;
    public List<GameObject> sockets;

    public GameObject selectedItem;
    public bool isCanLookItem;

    public AllStatText info;

    
    public float[,] stats;

    [SerializeField]
    private GameObject item;

    public POIC_Controller panelItemController;

    void Awake () {

        //playerStat.Start();
        gearsInPanel = new Gear[6];
        stats = new float[9, 6];
        info.CreateAllStatText();

        for (int i = 0; i < 2; i++)
        {
            AddGear();
        }



        //sockets = inventaryGear.CreateInventarySocket(gears.Count);
        //CreatingSpriteOfItemInInventary();
        //CreatingSpriteOfItemInPanel();



	}

    public void CreatingSpriteOfItemInInventary()
    {

        for (int i = 0; i < gears.Count; i++)
        {
            GameObject item = sockets[i].transform.GetChild(0).gameObject;
            Gear gearItem = item.AddComponent<Gear>();
            DragItem dragItem = item.GetComponent<DragItem>();
            gearItem.CreateGear(gears[i]);
            dragItem.socket = sockets[i];
            item.GetComponent<Image>().sprite = borders.border[gears[i].rarity];
            item.transform.Find("Border").gameObject.GetComponent<Image>().sprite = gears[i].sprite;
        }
    }

    public void CreatingSpriteOfItemInPanel()
    {
        //gearsInPanel.Add(new Gear(typeGear[4]));
        //gearsInPanel.Add(new Gear(typeGear[1]));
        //gearsInPanel.Add(new Gear(typeGear[2]));
        //gearsInPanel.Add(new Gear(typeGear[3]));
        //gearsInPanel.Add(new Gear(typeGear[3]));
        //gearsInPanel.Add(new Gear(typeGear[0]));

        for (int i = 0; i < playerStat.e_sockets.Length; i++)
        {
            GameObject itm = Instantiate(item, playerStat.e_sockets[i].transform);
            itm.name = "Item";
            Gear gearItem = itm.AddComponent<Gear>();
            DragItem dragItem = itm.GetComponent<DragItem>();
            gearItem.CreateGear(gearsInPanel[i]);
            dragItem.socket = playerStat.e_sockets[i];

            itm.GetComponent<Image>().sprite = borders.border[gearsInPanel[i].rarity];
            itm.transform.Find("Border").gameObject.GetComponent<Image>().sprite = gearsInPanel[i].sprite;

            playerStat.e_sockets[i].GetComponent<socketFix>().currentGear = gearsInPanel[i];


        }


    }

    public void AddGear()
    {
        gears.Add(new Gear(typeGear[Random.Range(0, typeGear.Length)]));
        sockets = inventaryGear.CreateAdditionalSockets(gears.Count);

        int index = gears.Count - 1;

        GameObject itm = Instantiate(item, sockets[index].transform);

        Gear gearItem = itm.AddComponent<Gear>();
        DragItem dragItem = itm.GetComponent<DragItem>();

        itm.name = "Item";
        gearItem.CreateGear(gears[index]);
        dragItem.socket = sockets[index];
        gearItem.index = index;

        gears[index] = gearItem;

        itm.GetComponent<Image>().sprite = borders.border[gears[index].rarity];
        itm.transform.Find("Border").gameObject.GetComponent<Image>().sprite = gears[index].sprite;


    }

    public void RemoveGear()
    {

        if (selectedItem != null)
        {
            Debug.Log(gears.Count);
            Gear gear = selectedItem.GetComponentInChildren<Gear>();
            //gears.Remove(selectedItem.GetComponent<Gear>());
            int index = gear.index;
            gears.RemoveAt(index);
            //info.Clean();

            Destroy(selectedItem.transform.GetChild(0).gameObject);
            selectedItem = null;
            SortingGear(index);


            Debug.Log(gears.Count);
        }

    }

    public void RemoveGear(Gear gear, socketFix socket, float[] stats)
    {
        if (selectedItem != null)
        {
            gearsInPanel[socket.index] = gear;
            int index = gear.index;
            gears.RemoveAt(index);
            gear.isEquiped = true;
            //info.Clean();

            GameObject itm = selectedItem.transform.GetChild(0).gameObject;
            itm.transform.SetParent(socket.transform);
            itm.transform.localPosition = new Vector3(0f, 0f);
            selectedItem = null;

            SortingGear(index);


            StatSwap(socket.index, stats);




            Debug.Log(gears.Count);
        }
    }

    public void SortingGear(int index)
    {

        for (int i = index; i < gears.Count; i++)
        {
            //if (sockets[i + 1].transform.childCount == 0) { break; }
            GameObject itm = sockets[i + 1].transform.GetChild(0).gameObject;
            itm.transform.SetParent(sockets[i].transform);
            itm.transform.localPosition = new Vector3(0f, 0f);
            itm.GetComponent<Gear>().index = i;
            itm.GetComponent<DragItem>().socket = sockets[i];
        }

        sockets = inventaryGear.DeleteLastSockets(gears.Count);

    }

    public void AddGear(Gear gear)
    {
        gearsInPanel[gear.GetComponentInParent<socketFix>().index] = null;

        gears.Add(new Gear(gear));
        sockets = inventaryGear.CreateAdditionalSockets(gears.Count);

        int index = gears.Count - 1;
        gear.gameObject.transform.SetParent(sockets[index].transform);
        gear.gameObject.transform.localPosition = new Vector3(0f, 0f);

       gear.index = index;
        gear.isEquiped = false;

        //gearsInPanel[gear.gameObject.GetComponent<DragItem>().socket.GetComponent<socketFix>().index] = null;
        //gear.gameObject.GetComponent<DragItem>().socket = sockets[index];
        //gear.gameObject.GetComponent<DragItem>().socketPut = null;

        gears[index] = gear;

        Debug.Log(gears.Count);
    }

    public void AddGear(Gear gear, int index)
    {
        AddGear(gear);
        StatZero(index);
    }

    public void SwapGear(GameObject itemIn, GameObject itemOut)
    {
        Gear gearItemIn = itemIn.GetComponent<Gear>();
        Gear gearItemOut = itemOut.GetComponent<Gear>();

        DragItem dragItemIn = itemIn.GetComponent<DragItem>();
        DragItem dragItemOut = itemOut.GetComponent<DragItem>();

        if (itemOut.GetComponent<socketFix>() != null)
        {
            itemOut.GetComponent<socketFix>().currentGear = gearItemIn;
            RemoveGear();
            AddGear(gearItemOut);
        }
        else
        {
            itemIn.GetComponent<socketFix>().currentGear = gearItemIn;
        }

        itemIn.transform.SetParent(dragItemIn.socketPut.transform);
        itemOut.transform.SetParent(dragItemOut.socket.transform);

    }

    public void SwapGear(Gear gearOut, Gear gearIn, float[] stats)
    {
        Transform socketOut = gearOut.gameObject.transform.parent;
        Transform socketIn = gearIn.gameObject.transform.parent;
        
        //RemoveGear();
        AddGear(gearOut);
        gearOut.isEquiped = false;
        gearIn.isEquiped = true;

        gearsInPanel[socketOut.GetComponent<socketFix>().index] = gearIn;
        StatSwap(socketOut.GetComponent<socketFix>().index, stats);
        gearIn.transform.SetParent(socketOut);

        
        SortingGear(gearIn.index);
        gears.RemoveAt(gearIn.index);
        gearIn.transform.localPosition = new Vector3(0f, 0f);



    }

    public void StatSwap(int indexSocket, float[] stat)
    {
        for (int i = 0; i < 9; i++)
        {
            stats[i, indexSocket] = stat[i];
        }
        info.CreateAllStatText();
    }

    public void StatZero(int indexSocket)
    {
        for (int i = 0; i < 9; i++)
        {
            stats[i, indexSocket] = 0;
        }
        info.CreateAllStatText();
    }


}
