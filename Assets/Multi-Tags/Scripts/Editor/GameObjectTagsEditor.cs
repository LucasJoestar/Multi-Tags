using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObject)), CanEditMultipleObjects]
public class GameObjectTagsEditor : Editor
{
    /* GameObjectTagsEditor :
	 *
	 *	#####################
	 *	###### PURPOSE ######
	 *	#####################
	 *
	 *	    Customizes the inspector of the GameObject class to add a section for a custom tag system.
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
     *	    Added the defaultEditor, isUnfolded & tagIcon fields.
     *	    Added the DrawTagSystem method.
     *	    Implemented OnDisable, OnEnable, OnHeaderGUI & OnInspectorGUI Unity methods.
	 *
	 *	-----------------------------------
	*/



    #region Events

    #endregion

    #region Fields / Properties
    /// <summary>
    /// Unity GameObject class built-in editor.
    /// </summary>
    private Editor defaultEditor;

    /// <summary>
    /// Is the tag sector visible or not ?
    /// </summary>
    private bool isUnfolded = true;

    /// <summary>
    /// Alls tags of this object.
    /// </summary>
    private List<Tag> objectTags = new List<Tag>();

    /// <summary>
    /// Tag icon to display in the inspector.
    /// </summary>
    private Texture2D tagIcon = null;
    #endregion

    #region Methods

    #region Original Methods
    /// <summary>
    /// Draws the custom tag system for game objects set in the <see cref="GameObjectTagsExtensions"/> script.
    /// </summary>
    private void DrawTagSystem()
    {
        EditorGUILayout.BeginHorizontal();

        // Title of the tag section
        EditorGUILayout.LabelField(new GUIContent("Tags", tagIcon), EditorStyles.boldLabel);

        // Creates a button at the top right of the inspector to open the tags editor window
        // First, get its rect, then draw it
        Rect _editButtonRect = GUILayoutUtility.GetRect(new GUIContent("Edit Tags"), EditorStyles.miniButtonRight, GUILayout.Width(100));

        if (GUI.Button(_editButtonRect, "Edit Tags", EditorStyles.miniButtonRight))
        {
            TagsEditorWindow.CallWindow();
        }

        EditorGUILayout.EndHorizontal();

        // Draws the tags of editing objets.
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
    }

    // This function is called when the object is loaded
    void OnEnable()
    {
        // When this inspector is created, also create the built-in inspector
        defaultEditor = CreateEditor(targets, Type.GetType("UnityEditor.GameObjectInspector, UnityEditor"));

        // Load the tag icon
        tagIcon = EditorGUIUtility.Load("Assets/Icons/Resources/tag.png") as Texture2D;

        // Get all tags of the object
        
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
        // Draw custom tag system
        DrawTagSystem();

        // Draw the default Inspector
        defaultEditor.OnInspectorGUI();
    }
	#endregion

	#endregion
}
