using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UIInstance
{
    public UISelectLevel UISelectLevel;
    protected override void OnInit()
    {
        base.OnInit();
        CloseMenu();
    }
    public void StartGame()
    {
        MapManager.Instance.StartLevel(DataManager.GetInt(ConstantManager.LEVEL, 1));
        UIManager.Instance.CloseUI(numberUI);
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }
    public void OpenMenu()
    {
        UISelectLevel.gameObject.SetActive(true);
    }
    public void CloseMenu()
    {
        UISelectLevel.gameObject.SetActive(false);
    }
}
