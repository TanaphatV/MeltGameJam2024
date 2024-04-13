using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //[SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += velocity * Time.deltaTime;
    }
    private void Update()
    {
        UserInput();
    }
    void UserInput()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        velocity = input * speed;
    }
    private void OnDrawGizmos()
    {
        
    }
}
