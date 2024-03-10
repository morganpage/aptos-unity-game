using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SyncController : MonoBehaviour
{
  void OnEnable()
  {
    Serializer.OnSaveComplete += HandleSaveComplete;
  }

  void OnDisable()
  {
    Serializer.OnSaveComplete -= HandleSaveComplete;
  }

  void HandleSaveComplete(string fileName) //On save, lets sync to the blockchain!
  {
    Debug.Log("SyncController.OnSave: " + fileName);
    GameData gameData = Serializer.Load(fileName);
    string json = JsonConvert.SerializeObject(gameData);
    Debug.Log("SyncController.HandleSaveComplete: " + json);
    AptosManager aptosManager = FindObjectOfType<AptosManager>();
    aptosManager.InitWalletFromCache();
    aptosManager.SendDataToBlockchain(json);
  }

  [ContextMenu("TestSync")]
  void TestSync()
  {
    GameData gameData = new GameData();//Create a new GameData object and pulls in the current state of the game world
    gameData.Influence = 99;//Just to test
    Serializer.Save("gameWorld.dat", gameData);
  }


}
