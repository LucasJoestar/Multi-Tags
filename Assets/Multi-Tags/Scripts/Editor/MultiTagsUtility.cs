using System.Linq;
using UnityEditorInternal;

public sealed class MultiTagsUtility
{
    /* MultiTagsUtility :
	 *
	 *	#####################
	 *	###### PURPOSE ######
	 *	#####################
	 *
	 *	Stock utilities static methods around the multi-tags system in one script.
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
	 *	    Added the AddTag & RemoveTag methods.
	 *
	 *	-----------------------------------
     * 
	 *	Date :			[30 / 01 / 2019]
	 *	Author :		[Guibert Lucas]
	 *
	 *	Changes :
	 *
	 *	Creation of the MultiTagsUtility class.
     *	
     *	    Added the static TagSeparator field.
     *	    Added the static GetTags & GetUnityTags methods.
	 *
	 *	-----------------------------------
	*/

    #region Fields / Properties
    /// <summary>
    /// Separator used to separate tags in the original Unity tag system
    /// </summary>
    public static char TagSeparator = '|';
    #endregion

    #region Methods
    /// <summary>
    /// Adds a tag to this Unity project.
    /// </summary>
    /// <param name="_tag">New tag to add.</param>
    public static void AddTag(string _tag) => InternalEditorUtility.AddTag(_tag);

    /// <summary>
    /// Get all this project tags, using the multi-tags system.
    /// </summary>
    /// <returns>Returns a string array of all tags, using the multi-tags system, of this proejct.</returns>
    public static string[] GetTags() { return GetUnityTags().SelectMany(t => t.Split(TagSeparator)).ToArray(); }

    /// <summary>
    /// Get all this project Unity tags.
    /// </summary>
    /// <returns>Returns a string array of all Unity tags from this project.</returns>
    public static string[] GetUnityTags() { return InternalEditorUtility.tags; }

    /// <summary>
    /// Removes a tag from this Unity project.
    /// </summary>
    /// <param name="_tag">Tag to remove.</param>
    public static void RemoveTag(string _tag) => InternalEditorUtility.RemoveTag(_tag);
	#endregion
}
