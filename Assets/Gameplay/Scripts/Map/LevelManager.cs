using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public ItemInstance prefabItem;
    public EnemyController prefabEnemy;
    public PlayerController m_MainPlayer;
    private Dictionary<Collider, CharacterInstance> dict_col;
    private GameObject currentLevel;
    private LevelSettings currentLevelSettings;
    public LevelSettings Settings { get { return currentLevelSettings; } }
    private MapManager[] allStages;
    private List<EnemyController> allEnemy;

    public void StartLevel(int level)
    {
        allEnemy = new List<EnemyController>();
        StartCoroutine(LoadLevel(level));
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
        currentLevel = Instantiate(Resources.Load<GameObject>("Level/Levels_" + level.ToString()), transform);
        allStages = currentLevel.GetComponentsInChildren<MapManager>();
        if(allStages.Length > 0)
        {
            for (int i = 0; i < currentLevelSettings.enemyColors.Count; i++) {
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
        
        yield return new WaitForSeconds(5f);
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
