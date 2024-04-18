using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{
    public int oreCount;
    public int enemyCount;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject parent;
    [SerializeField] LadderHole ladder;
    [SerializeField] BoxCollider2D spawnArea;
    OreDropChanceSO dropChanceSO;
    List<OreVein> oreVeinList = new List<OreVein>();
    List<OreVein> activeOreVein = new List<OreVein>();
    List<Enemy> enemies =  new List<Enemy>();
    [SerializeField] Enemy enemyPrefab;

    Bounds bound;
    Vector3 RandomPointInBounds()
    {
        return new Vector3(
            Random.Range(bound.min.x, bound.max.x),
            Random.Range(bound.min.y, bound.max.y),
            0
        );
    }

    public Vector3 GetSpawnPoint()
    {
        return spawnPoint.position;
    }
    public Vector3 GetReturnPosition()
    {
        return ladder.GetReturnPosition();
    }

    public void SetOreDropChance(OreDropChanceSO dropChanceSO)
    {
        this.dropChanceSO = dropChanceSO;
    }
    private void Awake()
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
        //InitCave();
    }
    public void InitCave()
    {
        bound = spawnArea.bounds;
        spawnArea.enabled = false;
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i] != null)
            {
                Destroy(enemies[i]);
            }
        }
        enemies = new List<Enemy>();
        this.ladder.gameObject.SetActive(false);
        for (int i = 0; i < enemyCount; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, parent.transform);
            enemy.transform.position = spawnArea.transform.position + RandomPointInBounds();
            enemy.materialDrop = dropChanceSO.GetRandomizedMaterialDrop();
            enemies.Add(enemy);
        }

        foreach (OreVein ore in activeOreVein)
        {
            ore.gameObject.SetActive(false);
        }
        activeOreVein = new List<OreVein>();

        List<OreVein> tempList = new List<OreVein>(oreVeinList);
        bool ladder = true ;
        for(int i = 0; i < oreCount; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            OreVein tempVein = tempList[randomIndex];
            tempVein.gameObject.SetActive(true);
            activeOreVein.Add(tempVein);
            tempVein.Init(dropChanceSO.GetRandomizedMaterialDrop());
            if (ladder)
            {
                this.ladder.transform.position = tempVein.transform.position;
                tempVein.onBreak += () => { this.ladder.gameObject.SetActive(true); };
            }
            ladder = false;
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
