using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDrop : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField]MaterialSO material;
    Vector3 velocity;
    RectTransform target = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(MaterialSO material)
    {
        this.material = material;
        spriteRenderer.sprite = material.icon;
    }

    // Update is called once per frame
    void Update()
    {
        if(target !=null)
        {
            

            Vector3 difference = target.TransformPoint(target.rect.center) - transform.position;
            Vector3 direction = difference.normalized;
            velocity += direction * acceleration * Time.deltaTime;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            transform.position += velocity * Time.deltaTime;
            Debug.Log(difference.magnitude);
            if(difference.magnitude <= 1)
            {
                PlayerResources.instance.AddMaterial(material,1);
                Destroy(gameObject);
            }
        }
        else
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (var col in cols)
            {
                if (col.CompareTag("Player"))
                {
                    target = FindAnyObjectByType<ResourceUIController>().GetMaterialUIRectTransform(material);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
