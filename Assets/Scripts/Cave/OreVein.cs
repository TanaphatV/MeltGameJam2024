using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OreVein : MonoBehaviour, IHitable
{
    [SerializeField] MaterialContainer materialContainer;
    [SerializeField] int hitsNeeded;
    [SerializeField] float dropRadius;
    [SerializeField] MaterialDrop materialDropPrefab;
    int hitLeft;

    public UnityAction onBreak;

    public void Hit()
    {
        hitLeft -= PlayerStats.instance.mining;
        if (hitLeft <= 0)
            Break();
    }

    public void Init(MaterialContainer materialContainer)
    {
        hitLeft = hitsNeeded;
        this.materialContainer = materialContainer;
        onBreak = null;
    }



    void Break()
    {
        for(int i = 0; i < materialContainer.amount; i++)
        {
            MaterialDrop temp = Instantiate(materialDropPrefab);
            temp.gameObject.transform.position = transform.position;
            temp.Init(materialContainer.material, Random.insideUnitCircle * dropRadius);
        }
        if (onBreak != null)
            onBreak();
        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dropRadius);
    }
}
