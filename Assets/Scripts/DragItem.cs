using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Gear gear;
    public GameObject socketPutItem, socket, socketPut;
    public GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D item)
    {
        gear = gameObject.GetComponent<Gear>();
        socketFix socketFix = item.transform.GetComponentInParent<socketFix>();
        

        // Если предмет соприкасается с панель-сокетами
        if (socketFix != null && socketFix.isSocketPanel && socketFix.nameOfSocket == gear.type)
            {
            // Если панель-сокет имеет уже предмет
            if (item.name == "Item")
            {                
                
                    socketPut = item.GetComponent<DragItem>().socket;
                    socketPutItem = item.gameObject;
                    //Debug.Log(gear.index + " entered with " + item.name);
                
            }
            // Если панель-сокет не имеет предмет
            else if (item.gameObject.transform.childCount == 0)
            {
                socketPut = item.gameObject;
                socketPutItem = null;
                //Debug.Log(gear.index + " entered with empty " + item.name);
            }
        }

    }

    /*
    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        gear = gameObject.GetComponent<Gear>();
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
        Vector3 draw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(draw.x, draw.y);
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        // Если есть куда положить
        if (socketPut != null && socketPut.GetComponent<socketFix>().nameOfSocket == gear.type 
            && gameObject.GetComponent<BoxCollider2D>().IsTouching(socketPut.GetComponent<BoxCollider2D>()))
        {
            
                // И при этом если там занято, то меняем местами позиции

                // Однако, нужно будет переписать, чтобы
                // Кладешь в новую позицию - gearPanel обновляется, gear лишается по индексу - 
                // а старый предмет лишается gearPanel - идет пересортировка инвентаря - старый предмет получает новую позицию в конец
                if (socketPutItem != null)
                {
                    gameObject.transform.SetParent(socketPut.transform);
                    gameObject.transform.localPosition = new Vector3(0f, 0f);

                    socketPutItem.transform.SetParent(socket.transform);
                    socketPutItem.transform.localPosition = new Vector3(0f, 0f);

                    DragItem dragItem = socketPutItem.GetComponent<DragItem>();
                    Gear gearItem = socketPutItem.GetComponent<Gear>();

                    dragItem.socket = socket;
                    gearItem.index = gear.index;

                    gameController.gearsInPanel[socketPut.GetComponent<socketFix>().index] = gear;
                    gameController.gears[gear.index] = gearItem;

                    socketPutItem.GetComponent<Gear>().isEquiped = false;

                    socket = socketPut;
                    socketPut = null;
                    gear.isEquiped = true;

                }
                // А если свободно, то просто кладем на это место
                else
                {
                    if (!gear.isEquiped)
                    {
                        gameController.RemoveGear(gear, socketPut.GetComponent<socketFix>());
                        gameObject.transform.SetParent(socketPut.transform);
                        gameObject.transform.localPosition = new Vector3(0f, 0f);
                        socket = socketPut;
                        socketPut = null;
                        gear.isEquiped = true;
                    }
                    else
                    {
                        gameObject.transform.SetParent(socket.transform);
                        gameObject.transform.localPosition = new Vector3(0f, 0f);
                    }
                }
            

        }
        // Если нет куда положить, то возвращаем обратно
        else
        {
            if (!gear.isEquiped)
            {
                gameObject.transform.SetParent(socket.transform);
                gameObject.transform.localPosition = new Vector3(0f, 0f);
            }
            else
            {
                gameController.AddGear(gear);
                gameObject.transform.localPosition = new Vector3(0f, 0f);
                //Debug.Log("ssss"); // Заброска в инвентарь
            }
        }

    }

        */

    }
