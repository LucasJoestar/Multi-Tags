using System;

[Serializable]
public static class MultiTags 
{
    /* MultiTags :
	 *
	 *	#####################
	 *	###### PURPOSE ######
	 *	#####################
	 *
	 *	script to reference everything needed from the Multi-Tags system in a static class.
	 *
	 *	#####################
	 *	####### TO DO #######
	 *	#####################
	 *
	 *	    - Get all project tags at runtime, wouldn't it be nice ?
     *	    
     *      - Dynamically add or remove tags to the game in runtime ? That would be cool.
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
     *	    - Added a Char variable used to separate tags witht he multi-tags system
     *	from the Unity one.
	 *
	 *	-----------------------------------
	*/

    #region Fields / Properties
    /// <summary>
    /// Separator used to separate tags from the original Unity tag system.
    /// </summary>
    public static char TagSeparator = '|';
    #endregion

    #region Methods

    #endregion
}
