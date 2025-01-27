using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveBarController : MonoBehaviour
{
    public Image waveBarValue;
    public TextMeshProUGUI waveBarTimeValue;
    public TextMeshProUGUI tooltip;

    private void Update()
    {
        UpdateValues();
    }

    private void UpdateValues()
    {
        tooltip.text = Manager.UI.GetTooltip();
        UpdateRoundProgressBar();
    }

    private void UpdateRoundProgressBar()
    {
        // Update the round progress bar value
        if (Manager.Game != null)
        {
            waveBarTimeValue.text = $"{((int)Manager.Game.timeBeforeNextWave).ToString("D2")}s";
            if (Manager.Game.isOnRestTime)
            {
                waveBarValue.fillAmount =
                    1 - Manager.Game.timeBeforeNextWave / Manager.Game.baseCountingTime;
            }
            else if (Manager.Game.isOnWave)
            {
                waveBarValue.fillAmount =
                    Manager.Game.timeBeforeNextWave / Manager.Game.baseCountingTime;
            }
            else
            {
                waveBarValue.fillAmount =
                    1 - Manager.Game.timeBeforeNextWave / Manager.Game.baseCountingTime;
            }
        }
    }
}
