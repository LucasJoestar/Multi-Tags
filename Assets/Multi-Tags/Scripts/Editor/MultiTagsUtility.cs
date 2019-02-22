using System.Linq;
using UnityEditorInternal;

public static class MultiTagsUtility
{
    /* MultiTagsUtility :
	 *
	 *	#####################
	 *	###### PURPOSE ######
	 *	#####################
	 *
	 *	Script to stock multiple utilities static members around the multi-tags system in one script.
	 *
     *	#####################
	 *	####### TO DO #######
	 *	#####################
     * 
     *      Well, wer's all alone here...
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
	 *	    - Moved the Char variable used to separate tags to the newly created MultiTags class.
	 *
	 *	-----------------------------------
     * 
     *	Date :			[03 / 02 / 2019]
	 *	Author :		[Guibert Lucas]
	 *
	 *	Changes :
	 *
	 *	    - We can now create and remove tags from the project with the
     *	AddTag & RemoveTag methods.
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
     *	    - Added a Char variable used to separate tags witht he multi-tags system
     *	from the Unity one.
     *	    - Creation of methods to get all this project tags using the Unity system
     *	or the multi one, witht he GetTags & GetUnityTags methods.
	 *
	 *	-----------------------------------
	*/

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
    public static string[] GetTags() { return GetUnityTags().SelectMany(t => t.Split(MultiTags.TagSeparator)).ToArray(); }

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
