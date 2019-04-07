using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExempleScript : MonoBehaviour
{
    [SerializeField, Tooltip("Name of this exemple")] string name = "Multi-Tags Exemple";
    [SerializeField, Tooltip("Exemple first tag")] Tag firstTag = null;
    [SerializeField, Tooltip("A little description")] string description = "Test tag & tags property drawers";
    [SerializeField, Tooltip("Second tag, just to test")] Tag secondTag = null;
    [SerializeField, Tooltip("Tags object for exemple")] Tags allTags = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
