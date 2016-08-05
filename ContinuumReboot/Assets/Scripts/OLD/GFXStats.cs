using UnityEngine; 
using System.Collections;
using UnityEngine.UI;
//using UnityEditor;

public class GFXStats : MonoBehaviour 
{ 
	[Header ("Audio:")]
	public float AudioLevel;
	public float DspLoad;
	public float Clipping;
	public float StreamLoad;

	[Header ("Graphics:")]
	public int batches;
	public int tris;
	public int verts;
	public string screenDimensions;
	public int setPassCalls;
	public int shadowCasters;
	public int visibleSkinnedMeshes;
	public int animations;

	[Header ("Texts")]
	public Text AudioLevelText;
	public Text DspLoadText;
	public Text ClippingText;
	public Text StreamLoadText;
	
	public Text BatchesText;
	public Text PolysText;
	public Text VertsText;
	public Text screenDimensionsText;
	public Text SetPassCallsText;
	public Text ShadowCastersText;
	public Text VisibleSkinnedMeshesText;
	public Text AnimationsText;
	//public UnityStats Stats;

	void Start()
	{
		//Stats = UnityEditor.UnityStats;
	}
	
	void Update()
	{
		# if UNITY_EDITOR
		// AUDIO
		AudioLevel = UnityEditor.UnityStats.audioLevel;
		AudioLevelText.text = "Audio level: " + AudioLevel + " dB";

		DspLoad = UnityEditor.UnityStats.audioDSPLoad;
		DspLoadText.text = "DSP load: " + DspLoad + "";

		Clipping = UnityEditor.UnityStats.audioClippingAmount;
		ClippingText.text = "Clipping: " + Clipping + "";

		StreamLoad = UnityEditor.UnityStats.audioStreamLoad;
		StreamLoadText.text = "Stream load: " + StreamLoad + "";

		batches = UnityEditor.UnityStats.batches;
		BatchesText.text = "Batches: " + batches + "";

		tris = UnityEditor.UnityStats.triangles;
		PolysText.text = "Tris: " + tris + "";
		
		verts = UnityEditor.UnityStats.vertices;
		VertsText.text = "Verts: " + verts + "";

		screenDimensions = UnityEditor.UnityStats.screenRes;
		screenDimensionsText.text = "Screen: " + screenDimensions + " px";

		setPassCalls = UnityEditor.UnityStats.setPassCalls;
		SetPassCallsText.text = "SetPass calls: " + setPassCalls + "";

		shadowCasters = UnityEditor.UnityStats.shadowCasters;
		ShadowCastersText.text = "Shadow casters: " + shadowCasters + "";

		visibleSkinnedMeshes = UnityEditor.UnityStats.visibleSkinnedMeshes;
		VisibleSkinnedMeshesText.text = "Visible skinned meshes: " + visibleSkinnedMeshes + "";

		animations = UnityEditor.UnityStats.visibleAnimations;
		AnimationsText.text = "Animations: " + animations + "";
		#endif
	}
}