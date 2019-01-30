using System.Linq;
using UnityEngine;

public static class GameObjectTagsExtensions 
{
    /* GameObjectTagsExtensions :
	 *
	 *	#####################
	 *	###### PURPOSE ######
	 *	#####################
	 *
	 *	    Make a custom multi-tags system using the Unity tag system.
     *	    
     *	    Add extension methods to split the tag of a game object to get all its tags, and way to check if the game object has a specific tag.
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
	 *	    Creation of the GameObjectTagsExtensions class.
     *	    
     *	    Added the GetTags, HasTag & HasTags methods.
	 *
	 *	-----------------------------------
	*/

    #region Methods
    /// <summary>
    /// Get all this game object tags.
    /// </summary>
    /// <param name="_go">Game object to get tags from.</param>
    /// <returns>Returns all this game object tags.</returns>
    public static string[] GetTags(this GameObject _go)
    {
        return _go.tag.Split('|');
    }

    /// <summary>
    /// Does this game object has a specific tag ?
    /// </summary>
    /// <param name="_go">Game object to compare tags.</param>
    /// <param name="_tag">Tag to compare.</param>
    /// <returns>Returns true if the game object has the specified tag, false otherwise.</returns>
    public static bool HasTag(this GameObject _go, string _tag)
    {
        return _go.GetTags().Contains(_tag);
    }

    /// <summary>
    /// Does this game object has all the specified tags ?
    /// </summary>
    /// <param name="_go">Game object to compare tags.</param>
    /// <param name="_tags">All tags to compare.</param>
    /// <returns>Returns true if the game object has all the specified tags, false if it lacks event one.</returns>
    public static bool HasTags(this GameObject _go, string[] _tags)
    {
        string[] _goTags = _go.GetTags();
        return _tags.All(t => _goTags.Contains(t));
    }
    #endregion
}
