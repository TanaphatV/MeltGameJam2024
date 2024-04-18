using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{
    public int oreCount;
    public int enemyCount;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject ladderPrefab;
    [SerializeField] OreDropChanceSO dropChanceSO;
    List<OreVein> oreVeinList = new List<OreVein>();
    List<OreVein> activeOreVein = new List<OreVein>();
    List<Enemy> enemies =  new List<Enemy>();
    [SerializeField] Enemy enemyPrefab;

    private void Start()
    {
        foreach(Transform child in parent.transform)
        {
            OreVein temp = child.GetComponent<OreVein>();
            if (temp != null)
            {
                oreVeinList.Add(temp);
                temp.gameObject.SetActive(false);
            }
        }
        InitCave();
    }
    public void InitCave()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i] != null)
            {
                Destroy(enemies[i]);
            }
        }
        enemies = new List<Enemy>();
        for (int i = 0; i < enemyCount; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, parent.transform);
            enemy.materialDrop = dropChanceSO.GetRandomizedMaterialDrop();
            enemies.Add(enemy);
        }

        foreach (OreVein ore in activeOreVein)
        {
            ore.gameObject.SetActive(false);
        }
        activeOreVein = new List<OreVein>();

        List<OreVein> tempList = new List<OreVein>(oreVeinList);
        for(int i = 0; i < oreCount; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            OreVein tempVein = tempList[randomIndex];
            tempVein.gameObject.SetActive(true);
            activeOreVein.Add(tempVein);
            tempVein.Init(dropChanceSO.GetRandomizedMaterialDrop());
            tempList.RemoveAt(randomIndex);
        }

    }

    public void SetCaveActive(bool active)
    {
        parent.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
