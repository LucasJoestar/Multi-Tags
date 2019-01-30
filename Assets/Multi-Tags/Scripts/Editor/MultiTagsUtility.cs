using System;
using System.Collections;
using System.Collections.Generic;
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
	 *	Date :			[30 / 01 / 2019]
	 *	Author :		[Guibert Lucas]
	 *
	 *	Changes :
	 *
	 *	Creation of the MultiTagsUtility class.
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
    /// Get all this project tags, using the multi-tags system.
    /// </summary>
    /// <returns>Returns a string array of all tags, using the multi-tags system, of this proejct.</returns>
    public static string[] GetTags() { return GetUnityTags().SelectMany(t => t.Split(TagSeparator)).ToArray(); }

    /// <summary>
    /// Get all this project Unity tags.
    /// </summary>
    /// <returns>Returns a string array of all Unity tags from this project.</returns>
    public static string[] GetUnityTags() { return InternalEditorUtility.tags; }
	#endregion
}
