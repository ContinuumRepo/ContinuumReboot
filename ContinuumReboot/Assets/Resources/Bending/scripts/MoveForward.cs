using UnityEngine;
using System.Collections;

public class MoveForward : Abstract {
	
	private WorldFactoryForward worldFactoryScript;

	// Use this for initialization
	void Start () {
		if(!worldFactoryScript)
		{
			//Get world factory script
			GameObject worldFactory=null;
			worldFactory=GameObject.Find("/WorldFactory");;
			if(worldFactory)
			{
				worldFactoryScript=worldFactory.GetComponent<WorldFactoryForward>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		singleTransform.position+=new Vector3(0,0,Time.deltaTime*20);
		
		if(singleTransform.position.z>worldFactoryScript.GetCurTerrainEnd().z+20)
		{
			worldFactoryScript.TryAddTerrrain();
		}
	}
}
