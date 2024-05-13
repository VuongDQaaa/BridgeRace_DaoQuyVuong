using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    public void MainMenuButton()
    {
        CameraController.Instance.DeleteTarget();
        GameManager.Instance.ClearAllObject();
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    public void NextButton()
    {

    }
}
