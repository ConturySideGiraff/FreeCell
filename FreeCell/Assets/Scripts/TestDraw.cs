using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class TestDraw : EditorWindow
{
    [MenuItem("CSG/Draw")]
    public static void Init()
    {
        TestDraw window = (TestDraw)GetWindow(typeof(TestDraw));

        float width = 270f;

        window.Show();
        window.titleContent.text = "Test Draw";
        window.minSize = new Vector2(width, 150f);
        window.maxSize = new Vector2(width, 900f);
    }

    private Texture2D _numTex, _jTex, _qTex, _kTex;
    private string _product, _version;

    private void OnGUI()
    {
        EditorGUILayout.LabelField("[ Set : 0 ]");
        _product =  EditorGUILayout.TextField("Product", _product);
        _version =  EditorGUILayout.TextField("Version", _version);


        EditorGUILayout.LabelField("[ Set : 1 ]");
        TexGUI("Nnum - Texture", ref _numTex);
        TexGUI("J - Texture", ref _jTex);
        TexGUI("Q - Texture", ref _qTex);
        TexGUI("K - Texture", ref _kTex);

        EditorGUILayout.LabelField("[ Set : 2 ]");

        if (GUILayout.Button("Make"))
        {
            if (string.IsNullOrEmpty(_product) || string.IsNullOrEmpty(_version))
            {
                return;
            } 

        }
    }

    private void TexGUI(string name, ref Texture2D tex)
    {
        // option
        float txtWidth = 200;
        float height = 64;

        // gui
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(name, GUILayout.Width(txtWidth), GUILayout.Height(height));
        tex = EditorGUILayout.ObjectField(tex, typeof(Texture2D), false, GUILayout.Width(height), GUILayout.Height(height)) as Texture2D;
        EditorGUILayout.EndHorizontal();
    }
}
