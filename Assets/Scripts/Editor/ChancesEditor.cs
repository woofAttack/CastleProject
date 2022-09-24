using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Chances))]
public class ChancesEditor : Editor {

    private Chances chances;

    private void Awake()
    {
        chances = (Chances)target;
    }

    public override void OnInspectorGUI()
    {
        
        EditorGUILayout.BeginHorizontal("box");

        EditorGUILayout.LabelField("Сколько всего разных уровней: ");
        chances.sumLevels = EditorGUILayout.IntField(chances.sumLevels);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        for (int i = 0; i < chances.chances.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.Space();

            if (chances.chances[i].chances == null) { chances.chances[i].chances = new List<float>(); }

            EditorGUILayout.BeginHorizontal();

            switch (i)
            {
                case 0:
                    EditorGUILayout.LabelField("Шансы для 1 уровня, при дискриминанте: ");                   
                    break;
                case 1:
                    EditorGUILayout.LabelField("Шансы для 2 уровня, при дискриминанте: ");
                    break;
                case 2:
                    EditorGUILayout.LabelField("Шансы для 3-" + (chances.sumLevels - 2) + " уровней, при дискриминанте: ");
                    break;
                case 3:
                    EditorGUILayout.LabelField("Шансы для " + (chances.sumLevels - 1) + " уровня, при дискриминанте: ");
                    break;
                default:
                    EditorGUILayout.LabelField("Шансы для " + (chances.sumLevels) + " уровня, при дискриминанте: ");
                    break;
            }

            chances.chances[i].disc = EditorGUILayout.IntField(chances.chances[i].disc, GUILayout.MaxWidth(120));
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();

            int countCh = chances.chances[i].chances.Count + 2;
            //Debug.Log(chances.chances[i].chances.Count);
            for (int j = 0; j < countCh; j++)
            {
                if (j == 0 || j == (countCh - 1)) { EditorGUILayout.LabelField("+"); }
                else
                {

                    if (i == 0)      { EditorGUILayout.LabelField(j.ToString()); }
                    else if (i == 1) { EditorGUILayout.LabelField((j - chances.chances[i].disc).ToString()); }
                    else if (i == 2) { EditorGUILayout.LabelField("n " + (j - chances.chances[i].disc).ToString()); }
                    else if (i == 3) { EditorGUILayout.LabelField((chances.sumLevels - chances.chances[i].disc - 1).ToString()); }
                    else             { EditorGUILayout.LabelField((chances.sumLevels - chances.chances[i].disc).ToString()); }

                }
            }

            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("-"))
            {
                chances.chances[i].chances.RemoveAt(chances.chances[i].chances.Count - 1);
            }
                        
            for (int j = 0; j < chances.chances[i].chances.Count; j++)
            {
                chances.chances[i].chances[j] = EditorGUILayout.FloatField(chances.chances[i].chances[j]);
            }            
            
            if (GUILayout.Button("+"))
            {
                chances.chances[i].chances.Add(new float());
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();

        }

        if (GUILayout.Button("Добавить"))
        {
            if (chances.chances != null)
            {
                if (chances.chances.Count < 5)
                {
                    chances.chances.Add(new Chance());
                }
            }
            else
            {
                chances.chances = new List<Chance>();
            }
            
        }







    }



}
