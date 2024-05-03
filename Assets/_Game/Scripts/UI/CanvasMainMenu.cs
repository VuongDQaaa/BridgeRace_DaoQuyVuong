using TMPro;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private TMP_Dropdown colorDropDown;

    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGameplay>();
        GameManager.Instance.currentGameState = GameManager.GameState.playing;
        GameManager.Instance.StartGame();
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<CanvasSetting>();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SelectPlayerColorDropdown()
    {
        GameManager.Instance.playerColor = SelectedColor(colorDropDown.value);
    }

    private ColorType SelectedColor(int dropDownValue)
    {
        switch (dropDownValue)
        {
            case 0:
                return ColorType.Red;
            case 1:
                return ColorType.Blue;
            case 2:
                return ColorType.Green;
            case 3:
                return ColorType.Pink;
            case 4:
                return ColorType.Yellow;
            default:
                return ColorType.Red;
        }
    }
}
