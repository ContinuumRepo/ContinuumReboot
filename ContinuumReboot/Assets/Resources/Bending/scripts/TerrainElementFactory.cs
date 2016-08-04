using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainElementFactory: AbstractElementFactory{	
	private SingleTerrainTag currentTerrain=null;
	
	private int versionForCoRoutine=0;
	
	public override void ReStart(){
		versionForCoRoutine++;
		base.ReStart();
		currentTerrain=null;
		for(int i=0;i<terrainsListToDel.Count;i++)
		{
			AbstractTag newterrainToDel=terrainsListToDel[i];
			List<AbstractTag> AllElements=(newterrainToDel.GetComponent("SingleTerrainTag") as SingleTerrainTag).GetAllElements();
			AllElements.Clear();	
		}
	}
	
	public override void DeleteOneFirstTerrain()
	{
		StartCoroutine(ClearTerrainFactory());	
	}
		
	private IEnumerator ClearTerrainFactory(){	
		
		int curversionForCoRoutine=versionForCoRoutine;
		if(terrainsList.Count>0)
		{
			if(curversionForCoRoutine!=versionForCoRoutine) yield break;
			AbstractTag newterrainToDel=terrainsList[0];
			SingleTerrainTag terrainTag=newterrainToDel.GetComponent("SingleTerrainTag") as SingleTerrainTag;
			List<AbstractTag> AllElements=terrainTag.GetAllElements();
			terrainTag.RemakeAllElementsList();
			yield return null;
			if(curversionForCoRoutine!=versionForCoRoutine) yield break;
			terrainsList.Remove(newterrainToDel);			
			terrainsListToDel.Add(newterrainToDel);	
			if(curversionForCoRoutine!=versionForCoRoutine) yield break;
			yield return null;
			for(int i=0;i<AllElements.Count;i++)
			{
				if(curversionForCoRoutine!=versionForCoRoutine) yield break;
				if(AllElements[i]){
					AllElements[i].DeleteFromUsed();
				}
				yield return null;
			}
			if(curversionForCoRoutine!=versionForCoRoutine) yield break;
			AllElements.Clear();
			AllElements=null;
		}
		yield return null;
	}
	
	public override void PutToFirstState(AbstractTag newTerrain){
		newTerrain.singleTransform.position=new Vector3(-9999,-9999,-9999);
		newTerrain.singleTransform.rotation=Quaternion.identity;
	}
	
	public SingleTerrainTag GetCurrentTerrainForZ()
	{
		if(!currentTerrain)
		{
			currentTerrain=(terrainsList[0] as Abstract).GetComponent<SingleTerrainTag>();
		}	
		return currentTerrain;
	}
	
	public void SetNextCurrentTerrain(SingleTerrainTag interrain)
	{
		currentTerrain=interrain;
	}
	
	public void  SetNextCurrentTerrainNext()
	{
		SetNextCurrentTerrain(terrainsList[1]as SingleTerrainTag);
	}
	
	public float GetCurTerrainCenter(){
		float returny=GetCurrentTerrainForZ().transform.position.y;
		
		return returny;
	}
}
