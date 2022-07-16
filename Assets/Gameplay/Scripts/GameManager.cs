using UnityEngine.Events;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public UserData userData;
    private GameState gameState;
    public  GameState GameState => gameState;
    private int level;

    private void Start()
    {
        gameState = GameState.GameMenu;
        userData.OnInitData();
        UIManager.Instance.OpenUI(UIID.UICMainMenu);
    }

    public void ChangeState(GameState newState)
    {
        gameState = newState;
    }

    public bool IsState(GameState checkState)
    {
        return gameState == checkState;
    }

}


