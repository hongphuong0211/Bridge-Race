using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    public void ButtonStartGame()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
        Close();
        UIManager.Instance.OpenUI(UIID.UICGamePlay);
        LevelManager.Instance.StartLevel(GameManager.Instance.userData.PlayingLevel);
    }
}
