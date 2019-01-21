using System;
using System.Collections;
using System.Collections.Generic;
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

    #endregion

    #region Methods

    #region Original Methods

    #region Menu Navigation
    /// <summary>
    /// Opens the tags editor window
    /// </summary>
    [MenuItem("Window/Tags")]
    public static void CallWindow()
    {
        GetWindow<TagsEditor>("Tags Editor").Show();
    }
    #endregion

    #endregion

    #region Unity Methods
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        
    }

    // This function is called when the scriptable object goes out of scope
    private void OnDisable()
    {
        
    }

    // This function is called when the object is loaded
    private void OnEnable()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
	}
	#endregion

	#endregion
}
