using System;
using System.Collections.Generic;
using System.IO;
using Dalamud.Logging;
using NecroLens.Model;
using Newtonsoft.Json;

namespace NecroLens.Service;

public class MobInfoService : IDisposable
{
    public readonly Dictionary<uint, MobInfo> MobInfoDictionary;

    public MobInfoService()
    {
        MobInfoDictionary = new Dictionary<uint, MobInfo>();
        LoadDeepDungeonMobInfos();
    }

    public void Dispose()
    {
        MobInfoDictionary.Clear();
    }

    private void LoadDeepDungeonMobInfos()
    {
        PluginLog.Log("Loading Mob infos...");
        LoadMobInfoFile(Path.Combine(PluginService.PluginInterface.AssemblyLocation.Directory?.FullName!,
                                     "data/allMobs.json"));
    }

    private void LoadMobInfoFile(string path)
    {
        var info = JsonConvert.DeserializeObject<List<MobInfo>>(File.ReadAllText(path));
        if (info != null)
        {
            foreach (var mobInfo in info)
                MobInfoDictionary[mobInfo.Id] = mobInfo;
        }
        else
            PluginLog.Error($"Unable to load MobInfo file {path}!");
    }
}
