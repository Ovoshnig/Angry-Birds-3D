using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace Ovoshnig.DataStorage.Storage;

public sealed class SaveStorage : DataStorage
{
    public override DataStorageType StorageType => DataStorageType.Save;

    protected override string FileName => SaveConstants.FileName;

    private static string HashFilePath => Path.Combine(Application.persistentDataPath,
        SaveConstants.HashFileName);

    protected override void LoadData()
    {
        base.LoadData();

        if (!File.Exists(FilePath) || !File.Exists(HashFilePath))
        {
            ResetData();
            return;
        }

        string savedHash = File.ReadAllText(HashFilePath);
        string currentHash = CalculateHash(FilePath);

        if (savedHash == currentHash)
            return;

        Debug.LogWarning("File integrity check failed. The save file might have been tampered with.");

        if (!Debug.isDebugBuild)
            ResetData();
    }

    protected override void SaveData()
    {
        base.SaveData();

        string fileHash = CalculateHash(FilePath);
        File.WriteAllText(HashFilePath, fileHash);
    }

    private static string CalculateHash(string filePath)
    {
        byte[] fileBytes = File.ReadAllBytes(filePath);
        byte[] hashBytes = SHA256.HashData(fileBytes);

        return Convert.ToBase64String(hashBytes);
    }
}
