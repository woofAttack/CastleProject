using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chances", menuName = "Chances", order = 55)]
public class Chances : ScriptableObject {

    [SerializeField]
    public int sumLevels; // Сколько всего уровней того или иного дела
    [SerializeField]
    public List<Chance> chances;
    

}

[System.Serializable]
public class Chance
{

    public List<float> chances;
    public int minLevel;
    public int disc;

}
