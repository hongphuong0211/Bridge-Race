using UnityEngine.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    private GameState gameState;
    private int countLife;
    private int countRing;
    private int level;
    private Vector2 checkPoint = Vector2.zero;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        gameState = GameState.GameMenu;
    }

    public void ChangeState(GameState newState)
    {
        if (gameState != newState)
        {
            if (newState == GameState.GameMenu)
            {
                UIManager.Instance.OpenUI(NumberUI.MainMenu);
            }
            else if(newState == GameState.GamePlay)
            {
                UIManager.Instance.OpenUI(NumberUI.GamePlay);
            }
            else if(newState == GameState.Pause)
            {
                UIManager.Instance.OpenUI(NumberUI.Pause);

            }else if(newState == GameState.EndGame)
            {
                UIManager.Instance.OpenUI(NumberUI.Results);
            }
            gameState = newState;
        }
    }

    public bool IsState(GameState checkState)
    {
        return gameState == checkState;
    }

    
    public bool GetGameResult()
    {
        if(gameState == GameState.EndGame)
        {
            return countLife > 0;
        }
        return false;
    }

    public void RestartGame()
    {
        countLife = 3;
        ChangeState(GameState.GamePlay);
    }
}


