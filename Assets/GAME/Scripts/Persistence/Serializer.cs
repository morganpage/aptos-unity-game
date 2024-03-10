using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class Serializer
{
  public static Action<string> OnSaveComplete;
  public static void Save(string filename, GameData data)
  {
    string fullPath = Application.persistentDataPath + "/" + filename;
    JsonSerializer jsonSerializer = new JsonSerializer();
    jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
    //jsonSerializer.Formatting = Formatting.Indented;
    using (StreamWriter sw = new StreamWriter(fullPath))
    using (JsonWriter writer = new JsonTextWriter(sw))
    {
      jsonSerializer.Serialize(writer, data);
    }
    OnSaveComplete?.Invoke(filename);
  }

  public static GameData Load(string filename)
  {
    string fullPath = Application.persistentDataPath + "/" + filename;
    if (File.Exists(fullPath))
    {
      Debug.Log("Loading from: " + fullPath);
      using (StreamReader sr = new StreamReader(fullPath))
      using (JsonReader reader = new JsonTextReader(sr))
      {
        JsonSerializer jsonSerializer = new JsonSerializer();
        GameData data = jsonSerializer.Deserialize<GameData>(reader);
        return data;
      }
    }
    return null;
  }

}

