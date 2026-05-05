using Cysharp.Threading.Tasks;
using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class SaveStorage : DataStorage
{
    protected override string FileName => SaveConstants.FileName;

    private string HashFilePath => Path.Combine(Application.persistentDataPath, SaveConstants.HashFileName);

    protected override async UniTask LoadDataAsync()
    {
        await base.LoadDataAsync();

        if (!File.Exists(FilePath) || !File.Exists(HashFilePath))
        {
            ResetData();
            return;
        }

        string savedHash = await File.ReadAllTextAsync(HashFilePath);
        string currentHash = await CalculateHashAsync(FilePath);

        if (savedHash == currentHash)
            return;

        Debug.LogWarning("File integrity check failed. The save file might have been tampered with.");
        ResetData();
    }

    protected override async UniTask SaveDataAsync()
    {
        await base.SaveDataAsync();

        string fileHash = await CalculateHashAsync(FilePath);
        await File.WriteAllTextAsync(HashFilePath, fileHash);
    }

    private async UniTask<string> CalculateHashAsync(string filePath)
    {
        return await UniTask.RunOnThreadPool(() =>
        {
            using SHA256 sha256 = SHA256.Create();
            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            byte[] hashBytes = sha256.ComputeHash(stream);
            return Convert.ToBase64String(hashBytes);
        });
    }
}
