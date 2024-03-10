using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGame : MonoBehaviour
{

  [ContextMenu("Save Game")]
  public void SaveGame()
  {
    GameWorld.Save();
  }

}
