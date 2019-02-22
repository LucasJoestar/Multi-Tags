using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class TagsEditorWindow : EditorWindow 
{
    /* TagsEditorWindow :
	 *
	 *	#####################
	 *	###### PURPOSE ######
	 *	#####################
	 *
	 *	    Editor window used to add, remove and personalize project tags.
	 *
     *	#####################
	 *	####### TO DO #######
	 *	#####################
     * 
     *      - Display in a cool way all project tags. For now, they are all in a single line,
     *  but got to find a way to go to the next line when reaching the end of the screen.
     *  Maybe with non-layout GUI...
     *  
     *      - Change the color of existing tags. That should be easy.
     *      
     *      - Change the name of created tags. That should be tricky.
     * 
	 *	#####################
	 *	### MODIFICATIONS ###
	 *	#####################
	 *
     *	Date :			[03 / 02 / 2019]
	 *	Author :		[Guibert Lucas]
	 *
	 *	Changes :
	 *
     *	    Finally, tags can be created & removed from this window, and everything is fully
     *	saved on a scriptable object reference. Pretty cool, it is.
     *	    Still, tags are not displayed the way I want to, so got to find a way to display them
     *	horizontally and automatically go to the line when reaching the end of the screen.
     *	    Hard, it is. When trying to check this by rect size, 'got a weird error 'cause events
     *	Layout & Repaint have different informations.
     *	
     *	    At worst, can try with non-layout GUI.
	 *
     *	-----------------------------------
     * 
     *	Date :			[30 / 01 / 2019]
	 *	Author :		[Guibert Lucas]
	 *
	 *	Changes :
	 *
	 *	    The window loads & write dynamically project tags on a scriptable object in the
     *	Resources folder. Plus, all of them are drawn on the window. Pretty cool.
     * 
	 *	-----------------------------------
     * 
	 *	Date :			[20 / 01 / 2019]
	 *	Author :		[Guibert Lucas]
	 *
	 *	Changes :
	 *
	 *	Creation of the TagsEditorWindow class.
     *	    
     *	    The window can now be called from a Menu Toolbar button, and... That's it.
     *	Yeah, I know...
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

    /// <summary>Shorthand to access reference <see cref="TagsSO.Tags"/>.</summary>
    public List<Tag> Tags
    {
        get { return Reference.Tags; }
    }

    /// <summary>Shorthand to access reference <see cref="TagsSO.UnityBuiltInTags"/>.</summary>
    public List<Tag> UnityBuiltInTags
    {
        get { return Reference.UnityBuiltInTags; }
    }

    /// <summary>Backing field for <see cref="Reference"/>.</summary>
    private TagsSO reference = null;

    /// <summary>
    /// Scriptable object containing this project tags informations.
    /// </summary>
    public TagsSO Reference
    {
        get
        {
            if (!reference) CreateTagsAsset();
            return reference;
        }
        private set { reference = value; }
    }

    /// <summary>
    /// Get the scriptable object containing all this project tags.
    /// </summary>
    public TagsSO GetTagsAsset
    {
        get { return Resources.Load<TagsSO>(tagsSOPath); }
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
        // Creates & get variables
        Color _originalColor = GUI.color;
        Color _originalContentColor = GUI.contentColor;
        GUIStyle _olMinus = new GUIStyle("OL Minus");
        GUIStyle _cnCountBadge = new GUIStyle("CN CountBadge");
        Vector2 _olMinusCaclSize = _olMinus.CalcSize(GUIContent.none);

        // Draws a header
        EditorGUILayout.LabelField("Unity built-in Tags", EditorStyles.boldLabel);

        // If no tags, draws a information box and return
        if (UnityBuiltInTags == null || UnityBuiltInTags.Count == 0)
        {
            EditorGUILayout.HelpBox("No built-in tag found on this project", MessageType.Info);
        }
        else
        {
            EditorGUILayout.BeginHorizontal();

            // Draws Unity built-in tags
            foreach (Tag _tag in UnityBuiltInTags)
            {
                GUI.color = _tag.Color;
                if (GUI.color.grayscale > .5f) GUI.contentColor = Color.white;
                else GUI.contentColor = Color.black;

                Rect _badgeRect = GUILayoutUtility.GetRect(new GUIContent(_tag.Name), EditorStyles.boldLabel, GUILayout.ExpandWidth(false));

                if (_badgeRect.xMax > Screen.width && _badgeRect.xMin > 15)
                {
                    //EditorGUILayout.BeginHorizontal();
                    //EditorGUILayout.EndHorizontal();
                }

                // Creates the box for the whole tag area
                GUI.Box(new Rect(_badgeRect.x, _badgeRect.y - 1, _badgeRect.width + 2, _badgeRect.height), string.Empty, _cnCountBadge);

                // Label to display the tag name
                GUI.Label(new Rect(_badgeRect.x + 1, _badgeRect.y, _badgeRect.width, _badgeRect.height), _tag.Name, EditorStyles.boldLabel);
            }

            EditorGUILayout.EndHorizontal();
        }

        // Draws a header
        EditorGUILayout.LabelField("Custom Tags", EditorStyles.boldLabel);

        // If no tags, draws a information box and return
        if (Tags == null || Tags.Count == 0)
        {
            GUI.color = _originalColor;
            GUI.contentColor = _originalContentColor;
            EditorGUILayout.HelpBox("No tag found on this project. How about create a first one ?", MessageType.Info);
            return;
        }

        EditorGUILayout.BeginHorizontal();

        // Draws each tag with its associated color
        foreach (Tag _tag in Tags)
        {
            GUI.color = _tag.Color;
            if (GUI.color.grayscale > .5f) GUI.contentColor = Color.white;
            else GUI.contentColor = Color.black;

            Rect _labelRect = GUILayoutUtility.GetRect(new GUIContent(_tag.Name), EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            Rect _badgeRect = new Rect(_labelRect.position, _labelRect.size + new Vector2(_olMinusCaclSize.x, 0));

            // Creates the box for the whole tag area
            GUI.Box(new Rect(_badgeRect.x, _badgeRect.y - 1, _badgeRect.width, _badgeRect.height), string.Empty, _cnCountBadge);

            // Button to remove the tag from the game object
            if (GUI.Button(new Rect(_badgeRect.position, _olMinusCaclSize), GUIContent.none, _olMinus))
            {
                RemoveTag(_tag);
                Repaint();
                return;
            }

            // Label to display the tag name
            GUI.Label(new Rect(_labelRect.position + new Vector2(_olMinusCaclSize.x - 2, 0), _labelRect.size), _tag.Name, EditorStyles.boldLabel);

            GUILayout.Space(_olMinusCaclSize.x);
        }

        GUI.color = _originalColor;
        GUI.contentColor = _originalContentColor;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("This is the End...");
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
            GetWindow<CreateTagWindow>("Create new Tag").Show();
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
    #endregion

    #region Tags
    /// <summary>
    /// Creates a brand new tag and add it to the project.
    /// </summary>
    /// <param name="_tag">New tag to add.</param>
    public void CreateTag(Tag _tag)
    {
        MultiTagsUtility.AddTag(_tag.Name);
        Tags.Add(_tag);
    }

    /// <summary>
    /// Creates the tags scriptable object used to save & load project tags.
    /// </summary>
    private void CreateTagsAsset()
    {
        // Creates the scriptable object to write
        reference = CreateInstance<TagsSO>();

        // Creates the default folders & write the asset on disk
        Directory.CreateDirectory(Application.dataPath + "/Resources/" + Path.GetDirectoryName(tagsSOPath));
        AssetDatabase.CreateAsset(reference, "Assets/Resources/" + tagsSOPath + ".asset");
        AssetDatabase.SaveAssets();
    }

    /// <summary>
    /// Removes a tag from the project.
    /// </summary>
    /// <param name="_tag">Tag to remove</param>
    public void RemoveTag(Tag _tag)
    {
        MultiTagsUtility.RemoveTag(_tag.Name);
        Tags.Remove(_tag);
    }
    #endregion

    #region Menu Navigation
    /// <summary>
    /// Opens the tags editor window.
    /// </summary>
    [MenuItem("Window/Tags")]
    public static TagsEditorWindow CallWindow()
    {
        TagsEditorWindow _window = GetWindow<TagsEditorWindow>("Tags Editor");
        _window.Show();
        return _window;
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
        reference = GetTagsAsset;
        if (!reference) CreateTagsAsset();
    }

    // Implement your own editor GUI here
    private void OnGUI()
    {
        DrawTagsEditor();
    }
	#endregion

	#endregion
}

public class CreateTagWindow : EditorWindow
{
    /* CreateTagWindow :
     *
     *	#####################
     *	###### PURPOSE ######
     *	#####################
     *
     *	    Editor window used to create a new tag.
     *
     *	#####################
     *	### MODIFICATIONS ###
     *	#####################
     *
     *	Date :			[03 / 02 / 2019]
     *	Author :		[Guibert Lucas]
     *
     *	Changes :
     *
     *	Creation of the CreateTagWindow class.
     *	    
     *	    This is a little cool window that allow to create a new tag.
     *	If the tag cannot be created, a little message indicates it to the user.
     *	When created, the window closes by itself. And, it works fine.
     *
     *	-----------------------------------
    */

    #region Fields & Properties
    /// <summary>
    /// Name of the new tag to create.
    /// </summary>
    private string tagName = "New Tag";

    /// <summary>
    /// Color of the new tag to create.
    /// </summary>
    private Color color = Color.white;

    /// <summary>
    /// Indicates if the name entered is empty.
    /// </summary>
    private bool isNameEmpty = false;

    /// <summary>
    /// Indicates if a tag with the same name already exist or not.
    /// </summary>
    private bool doesNameAlreadyExist = false;

    /// <summary>
    /// Indicates if the name entered contains the tag separator or not.
    /// </summary>
    private bool doesNameContainSeparator = false;
    #endregion

    #region Methods

    #region Original Method
    /// <summary>
    /// Draws this editor window content.
    /// </summary>
    private void DrawEditor()
    {
        // If the name contains the tag separator, indicate it
        if (doesNameContainSeparator)
        {
            EditorGUILayout.HelpBox("The name of the tag cannot contains \'" + MultiTags.TagSeparator + "\'", MessageType.Error);
        }
        // If the name is not valid, indicate it
        else if (doesNameAlreadyExist)
        {
            EditorGUILayout.HelpBox("A tag with the same name already exist", MessageType.Error);
        }
        // If the name is empty, indicate it
        else if (isNameEmpty)
        {
            EditorGUILayout.HelpBox("You must enter a name", MessageType.Error);
        }

        EditorGUILayout.BeginHorizontal();

        // Set the name & color of the new tag
        EditorGUILayout.LabelField(new GUIContent("Name :", "Name of the new tag to create"), GUILayout.Width(50));
        tagName = EditorGUILayout.TextField(tagName, GUILayout.Width(100));
        EditorGUILayout.LabelField(new GUIContent("Color :", "Color of the new tag to create"), GUILayout.Width(50));
        color = EditorGUILayout.ColorField(color, GUILayout.Width(100));

        EditorGUILayout.EndHorizontal();
        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Create", EditorStyles.miniButton, GUILayout.ExpandWidth(false)))
        {
            // If the new tag name entered contains the tag separator, indicate it and refuse to create the tag
            if (tagName.Contains(MultiTags.TagSeparator))
            {
                doesNameContainSeparator = true;
                doesNameAlreadyExist = false;
                isNameEmpty = false;

                SetBigSize();
                Repaint();
                return;
            }
            else if (doesNameContainSeparator)
            {
                doesNameContainSeparator = false;
                SetSmallSize();
                Repaint();
            }

            // If a tag with the same name already exist, indicate it and refuse to create the tag
            if (MultiTagsUtility.GetUnityTags().Contains(tagName))
            {
                doesNameAlreadyExist = true;
                isNameEmpty = false;

                SetBigSize();
                Repaint();
                return;
            }
            else if (doesNameAlreadyExist)
            {
                doesNameAlreadyExist = false;
                SetSmallSize();
                Repaint();
            }

            // If the name entered is empty, indicate it and refuse to create the tag
            if (string.IsNullOrEmpty(tagName))
            {
                isNameEmpty = true;

                SetBigSize();
                Repaint();
                return;
            }

            // If everything is okay, create the new tag
            TagsEditorWindow.CallWindow().CreateTag(new Tag(tagName, color));
            Close();
        }
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Set the window size as "big".
    /// </summary>
    private void SetBigSize()
    {
        Vector2 _size = new Vector2(300, 95);
        minSize = _size;
        maxSize = _size;
    }

    /// <summary>
    /// Set the window size as "small".
    /// </summary>
    private void SetSmallSize()
    {
        Vector2 _size = new Vector2(300, 50);
        minSize = _size;
        maxSize = _size;
    }
    #endregion

    #region Unity Methods
    // This function is called when the object is loaded
    private void OnEnable()
    {
        SetSmallSize();
        ShowUtility();
    }

    // Implement your own editor GUI here
    private void OnGUI()
    {
        DrawEditor();
    }
    #endregion

    #endregion
}
