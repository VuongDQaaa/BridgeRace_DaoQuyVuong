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
        Close(0);
        CameraController.Instance.DeleteTarget();
        GameManager.Instance.currentGameState = GameManager.GameState.playing;
        GameManager.Instance.ClearAllObject();
        GameManager.Instance.StartGame();
    }
}
