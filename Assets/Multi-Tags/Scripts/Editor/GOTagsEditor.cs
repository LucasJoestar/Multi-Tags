using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObject)), CanEditMultipleObjects]
public class GOTagsEditor : Editor
{
    /* GOTagsEditor :
	 *
	 *	#####################
	 *	###### PURPOSE ######
	 *	#####################
	 *
	 *	    Customizes the inspector of the GameObject class to add a section for the
     *	custom multi-tags system at the top of the components, just below the header.
	 *
	 *	#####################
	 *	### MODIFICATIONS ###
	 *	#####################
	 *
     *	Date :			[22 / 02 / 2019]
	 *	Author :		[Guibert Lucas]
	 *
	 *	Changes :
	 *
     *      ???
	 *
	 *	-----------------------------------
     * 
	 *	Date :			[20 / 01 / 2019]
	 *	Author :		[Guibert Lucas]
	 *
	 *	Changes :
	 *
     *      Creation of the GameObjectTagsEditor.
     * 
	 *	    Researches. A lot, of researches.
     *	    
     *	    Finally, get to know how to draw it, thanks to these guys :
     *	            - https://forum.unity.com/threads/custom-inspector-default-header-for-scriptableobjects.544276/
     *	            - https://forum.unity.com/threads/extending-instead-of-replacing-built-in-inspectors.407612/
     *	            
     *	    So, to do it we create an editor of the GameObjectInspector type.
     *	But warning ! Got a lot of errors when calling the OnDisable method of the editor, so removed it ;
     *	Got also sometime an error when destroying the editor, so maybe this will be removed.
     *	
     *	    To get all tags of the project, use : "UnityEditorInternal.InternalEditorUtility.tags".
     *	    
     *	    Created a simple editor with... almost nothing it it.
     *	Added a header for Tags at the top left and a button at the top right of the editor
     *	to open the Tags Editor Window.
	 *
	 *	-----------------------------------
	*/

    #region Fields / Properties

    #region Editor Variables
    /// <summary>
    /// Unity GameObject class built-in editor.
    /// </summary>
    private Editor defaultEditor;

    /// <summary>
    /// All editing game objects.
    /// </summary>
    private GameObject[] targetGO = null;

    /// <summary>
    /// Is the tag editor visible or not ?
    /// </summary>
    private bool isUnfolded = true;
    #endregion

    #region Target Script(s) Variables
    /// <summary>
    /// Alls tags of this object.
    /// </summary>
    private List<Tag> objectTags = new List<Tag>();
    #endregion

    #endregion

    #region Methods

    #region Original Methods
    /// <summary>
    /// Draws an editor for the custom tag system for the GameObject class.
    /// </summary>
    private void DrawTagSystem()
    {
        EditorGUILayout.BeginHorizontal();

        // Title of the tag section
        EditorGUILayout.LabelField(new GUIContent("Tags"), EditorStyles.boldLabel);

        // Creates a button at the top right of the inspector to open the tags editor window
        // First, get its rect, then draw it
        Rect _editButtonRect = GUILayoutUtility.GetRect(new GUIContent("Edit Tags"), EditorStyles.miniButtonRight, GUILayout.Width(100));

        if (GUI.Button(_editButtonRect, "Edit Tags", EditorStyles.miniButtonRight))
        {
            TagsEditorWindow.CallWindow();
        }

        EditorGUILayout.EndHorizontal();

        // Draws the tags of editing objects.
        if (isUnfolded)
        {

        }

        // Males a space in the editor to finish
        EditorGUILayout.Space();
    }
    #endregion

    #region Unity Methods
    // This function is called when the scriptable object goes out of scope
    void OnDisable()
    {
        // When OnDisable is called, the default editor we created should be destroyed to avoid memory leakage.
        if (defaultEditor)
        {
            //Debug.Log("Destroy editor");
            //DestroyImmediate(defaultEditor);
        }

        //Also, make sure to call any required methods like OnDisable

        //MethodInfo disableMethod = defaultEditor.GetType().GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        //disableMethod?.Invoke(defaultEditor, null);

        Debug.Log("Editor => Disable...");
    }

    // This function is called when the object is loaded
    void OnEnable()
    {
        // Get editing objects as game objects
        targetGO = new GameObject[targets.Length];

        for (int _i = 0; _i < targets.Length; _i++)
        {
            targetGO[_i] = (GameObject)targets[_i];
        }

        // When this inspector is created, also create the built-in inspector
        defaultEditor = CreateEditor(targets, Type.GetType("UnityEditor.GameObjectInspector, UnityEditor"));

        /* Get all tags of the editing object(s)
         * 
         * If editing only one object, get all its tags ;
         * If performing multi-editing, get all tags in common between them
        */
        if (serializedObject.isEditingMultipleObjects)
        {
            // Get tags in common
        }
        else
        {
            // Get object tags
            //objectTags = targetGO[0].GetTags();
        }

        Debug.Log("Editor => Enable !");
    }

    // Implement this function to make a custom header
    protected override void OnHeaderGUI()
    {
        // Draw the default header
        defaultEditor.DrawHeader();
    }

    // Implement this function to make a custom inspector
    public override void OnInspectorGUI()
    {
        // Draws the custom multi-tags system, and then below it the default GameObject inspector
        DrawTagSystem();
        defaultEditor.OnInspectorGUI();
    }
	#endregion

	#endregion
}
