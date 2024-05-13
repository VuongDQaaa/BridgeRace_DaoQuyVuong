using TMPro;
using UnityEngine;

public class CanvasGameplay : UICanvas
{
    [SerializeField] private TextMeshProUGUI coinText;
    public override void SetUp()
    {
        base.SetUp();
        UpdateCoint(0);
    }
    public void PauseButton()
    {
        Time.timeScale = 0;
        UIManager.Instance.OpenUI<CanvasPause>();
    }

    public void UpdateCoint(int coin)
    {
        coinText.text = coin.ToString();
    }
}
