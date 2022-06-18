using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonLevel : MonoBehaviour
{
    public GameObject keyImage;
    public Sprite openLevel;
    public Sprite closeLevel;
    public TextMeshProUGUI textLevel;
    private int curLevel;
    private Image imageBackground;
    private void Awake()
    {
        imageBackground = GetComponent<Image>();
    }

    public void SetLevel(int level)
    {
        curLevel = level;
        UpdateView();
    }

    public void UpdateView()
    {
        textLevel.SetText(curLevel.ToString());
        if (curLevel <= DataManager.GetInt(ConstantManager.LEVEL, 1))
        {
            imageBackground.sprite = openLevel;
            keyImage.SetActive(false);
        }
        else
        {
            imageBackground.sprite = closeLevel;
            keyImage.SetActive(true);
        }
    }

    public void SelectLevel()
    {
        MapManager.Instance.StartLevel(curLevel);
        UIManager.Instance.CloseUI(NumberUI.MainMenu);
        GameManager.Instance.ChangeState(GameState.GamePlay);
        
    }
}
