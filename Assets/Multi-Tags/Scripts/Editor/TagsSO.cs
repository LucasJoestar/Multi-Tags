using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TagsSO : ScriptableObject 
{
    /* TagsSO :
	 *
	 *	#####################
	 *	###### PURPOSE ######
	 *	#####################
	 *
	 *	    Scriptable object used to store all tags of a project, allowing the save & load them dynamically.
	 *
     *	#####################
	 *	####### TO DO #######
	 *	#####################
     * 
     *      - Get newly created tags using the Untiy System, so basically the same
     *  that does the current Initialize method. Got to try with a coroutine doing things
     *  one after the other with a minimum stuff by frame.
     *  
     *      ... Well, I think that's all I got to do.
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
     *	    Now, the tags are fully loaded & set when this scriptable object is being loaded. That's really sweet.
     *	    You can copy this scriptable object from project to project, and all tags will be fully loaded. That's cool.
     *	    Oh, and the tags of the project not yet on this object are automatically added on load. Awesome, isn't it ?
	 *
	 *	-----------------------------------
     * 
	 *	Date :			[21 / 01 / 2019]
	 *	Author :		[Guibert Lucas]
	 *
	 *	Changes :
	 *
	 *	    Creation of the TagsScriptableObject class.
     *	    
     *	    Base content, with only a list of Tag objects.
	 *
	 *	-----------------------------------
	*/

    #region Fields / Properties
    /// <summary>
    /// All custom tags of this project.
    /// </summary>
    public List<Tag> Tags = new List<Tag>();

    /// <summary>
    /// All Unity built-in tags of this project.
    /// </summary>
    public List<Tag> UnityBuiltInTags = new List<Tag>();

    /// <summary>
    /// Name of all Unity built-in tags.
    /// </summary>
    public readonly string[] BuiltInTagsNames = new string[7] { "Untagged", "Respawn", "Finish", "EditorOnly", "MainCamera", "Player", "GameController" };
    #endregion

    #region Methods

    #region Original Methods
    /// <summary>
    /// Initializes this scriptable object with the project tags, and adds all tags this object contains to the project if not having them yet.
    /// </summary>
    public void Initialize()
    {
        List<string> _projectTags = MultiTagsUtility.GetTags().ToList();
        string[] _referenceTags = Tags.Select(t => t.Name).ToArray();

        // Adds each tag of this scriptable object to the project in not having thel yet
        foreach (string _tag in _referenceTags)
        {
            if (!_projectTags.Contains(_tag))
            {
                MultiTagsUtility.AddTag(_tag);
            }
            else
            {
                _projectTags.Remove(_tag);
            }
        }

        // Adds all tags of this project this object doesn't have to it
        foreach (string _tag in _projectTags)
        {
            if (BuiltInTagsNames.Contains(_tag)) UnityBuiltInTags.Add(new Tag(_tag));
            else Tags.Add(new Tag(_tag));
        }
    }
    #endregion

    #region Unity Methods
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Initialize();
    }
	#endregion

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
