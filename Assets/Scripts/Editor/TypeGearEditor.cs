using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TypeGear))]
public class TypeGearEditor : Editor
{
    private TypeGear typeGear;
    private bool checkArray;
    private GUIStyle gu = new GUIStyle();
    private string meggage;
    SerializedObject serializedBase;

    private void CreateMessage()
    {
        meggage = "";
        typeGear.SummFix();
        for (int i = 0; i < typeGear.chances.Count; i++)
        {
            meggage += "Шанс выпадения " + typeGear.sub[i].type.ToString() + ": " + ((float)(typeGear.chances[i] / typeGear.sumChances * 100)).ToString() + "% \n";
        }
    }

    private void Awake()
    {
        typeGear = (TypeGear)target;
        serializedBase = new SerializedObject(typeGear);
        gu.alignment = TextAnchor.MiddleCenter;

    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        typeGear.nameOfType = (TypesOfItem.TypeGear)EditorGUILayout.EnumPopup("Тип шмотки", typeGear.nameOfType);
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("box");


        typeGear.main = (Stats)EditorGUILayout.ObjectField("Главная хар-ка", typeGear.main, typeof(Stats), false);
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Дополнительные статы", gu);


        for (int i = 0; i < typeGear.sub.Count; i++)
        {
            GUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField("Доп. ", GUILayout.Width(30));
            typeGear.sub[i] = (Stats)EditorGUILayout.ObjectField(typeGear.sub[i], typeof(Stats), false, GUILayout.Width(200));
            EditorGUILayout.LabelField("Шанс: ", GUILayout.Width(45));
            typeGear.chances[i] = EditorGUILayout.FloatField(typeGear.chances[i]);

            if (GUILayout.Button("X", GUILayout.Height(15), GUILayout.Width(15)))
            {
                typeGear.sub.RemoveAt(i);
                typeGear.chances.RemoveAt(i);
            }


            GUILayout.EndHorizontal();
        }


        if (GUILayout.Button("Добавить характеристику"))
        {
            typeGear.sub.Add(new Stats());
            typeGear.chances.Add(new float());
        }
        EditorGUILayout.EndVertical();

        // Тут норм все


        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.LabelField("Спрайты по уровням", gu);

        GUILayout.BeginHorizontal(GUILayout.MaxWidth(100));
        for (int i = 0; i < typeGear.sprites.Count; i++)
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(100), GUILayout.MinHeight(120));
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("X", GUILayout.Height(15), GUILayout.Width(15)))
            {
                typeGear.sprites.RemoveAt(i);
            }

            EditorGUILayout.LabelField("Уровень " + (i + 1).ToString(), GUILayout.Width(80));
            EditorGUILayout.EndHorizontal();

            typeGear.sprites[i] = (Sprite)EditorGUILayout.ObjectField(typeGear.sprites[i], typeof(Sprite), false, GUILayout.MaxHeight(100), GUILayout.MaxWidth(100));
            EditorGUILayout.EndVertical();
            if ((i + 1) % 4 == 0)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal(GUILayout.MaxWidth(100));
            }

            

        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Добавить cпрайт"))
        {
            //typeGear.sprites.Add(CreateInstance<Sprite>());
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();

        CreateMessage();
        EditorGUILayout.HelpBox(meggage, MessageType.Info);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(typeGear);
            serializedBase.ApplyModifiedProperties();
        }

    }

}

