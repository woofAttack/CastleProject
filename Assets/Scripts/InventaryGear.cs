using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventaryGear : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField]
    private GameObject socket;
    [SerializeField]
    private GameObject item;
    [SerializeField]
    private GameController gameController;

    public Transform[] socketTransform;
    public GameObject[] items;

    public List<GameObject> prefabed;
    public int countRawSockets;
    public int rawDivine;

    void Awake()
    {
        gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
        gridTop = gridLayoutGroup.padding.top;
        gridSocketSize = (int)(gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x);
    }

    public List<GameObject> CreateInventarySocket(int count)
    {

        socketTransform = new Transform[count];
        items = new GameObject[count];



        for (int i = 0; i < count; i++)
        {
            prefabed.Add(Instantiate(socket, gameObject.transform));
            socketTransform[i] = prefabed[i].transform;
            items[i] = Instantiate(item, socketTransform[i]);
            items[i].name = "Item";
        }

        return prefabed;
    }

    public List<GameObject> CreateAdditionalSockets(int countGear)
    {
        int countNewSockets = 0;
        if (prefabed.Count < countRawSockets * 4)
        {
            countNewSockets = countRawSockets * 4;
            for (int i = 0; i < countNewSockets; i++)
            {
                prefabed.Add(Instantiate(socket, gameObject.transform));

            }
        }
        else if (prefabed.Count < countGear)
        {
            countNewSockets = countGear - prefabed.Count;
            countNewSockets = countNewSockets / countRawSockets + 1;
            for (int i = 0; i < countNewSockets * countRawSockets; i++)
            {
                prefabed.Add(Instantiate(socket, gameObject.transform));

            }

        }



        


        rawDivine = prefabed.Count % countRawSockets == 0 ? prefabed.Count / countRawSockets : prefabed.Count / countRawSockets + 1;
        return prefabed;
    }

    public List<GameObject> DeleteLastSockets(int countGear)
    {

        if (countGear < countRawSockets * 4)
        {


        }
        else
        {


            int countGeatDivine = countGear % countRawSockets == 0 ? countGear / countRawSockets : countGear / countRawSockets + 1;

            int countPrefabedDivine = prefabed.Count % countRawSockets == 0 ? prefabed.Count / countRawSockets : prefabed.Count / countRawSockets + 1;
            Debug.Log(countGeatDivine + " : " + countPrefabedDivine);

            if (countGeatDivine < countPrefabedDivine)
            {
                for (int i = countGear; i < prefabed.Count; i++)
                {

                    Destroy(prefabed[i]);

                }

                prefabed.RemoveRange(countGear, countRawSockets);
            }
        }

        rawDivine = prefabed.Count % countRawSockets == 0 ? prefabed.Count / countRawSockets : prefabed.Count / countRawSockets + 1;
        return prefabed;
    }



    GridLayoutGroup gridLayoutGroup;
    float spacing;
    float old;

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        Vector3 draw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spacing = draw.y;
        gameController.isCanLookItem = false;
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        Vector3 draw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gridLayoutGroup.padding.top += (int)(-(draw.y - spacing)  * 120);
        spacing = draw.y;
        gridLayoutGroup.SetLayoutVertical();
    }

    private int gridTop = 40; // Отступ сверху
    private int gridSocketSize = 125; // Сокет + пробел

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (gridLayoutGroup.padding.top > gridTop) gridLayoutGroup.padding.top = gridTop;
        if (rawDivine > 4)
        {
            if (gridLayoutGroup.padding.top < gridTop - (gridSocketSize * (rawDivine - 4))) gridLayoutGroup.padding.top = gridTop - (gridSocketSize * (rawDivine - 4));
        }
        else
        {
            gridLayoutGroup.padding.top = gridTop;
        }
        gridLayoutGroup.SetLayoutVertical();
        
        
    }

}

