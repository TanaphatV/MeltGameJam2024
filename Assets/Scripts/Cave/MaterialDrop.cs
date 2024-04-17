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
    float speed;
    Vector3 velocity;
    bool goToUI = false;
    ResourceUIController resourceUIController;
    // Start is called before the first frame update
    void Start()
    {
        resourceUIController = FindObjectOfType<ResourceUIController>();
    }

    public void Init(MaterialSO material)
    {
        this.material = material;
        spriteRenderer.sprite = material.icon;
    }
    float startTime;
    // Update is called once per frame
    void Update()
    {
        if(goToUI)
        {
            Vector3 difference = resourceUIController.GetWorldPositionFromMaterialUI(material) - transform.position;
            Vector3 direction = difference.normalized;



            speed += acceleration * Time.deltaTime;
            velocity = Vector3.ClampMagnitude(direction * speed, maxSpeed);
            transform.position += velocity * Time.deltaTime;

            if(difference.magnitude <= 4)
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
                    startTime = Time.time;
                    goToUI = true;
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
