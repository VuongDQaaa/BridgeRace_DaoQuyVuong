using UnityEngine;

public class CanvasPause : UICanvas
{
    public void MainMenuButton()
    {
        Time.timeScale = 1;
        CameraController.Instance.DeleteTarget();
        GameManager.Instance.ClearAllObject();
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        Close(0);
    }

    public void RetryButton()
    {
        Close(0);
        Time.timeScale = 1;
        CameraController.Instance.DeleteTarget();
        GameManager.Instance.ClearAllObject();
        GameManager.Instance.StartGame();
    }
}
