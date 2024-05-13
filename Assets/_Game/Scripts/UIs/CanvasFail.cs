public class CanvasFail : UICanvas
{
    public void MainMenuButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    public void RetryButton()
    {
        Close(0);
        CameraController.Instance.DeleteTarget();
        GameManager.Instance.ClearAllObject();
        GameManager.Instance.StartGame();
    }
}
