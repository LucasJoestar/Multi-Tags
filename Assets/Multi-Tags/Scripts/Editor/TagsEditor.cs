using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class TagsEditor : EditorWindow 
{
    /* TagsEditor :
	 *
	 *	#####################
	 *	###### PURPOSE ######
	 *	#####################
	 *
	 *	    Editor window used to add and remove tags.
	 *
	 *	#####################
	 *	### MODIFICATIONS ###
	 *	#####################
	 *
	 *	Date :			[20 / 01 / 2019]
	 *	Author :		[Guibert Lucas]
	 *
	 *	Changes :
	 *
	 *	    Creation of the TagsEditor class.
     *	    
     *	    Added the CallWindow method.
	 *
	 *	-----------------------------------
	*/

    #region Events

    #endregion

    #region Fields / Properties
    /// <summary>
    /// Path of the scriptable object from the resources folder, containing all tags of the project.
    /// </summary>
    [SerializeField] private const string tagsSOPath = "Tags/TagsResource";

    /// <summary>
    /// All tags of this project.
    /// </summary>
    public List<Tag> Tags = new List<Tag>();

    /// <summary>
    /// Get the scriptable object containing all this project tags.
    /// </summary>
    public TagsScriptableObject GetTagsAsset
    {
        get { return Resources.Load<TagsScriptableObject>(tagsSOPath); }
    }
    #endregion

    #region Methods

    #region Original Methods

    #region Editor
    /// <summary>
    /// Displays the tags of the project.
    /// </summary>
    private void DrawTags()
    {
        // Draws a header
        EditorGUILayout.LabelField("Tags", EditorStyles.boldLabel);

        // If no tags, draws a information box and return
        if (Tags == null || Tags.Count == 0)
        {
            EditorGUILayout.HelpBox("No tags found on this project !", MessageType.Warning);
            return;
        }

        Color _originalColor = GUI.color;
        GUIStyle _olMinus = new GUIStyle("OL Minus");
        GUIStyle _cnCountBadge = new GUIStyle("CN CountBadge");
        EditorGUILayout.BeginHorizontal();
        
        // Draws each tag with its associated color
        foreach (Tag _tag in Tags)
        {
            GUI.color = _tag.Color;

            Rect _labelRect = GUILayoutUtility.GetRect(new GUIContent(_tag.Name), EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            Rect _badgeRect = new Rect(_labelRect.position - new Vector2(0, 1), _labelRect.size + new Vector2(_olMinus.CalcSize(GUIContent.none).x, 0));

            // Creates the box for the whole tag area
            GUI.Box(_badgeRect, string.Empty, _cnCountBadge);

            // Button to remove the tag from the game object
            if (GUI.Button(new Rect(_badgeRect.position + new Vector2(0, 1), _olMinus.CalcSize(GUIContent.none)), GUIContent.none, _olMinus))
            {

            }

            // Label to display the tag name
            GUI.Label(new Rect(_labelRect.position + new Vector2(_olMinus.CalcSize(GUIContent.none).x, 0), _labelRect.size), _tag.Name, EditorStyles.boldLabel);

            GUILayout.Space(_olMinus.CalcSize(GUIContent.none).x);
        }

        EditorGUILayout.EndHorizontal();
        GUI.color = _originalColor;
    }

    /// <summary>
    /// Draws the multi-tags system editor.
    /// </summary>
    private void DrawTagsEditor()
    {
        // Draws a toolbar on the top of the window
        DrawToolbar();

        // Draw all project tags
        DrawTags();
    }

    /// <summary>
    /// Draws the toolbar on the top of the tags editor window.
    /// </summary>
    private void DrawToolbar()
    {
        GUILayout.BeginHorizontal(EditorStyles.toolbar);

        // Draws a button to create a brand new cool tag
        if (GUILayout.Button("Create Tag", EditorStyles.toolbarButton))
        {
            CreateTag();
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
    #endregion

    #region Tags
    /// <summary>
    /// Creates a new tag in the editor window.
    /// </summary>
    private void CreateTag()
    {

    }

    /// <summary>
    /// Creates the tags scriptable object used to save & load project tags
    /// </summary>
    private void CreateTagsAsset()
    {
        // Creates the scriptable object to write, and set all project tags on it
        TagsScriptableObject _tagsRef = CreateInstance<TagsScriptableObject>();
        MultiTagsUtility.GetTags().ToList().ForEach(t => _tagsRef.Tags.Add(new Tag(t)));

        // Creates the default folders & write the asset on disk
        Directory.CreateDirectory(Application.dataPath + "/Resources/" + Path.GetDirectoryName(tagsSOPath));
        AssetDatabase.CreateAsset(_tagsRef, "Assets/Resources/" + tagsSOPath + ".asset");
        AssetDatabase.SaveAssets();

        // Get the tags of the project
        Tags = _tagsRef.Tags;
    }
    #endregion

    #region Menu Navigation
    /// <summary>
    /// Opens the tags editor window.
    /// </summary>
    [MenuItem("Window/Tags")]
    public static void CallWindow()
    {
        GetWindow<TagsEditor>("Tags Editor").Show();
    }
    #endregion

    #endregion

    #region Unity Methods
    // This function is called when the scriptable object goes out of scope
    private void OnDisable()
    {
        
    }

    // This function is called when the object is loaded
    private void OnEnable()
    {
        // Get the scriptable object of the project containing all tags and load them ;
        // If not found, create it
        TagsScriptableObject _ref = GetTagsAsset;

        if (!_ref) CreateTagsAsset();
        else Tags = _ref.Tags;
    }

    // Implement your own editor GUI here
    private void OnGUI()
    {
        DrawTagsEditor();
    }
	#endregion

	#endregion
}
