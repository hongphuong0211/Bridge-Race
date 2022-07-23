using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Transform tf;
    public ItemInstance prefabItem;
    public EnemyController prefabEnemy;
    public PlayerController m_MainPlayer;
    public PathBridge prefabPathBridge;
    public MapManager goalPrefab;
    private Dictionary<Collider, CharacterInstance> dict_col;
    private MapManager currentLevel;
    private LevelSettings currentLevelSettings;
    public LevelSettings Settings { get { return currentLevelSettings; } }
    private List<MapManager> allStages;
    private List<EnemyController> allEnemy;

    public void StartLevel(int level)
    {
        allEnemy = new List<EnemyController>();
        allStages = new List<MapManager>();
        StartCoroutine(LoadLevel(1));
        for (int i = 0; i < allEnemy.Count; i++)
        {
            allEnemy[i].ChangeState(IdleState.Instance);
        }
    }

    private IEnumerator LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevelSettings = Resources.Load<LevelSettings>("LevelSetting/Levels_" + level.ToString());
        for (int j = 0; j < currentLevelSettings.numberStages; j++)
        {
            currentLevel = Instantiate(Resources.Load<MapManager>("Level/Levels_" + level.ToString()), tf);
            allStages.Add(currentLevel);
            if (j == 0 && allStages.Count > 0)
            {
                for (int i = 0; i < currentLevelSettings.enemyColors.Count; i++)
                {
                    allStages[0].SpawnBrickInStage(i, 225 / currentLevelSettings.enemyColors.Count);
                    if (i > 0)
                    {
                        EnemyController enemy = (EnemyController)SimplePool.Spawn(prefabEnemy);
                        enemy.MapManager = allStages[0];
                        enemy.poolType = PoolType.None;
                        enemy.ID = IngameType.ENEMY;
                        enemy.tf.position = Vector3.left * Random.Range(-1.2f, 1.2f) + Vector3.forward * Random.Range(-1.2f, 1.2f);
                        enemy.Setup(i);

                        allEnemy.Add(enemy);
                    }
                }
            }
            allStages[j].tf.localPosition = (Vector3.up + Vector3.forward * 5f) * j - 0.05f * Vector3.up;
            for (int i = 0; i < allStages[j].doorStart.Length; i++)
            {
                if (allStages[j].doorStart[i] != null)
                {
                    allStages[j].doorStart[i].currentMap = allStages[j];
                    allStages[j].doorStart[i].map = allStages[j];
                }
            }
            for (int i = 0; i < allStages[j].doorEnd.Length; i++)
            {
                if (allStages[j].doorEnd[i] != null)
                {
                    allStages[j].doorEnd[i].currentMap = allStages[j];
                }
            }
            if (j == currentLevelSettings.numberStages - 1)
            {
                MapManager genGoal = Instantiate(Resources.Load<MapManager>("Goal/Levels_" + level.ToString()), tf);
                genGoal.tf.position = (Vector3.up + Vector3.forward * 5f) * (j + 1) - 0.05f * Vector3.up;
                allStages[j].doorEnd[allStages[j - 1].doorEnd.Length / 2].map = genGoal;
            }
            if (j > 0)
            {
                for (int i = 0; i < allStages[j - 1].doorEnd.Length; i++)
                {
                    if (allStages[j - 1].doorEnd[i] != null)
                    {
                        allStages[j - 1].doorEnd[i].map = allStages[j];
                        for (int m = 1; m < 15; m++)
                        {
                            PathBridge path = Instantiate<PathBridge>(prefabPathBridge, allStages[j - 1].bridge[i]);
                            path.tf.localPosition = Vector3.up * (m * 0.5f + 0.3f) + Vector3.forward * (0.45f + 0.04f * m);
                        }
                    }
                }
            }
        }
        PlayerController mainPlayer = Instantiate(m_MainPlayer);
        mainPlayer.tf.position = Vector3.zero;
        Camera.main.GetComponent<CameraFollow>().followTransform = mainPlayer.tf;
        yield return null;
    }

    public CharacterInstance GetCharacterInstance(Collider other)
    {
        if (dict_col == null)
        {
            dict_col = new Dictionary<Collider, CharacterInstance>();
        }
        if (!dict_col.ContainsKey(other))
        {
            dict_col.Add(other, other.transform.GetComponent<CharacterInstance>());
        }
        return dict_col[other];
    }

}
