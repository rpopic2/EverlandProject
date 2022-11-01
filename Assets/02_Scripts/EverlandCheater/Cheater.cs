using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using Object = UnityEngine.Object;

public class Cheater : EditorWindow
{
    private readonly char[] _delims = new char[] { '_' };
    private string _customerID = "0000";
    private string _customerName = "김릿지";
    private bool _isButtonPressed;
    [MenuItem("Everland/Cheater")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(Cheater));
    }
    private void OnGUI()
    {
        _customerID = EditorGUILayout.TextField("Customer ID", _customerID);
        _customerName = EditorGUILayout.TextField("Customer Name", _customerName);
        _isButtonPressed = GUILayout.Button("Go");
        if (_isButtonPressed)
        {
            Go();
        }
    }
    private void Go()
    {
        var sprites = GetSprites().ToList();
        var slas = GetSpriteLibraryAssets().ToList();

        var len = sprites.Count;
        if (len != slas.Count) throw new Exception($"Length of texture : {len}, and legnth of SLAs{slas.Count} does not match.");
        while (sprites.Count > 0)
        {
            var sp = sprites.First();
            var spname = GetPartName(sp.name);
            var slaQuery = from s in slas
                           where GetPartName(s.name) == GetPartName(sp.name)
                           select s;
            if (slaQuery.Count() == 0) throw new Exception($"No matching SLA found with sprite : {spname}");
            var slaname = GetPartName(slaQuery.ElementAt(0).name);
            if (slaQuery.Count() >= 2) throw new Exception($"Sprite name mathing with more than one SLAs' name. Sprite : {spname}, SLA : {slaname}");
            var sla = slaQuery.ElementAt(0);
            sprites.Remove(sp);
            slas.Remove(sla);

            sla.AddCategoryLabel(sp, "Default", "1");
            Debug.Log($"Applied texture {sp.name} to {sla.name}");
        }
        foreach(var sr in FindObjectsOfType<SpriteResolver>())
        {
            sr.ResolveSpriteToSpriteRenderer();
        }
    }
    private Sprite[] GetSprites()
    {
        var textures = new List<Sprite>();
        var psb = AssetDatabase.LoadAllAssetsAtPath($"Assets/Univ_Char/{_customerID}_chracter.psb");
        foreach (Object o in psb)
        {
            if (o is Sprite s)
            {
                textures.Add(s);
                Debug.Log($"Found Texture : {o.name}");
            }
        }
        Debug.Log($"Found {textures.Count} textures");
        return textures.ToArray();
    }
    private SpriteLibraryAsset[] GetSpriteLibraryAssets()
    {
        var slasGUID = AssetDatabase.FindAssets("t:SpriteLibraryAsset");
        var slas = new SpriteLibraryAsset[slasGUID.Length];
        for (int i = 0; i < slasGUID.Length; i++)
        {
            string guid = slasGUID[i];
            var p = AssetDatabase.GUIDToAssetPath(guid);
            var sla = AssetDatabase.LoadAssetAtPath<SpriteLibraryAsset>(p);
            slas[i] = sla;
            Debug.Log($"Found SLA : {sla.name}");
        }
        Debug.Log($"Found {slas.Length} SLAs");
        return slas;
    }
    private string GetPartName(string name)
    {
        Debug.Log(name);
        var r = name.ToLower().Split(_delims, 2)[1];
        Debug.Log(r);
        return r;
    }
}