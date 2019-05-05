using UnityEngine;

public class ExempleScript : MonoBehaviour
{
    [SerializeField, Tooltip("Name of this exemple")] public new string name = "Multi-Tags Exemple";
    [SerializeField, Tooltip("Exemple first tag")] public Tag firstTag = null;
    [SerializeField, Tooltip("A little description")] public string description = "Test tag & tags property drawers";
    [SerializeField, Tooltip("Second tag, just to test")] public Tag secondTag = null;
    [SerializeField, Tooltip("Tags object for exemple")] public Tags allTags = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
