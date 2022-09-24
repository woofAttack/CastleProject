using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour {

    [SerializeField]
    private GameObject[] panels;

    public void OpenPanel(int index)
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        if (index != 5) panels[index].SetActive(true);
    }

}
