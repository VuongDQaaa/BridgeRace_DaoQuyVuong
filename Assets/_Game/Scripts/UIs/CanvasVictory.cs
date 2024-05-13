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
        UIManager.Instance.CloseAll();
        CameraController.Instance.DeleteTarget();
        GameManager.Instance.ClearAllObject();
        GameManager.Instance.StartGame();
    }
}
