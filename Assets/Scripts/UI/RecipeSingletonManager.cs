using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSingletonManager : MonoBehaviour
{
    public static RecipeSingletonManager Instance { get; private set; }

    [SerializeField] private ResourceSO resource;

    public ResourceSO GetResource => resource; 

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy GameManager when loading new scenes
        }
        else
        {
            Destroy(gameObject); // If another GameManager already exists, destroy this one
        }
    }
}
