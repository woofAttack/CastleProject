using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Stats))]
public class StatEditor : Editor
{
    private Stats stats;
    private string[] options = new string[] { "Обычный", "Вычитаемый", "Стихийный" };
    private bool showArray;
    private string message;


    private void Awake()
    {
        stats = (Stats)target;
        stats.CalculateValues();
        CreateMessage();
    }

    public override void OnInspectorGUI()
    {


        stats.whatIsOption = GUILayout.SelectionGrid(stats.whatIsOption, options, options.Length);



        //stats.nameOfStat = EditorGUILayout.TextField(new GUIContent("Название хар-ки: "), stats.nameOfStat);

        stats.type = (TypesOfItem.Type)EditorGUILayout.EnumPopup("Название хар-ки", stats.type);

        if (stats.whatIsOption == 0)
        {
            stats.minDifference = EditorGUILayout.FloatField("Мин.  значение в %", stats.minDifference);
            stats.maxDifference = EditorGUILayout.FloatField("Макс. значение в %", stats.maxDifference);
        }
        else if (stats.whatIsOption == 1)
        {
            stats.minDifference = EditorGUILayout.FloatField("Отклонение +/-", stats.minDifference);
        }
        else
        {
            stats.minDifference = EditorGUILayout.FloatField("Отклонение +/-", stats.minDifference);
            stats.element = (TypesOfItem.Element)EditorGUILayout.EnumPopup("Елемент стихии", stats.element);
        }

        showArray = EditorGUILayout.Foldout(showArray, "Уровни");
        if (showArray)
        {
            message = "";
            for (int i = 0; i < stats.statValue.Length; i++)
            {
                stats.statValue[i] = EditorGUILayout.FloatField("Уровень " + (i + 1).ToString(), stats.statValue[i]);
            }
        }

        if (GUILayout.Button("Обновить расчет статов"))
        {
            CreateMessage();
        }
        EditorGUILayout.HelpBox(message, MessageType.Info);

        if (GUI.changed) EditorUtility.SetDirty(stats);




    }

    private void CreateMessage()
    {
        message = "";
        stats.CalculateValues();
        for (int i = 0; i < stats.statValue.Length; i++)
        {
            message += "Уровень " + (i + 1).ToString() + ":\n (main) " + stats.statValueResult[i, 0].ToString() + " - " + stats.statValueResult[i, 1].ToString() +
                " \n (sub) " + stats.statSubValueResult[i, 0].ToString() + " - " + stats.statSubValueResult[i, 1].ToString() + "\n\n";
        }
    }



}


