using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class PostProcessBuild 
{
	[PostProcessBuildAttribute(1)]
	public static void OnPostProcessBuild (BuildTarget target, string pathToBuiltProject)
	{
		if (target == BuildTarget.StandaloneWindows || target == BuildTarget.StandaloneWindows64) 
		{
			// Removes annoying .pdb files on build.
			string pureBuildPath = Path.GetDirectoryName (pathToBuiltProject);
			foreach (string file in Directory.GetFiles(pureBuildPath, "*.pdb")) 
			{
				Debug.Log (file + " deleted!");
				File.Delete (file);
			}
		}
	}
}