using System;
using System.Collections.Generic;
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
     *	Date :			[30 / 03 / 2019]
	 *	Author :		[Guibert Lucas]
	 *
	 *	Changes :
	 *
	 *	    • Added multiple methods to get tag(s) from string(s) and to check if tag(s) exist.
	 *
	 *	-----------------------------------
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
    /// Separator used to separate tags from the original Unity tag system.
    /// </summary>
    public const char                   TAG_SEPARATOR               = '|';

    /// <summary>
    /// Path of the scriptable object from the resources folder, containing all tags of the project.
    /// </summary>
    public const string                 TAGS_SO_PATH                = "Tags/TagsResource";


    /// <summary>
    /// Name of all Unity built-in tags.
    /// </summary>
    public static readonly string[]     BuiltInTagsNames            = new string[7] { "Untagged", "Respawn", "Finish", "EditorOnly", "MainCamera", "Player", "GameController" };

    /// <summary>
    /// All this project tags.
    /// </summary>
    public static Tag[]                 AllTags                     { get { return TagsAsset.AllTags; } }

    /// <summary>
    /// The scriptable object containing all this project tags. Null if none.
    /// </summary>
    private static TagsSO               TagsAsset                   { get { return Resources.Load<TagsSO>(TAGS_SO_PATH); } }
    #endregion

    #region Methods
    /// <summary>
    /// Get if a specified tag exist.
    /// </summary>
    /// <param name="_tag">Tag to check existence.</param>
    /// <returns>Returns true if it exist, false otherwise.</returns>
    public static bool DoesTagExist(Tag _tag)
    {
        return AllTags.Contains(_tag);
    }

    /// <summary>
    /// Get existing tags from a tag array.
    /// </summary>
    /// <param name="_tags">Tags to check existence.</param>
    /// <returns>Returns existing tags from the tag array.</returns>
    public static Tag[] ExistingTags(Tag[] _tags)
    {
        return AllTags.Where(t => _tags.Contains(t)).ToArray();
    }


    /// <summary>
    /// Get a Tag object from a given tag name.
    /// </summary>
    /// <param name="_tagName">Tag name to get Tag object from.</param>
    /// <returns>Returns the Tag object from this tag name.</returns>
    public static Tag GetTag(string _tagName)
    {
        return Array.Find(AllTags, (Tag _t) => _t.Name == _tagName);
    }

    /// <summary>
    /// Get all Tag objects from given tags name.
    /// </summary>
    /// <param name="_tagsName">Tags name to get Tags object from.</param>
    /// <returns>Returns the Tags object from these tags name.</returns>
    public static Tags GetTags(string[] _tagsName)
    {
        return new Tags(Array.FindAll(AllTags, (Tag _t) => _tagsName.Contains(_t.Name)));
    }


    /// <summary>
    /// Returns the first game object found having a certain tag.
    /// </summary>
    /// <param name="_tag">Tag to search for.</param>
    /// <returns>Returns first game object found having the tag, or null if none.</returns>
    public static GameObject FindObjectWithTag(string _tag)
    {
        return Object.FindObjectsOfType<GameObject>().Where(g => g.GetTags().Contains(_tag)).FirstOrDefault();
    }
    #endregion
}

/// <summary>
/// Object referencing a tag, in the multi-tags system.
/// </summary>
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
    public string       Name        = "New Tag";

    /// <summary>
    /// Color of this tag, used to render it in the editor.
    /// </summary>
    public Color        Color       = Color.white;
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

/// <summary>
/// Object referencing multiple tags, in the multi-tags system.
/// </summary>
[Serializable]
public class Tags
{
    /* Tags :
     *
     *	#####################
     *	###### PURPOSE ######
     *	#####################
     *
     *	    Class used to store multiple tags, with all their informations.
     *
     *	#####################
     *	### MODIFICATIONS ###
     *	#####################
     *
     *	Date :			[30 / 03 / 2019]
     *	Author :		[Guibert Lucas]
     *
     *	Changes :
     *
     *	    Creation of the Tags class.
     *	    
     *	    This class Tags contains multiple tags. With its property drawer and methods to add / remove tags, it is more convenient to use than
     *	a simple list of tags.
     *
     *	-----------------------------------
    */

    #region Fields / Properties
    /// <summary>
    /// All tags of this object.
    /// </summary>
    public Tag[] ObjectTags
    {
        get; private set;
    }

    /// <summary>
    /// Name of all this object tags.
    /// </summary>
    public string[] TagsName
    {
        get { return ObjectTags.Select(t => t.Name).ToArray(); }
    }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new Tags object empty.
    /// </summary>
    public Tags()
    {
        ObjectTags = new Tag[] { };
    }

    /// <summary>
    /// Creates a new Tags object empty.
    /// </summary>
    /// <param name="_tags">Tags to initialize this object with.</param>
    public Tags(IEnumerable<Tag> _tags)
    {
        ObjectTags = _tags.ToArray();
    }
    #endregion

    #region Methods

    #region Add
    /// <summary>
    /// Adds a new tag to the object.
    /// </summary>
    /// <param name="_tag">Name of the tag to add.</param>
    public void AddTag(string _tag)
    {
        Tag _toAdd = MultiTags.GetTag(_tag) ?? throw new NullReferenceException("The specified tag does not exist.");
        AddTag(_toAdd, true);
    }

    /// <summary>
    /// Adds a new tag to the object.
    /// </summary>
    /// <param name="_tag">Tag to add.</param>
    public void AddTag(Tag _tag)
    {
        AddTag(_tag, false);
    }

    /// <summary>
    /// Adds a new tag to the object.
    /// </summary>
    /// <param name="_tag">Tag to add.</param>
    /// <param name="_doExist">Does the tag exist, or should it be checked before add.</param>
    private void AddTag(Tag _tag, bool _doExist)
    {
        if (!_doExist && !MultiTags.DoesTagExist(_tag)) throw new NullReferenceException("The specified tag does not exist.");

        Tag[] _newTags = new Tag[ObjectTags.Length + 1];

        for (int _i = 0; _i < ObjectTags.Length; _i++)
        {
            _newTags[_i] = ObjectTags[_i];
        }

        _newTags[_newTags.Length] = _tag;

        ObjectTags = _newTags;
    }

    /// <summary>
    /// Adds multiple new tags to the object.
    /// </summary>
    /// <param name="_tags">Name of the tags to add.</param>
    public void AddTags(string[] _tags)
    {
        Tag[] _toAdd = MultiTags.GetTags(_tags).ObjectTags ?? throw new NullReferenceException("None of the specified tags does exist.");
        AddTags(_toAdd, true);
    }

    /// <summary>
    /// Adds multiple new tags to the object.
    /// </summary>
    /// <param name="_tags">Tags to add.</param>
    public void AddTags(Tag[] _tags)
    {
        AddTags(_tags, false);
    }

    /// <summary>
    /// Adds multiple new tags to the object.
    /// </summary>
    /// <param name="_tags">Tags to add.</param>
    /// <param name="_doExist">Does the tags exist, or should them be checked before add.</param>
    private void AddTags(Tag[] _tags, bool _doExist)
    {
        if (!_doExist) _tags = MultiTags.ExistingTags(_tags) ?? throw new NullReferenceException("None of the specified tags does exist.");

        Tag[] _newTags = new Tag[ObjectTags.Length + _tags.Length];

        for (int _i = 0; _i < ObjectTags.Length; _i++)
        {
            _newTags[_i] = ObjectTags[_i];
        }

        for (int _i = 1; _i <= _tags.Length; _i++)
        {
            _newTags[ObjectTags.Length + _i] = _tags[_i];
        }

        ObjectTags = _newTags;
    }
    #endregion

    #region Remove
    /// <summary>
    /// Removes a tag from the object.
    /// </summary>
    /// <param name="_tag">Name of the tag to remove.</param>
    public void RemoveTag(string _tag)
    {
        ObjectTags = ObjectTags.Where(t => t.Name != _tag).ToArray();
    }

    /// <summary>
    /// Removes a tag from the object.
    /// </summary>
    /// <param name="_tag">Tag to remove.</param>
    public void RemoveTag(Tag _tag)
    {
        ObjectTags = ObjectTags.Where(t => t != _tag).ToArray();
    }

    /// <summary>
    /// Removes multiple tags from the object.
    /// </summary>
    /// <param name="_tags">Name of the tags to remove.</param>
    public void RemoveTags(string[] _tags)
    {
        ObjectTags = ObjectTags.Where(t => !_tags.Contains(t.Name)).ToArray();
    }

    /// <summary>
    /// Removes multiple tags from the object.
    /// </summary>
    /// <param name="_tags">Tags to remove.</param>
    public void RemoveTags(Tag[] _tags)
    {
        ObjectTags = ObjectTags.Where(t => !_tags.Contains(t)).ToArray();
    }
    #endregion

    #endregion
}
