using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.timerText =
            GetComponent<TextMeshProUGUI>();
    }
}
