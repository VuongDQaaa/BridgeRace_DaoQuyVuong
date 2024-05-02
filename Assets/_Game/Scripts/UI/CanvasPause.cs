using UnityEngine;

public class CanvasPause : UICanvas
{
    public void MainMenuButton()
    {
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

    }
}
