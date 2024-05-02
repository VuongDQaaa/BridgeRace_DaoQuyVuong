using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGameplay>();
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<CanvasSetting>();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
