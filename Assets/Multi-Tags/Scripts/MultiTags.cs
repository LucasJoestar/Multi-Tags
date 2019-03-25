using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Class to access multiple methods around to multi-tags system not using editor.
/// </summary>
[Serializable]
public static class MultiTags 
{
    /* MultiTags :
	 *
	 *	#####################
	 *	###### PURPOSE ######
	 *	#####################
	 *
	 *	Script to reference everything needed from the Multi-Tags system in a static class.
	 *
	 *	#####################
	 *	####### TO DO #######
	 *	#####################
	 *
	 *	    • Get all project tags at runtime, wouldn't it be nice ?
     *	    
     *      • Dynamically add or remove tags to the game in runtime ? That would be cool.
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
	 *	Creation of the MultiTags class.
     *	
     *	    • Added a Char variable used to separate tags witht he multi-tags system
     *	from the Unity one.
	 *
	 *	-----------------------------------
	*/

    #region Fields / Properties
    /// <summary>
    /// Path of the scriptable object from the resources folder, containing all tags of the project.
    /// </summary>
    public const string TAGS_SO_PATH = "Tags/TagsResource";

    /// <summary>
    /// Name of all Unity built-in tags.
    /// </summary>
    public static readonly string[] BuiltInTagsNames = new string[7] { "Untagged", "Respawn", "Finish", "EditorOnly", "MainCamera", "Player", "GameController" };

    /// <summary>
    /// Separator used to separate tags from the original Unity tag system.
    /// </summary>
    public static char TagSeparator = '|';

    /// <summary>
    /// Get the scriptable object containing all this project tags.
    /// </summary>
    public static TagsSO GetTagsAsset
    {
        get { return Resources.Load<TagsSO>(TAGS_SO_PATH); }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Returns the first game object found having a certain tag.
    /// </summary>
    /// <param name="_tag">Tag to search for.</param>
    /// <returns>Returns first game object found having the tag, or null if none.</returns>
    public static GameObject FindObjectWithTag(string _tag) { return Object.FindObjectsOfType<GameObject>().Where(g => g.GetTags().Contains(_tag)).FirstOrDefault(); }
    #endregion
}

[Serializable]
public class Tag
{
    /* Tag :
     *
     *	#####################
     *	###### PURPOSE ######
     *	#####################
     *
     *	    Class used to store a tag, with all its informations.
     *
     *	#####################
     *	### MODIFICATIONS ###
     *	#####################
     *
     *	Date :			[21 / 01 / 2019]
     *	Author :		[Guibert Lucas]
     *
     *	Changes :
     *
     *	    Creation of the Tag class.
     *	    
     *	    This class Tag contain a name & an associated color.
     *
     *	-----------------------------------
    */

    #region Fields / Properties
    /// <summary>
    /// Name of this tag.
    /// </summary>
    public string Name = "New Tag";

    /// <summary>
    /// Color of this tag, used to render it in the editor.
    /// </summary>
    public Color Color = Color.white;
    #endregion

    #region Constructor
    /// <summary>
    /// Creates a new tag.
    /// </summary>
    /// <param name="_name">Name of the newly created tag.</param>
    public Tag(string _name)
    {
        Name = _name;
    }

    /// <summary>
    /// Creates a new tag.
    /// </summary>
    /// <param name="_name">Name of the newly created tag.</param>
    /// <param name="_color">Color of the tag, used to display it in the inspector.</param>
    public Tag(string _name, Color _color)
    {
        Name = _name;
        Color = _color;
    }
    #endregion
}
