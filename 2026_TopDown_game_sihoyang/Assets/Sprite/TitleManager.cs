using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void GameStartButton()
    {
        GameManager.instance.StartGame();
    }
}
