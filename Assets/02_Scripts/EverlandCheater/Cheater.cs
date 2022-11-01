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
            ApplySprites();
            ApplyName();
        }
        EditorGUILayout.LabelField("Cheatsheet :\n1.Drag the file in and select\n2.Hit E and Create bones.\n- Head : Chin->Top of forehead\n- Body : Bottom->Top(Neck)\n- Others : Inner->Mid->Outer\n3.Hit A and press Generate For All Visible\n4.Hit V and check bone influences.\n5.Hit GO button above\n6.Hit ^1 and check if char looks okay.\n7.Test and build. Voila!", GUILayout.ExpandHeight(true));
    }
    private void ApplyName()
    {
        FindObjectOfType<PlayerSetting>().playerName = _customerName;
        Debug.Log($"PlayerSetting.playerName set to {_customerName}");
    }
    private void ApplySprites()
    {
        var sprites = GetSprites().ToList();
        var slas = GetSpriteLibraryAssets().ToList();
        var len = sprites.Count;
        if (len != slas.Count) throw new Exception($"Count of texture : {len}, and SLAs : {slas.Count} does not match.");
        while (sprites.Count > 0)
        {
            var sp = sprites.First();
            var sla = GetMatchingSLA(sp);
            sprites.Remove(sp);
            slas.Remove(sla);
            sla.AddCategoryLabel(sp, "Default", "1");
            Debug.Log($"Applied texture {sp.name} to {sla.name}");
        }
        foreach (var sr in FindObjectsOfType<SpriteResolver>())
        {
            sr.ResolveSpriteToSpriteRenderer();
        }
        SpriteLibraryAsset GetMatchingSLA(Sprite sp)
        {
            var spname = sp.name;
            var slaQuery = from s in slas
                           where GetPartName(s.name) == GetPartName(spname)
                           select s;
            if (slaQuery.Count() == 0) throw new Exception($"No matching SLA found with sprite : {spname}");
            var slaname = GetPartName(slaQuery.ElementAt(0).name);
            if (slaQuery.Count() >= 2) throw new Exception($"Sprite name mathing with more than one SLAs' name. Sprite : {spname}, SLA : {slaname}");
            return slaQuery.ElementAt(0);
        }
    }
    private List<Sprite> GetSprites()
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
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>($"Assets/Univ_Char/{_customerID}_chracter.psb");
        Debug.Log($"Found {textures.Count} textures");
        return textures;
    }
    private List<SpriteLibraryAsset> GetSpriteLibraryAssets()
    {
        var slasGUID = AssetDatabase.FindAssets("t:SpriteLibraryAsset");
        var slas = new List<SpriteLibraryAsset>(slasGUID.Length);
        for (int i = 0; i < slasGUID.Length; i++)
        {
            string guid = slasGUID[i];
            var p = AssetDatabase.GUIDToAssetPath(guid);
            var sla = AssetDatabase.LoadAssetAtPath<SpriteLibraryAsset>(p);
            slas.Add(sla);
            Debug.Log($"Found SLA : {sla.name}");
        }
        Debug.Log($"Found {slas.Count} SLAs");
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