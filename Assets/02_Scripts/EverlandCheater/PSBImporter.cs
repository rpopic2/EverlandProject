
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class PSBImporter
{
    private static readonly string s_home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    private static readonly string s_download = Path.Combine(s_home, "Downloads");
    private static string FileSuffix => EditorWindow.GetWindow<Cheater>().FileSuffix;
    private static string GetFileName(string s) => s + FileSuffix;

    private static readonly string s_assetFolder = Path.Combine(Environment.CurrentDirectory, "Assets", "Univ_Char");
    public static void Import(string filename)
    {
        filename = GetFileName(filename);
        var target = Path.Combine(s_download, filename);
        try
        {
            File.Copy(target, Path.Combine(s_assetFolder, filename), true);
        }
        catch (FileNotFoundException)
        {

            Debug.LogError($"File not found : {target}");
        }
    }

}