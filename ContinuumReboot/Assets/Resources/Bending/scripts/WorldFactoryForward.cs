using UnityEngine;
using System.Collections;

public class WorldFactoryForward : Abstract {
	public GameObject UniqueFactory;
	public GameObject terrainFactory;
	public string RoadTerrains="";
	
	private int numberOfTerrains=3;
	protected string []roadTerrainsNames;
	protected int currentRoadPos=0;	
	private AbstractElementFactory uniqueElementFactory;
	private TerrainElementFactory terrainElementFactory;
	private int versionForCoRoutine=0;
	// Use this for initialization
	void Start () {
		init();
	}
	
	private void init()
	{
		GameObject curFactoryObject;
		
		//terrain
		curFactoryObject=Instantiate (terrainFactory) as GameObject;
		terrainElementFactory=curFactoryObject.GetComponent("TerrainElementFactory") as TerrainElementFactory;
		
		//uniqueObjects
		curFactoryObject=Instantiate (UniqueFactory) as GameObject;
		uniqueElementFactory=curFactoryObject.GetComponent("AbstractElementFactory") as AbstractElementFactory;
		
		Parse();
		
		for(int i=0;i<=numberOfTerrains;i++)
		{
			AddNextTerrain(false);
		}
	}
	
	public void Parse()
	{
		if(roadTerrainsNames==null){
			ParseRoadTerrainNames();
		}
	}
	
	private void AddNextTerrain(bool FlagCoRoutine){
		StartCoroutine(AddObjects(FlagCoRoutine));		
	}
	
	private void ParseRoadTerrainNames()
	{
		//получили массив террейнов
		char []separator={',','\n',' '};
		string []names=RoadTerrains.Split(separator);;
		roadTerrainsNames=names;
	}
	
	// Update is called once per frame
	void Update () {
		singleTransform.Translate(0,0,Time.deltaTime*20);
	}
	
	public void TryAddTerrrain() {	
		terrainElementFactory.SetNextCurrentTerrainNext();
		AddNextTerrain(true);
		//удаляем старый кусочек земли
		DeleteOneFirstTerrain();
    }
	
	private void DeleteOneFirstTerrain()
	{		
		terrainElementFactory.DeleteOneFirstTerrain();
	}
	
	private IEnumerator AddObjects(bool FlagCoRoutine){	
		
		SingleTerrainTag terrainTag;
		GameObject newTerrain=null;
		Vector3 newpos=new Vector3(0,0,0);
				
		Vector3 lastAddedEndOfTerrain;
		
		if(terrainElementFactory.GetLastAddedObject())
		{
			lastAddedEndOfTerrain=terrainElementFactory.GetLastAddedObject().singleTransform.FindChild("EndOfTerrain").position;
		}
		else
		{
			lastAddedEndOfTerrain=new Vector3(0,0,0);
		}
		
		newTerrain=AddTerrain();
		
		terrainTag=terrainElementFactory.GetLastAddedObject().GetComponent<SingleTerrainTag>();
					
		newpos=new Vector3(lastAddedEndOfTerrain.x,lastAddedEndOfTerrain.y,lastAddedEndOfTerrain.z+terrainTag.sizeOfPlane/2);
		
		
		newTerrain.transform.position=newpos;
			
		if(FlagCoRoutine) yield return null;

		//а надо ли что нибудь ещё добавлять?
		//динамические объекты
		StartCoroutine(addDynamicByMarkers(newTerrain,terrainTag,FlagCoRoutine));	
		if(FlagCoRoutine) yield return null;
		
	}
	
	private IEnumerator addDynamicByMarkers(GameObject inTerrain,SingleTerrainTag interrainTag,bool FlagCoRoutine){
		int i,j;
		string curname;
		
		int curversionForCoRoutine=versionForCoRoutine;
		//uniqueobjects
		ArrayList markedObjectsUnique=new ArrayList();		
		
		//find all marks
		Transform[] allChildren = inTerrain.gameObject.GetComponentsInChildren<Transform>();
		for(i=0;i<allChildren.Length;i++)
		{
			if(curversionForCoRoutine!=versionForCoRoutine) yield break;

			//uniqueObjects
			if(allChildren[i].name=="UniqueObjectPool"){
				markedObjectsUnique.Add (allChildren[i]);
			}	
			
			if(FlagCoRoutine&&i%50==0) yield return null;
		}
		
		//unique
		Transform curUnique;
		for(i=0;i<markedObjectsUnique.Count;i++){
			Transform[] uniqueMarkers = (markedObjectsUnique[i] as Transform).gameObject.GetComponentsInChildren<Transform>();
			for(j=1;j<uniqueMarkers.Length;j++){
				if(curversionForCoRoutine!=versionForCoRoutine)yield break;
				curUnique=(uniqueMarkers[j] as Transform);
				
				curname=curUnique.name;
				if(!curname.Contains("IgnoreContainer"))
				{
					addOneUniqueAtMarker(curUnique,interrainTag);
					if(FlagCoRoutine&&j%2==0) yield return null;
				}
			}
		}
		if(curversionForCoRoutine!=versionForCoRoutine) yield break;
		if(FlagCoRoutine) yield return null;
	}
	
	private void addOneUniqueAtMarker(Transform marker,SingleTerrainTag interrainTag){
		GameObject newObject;
		
		newObject = uniqueElementFactory.GetNewObjectWithName(marker.name);
		
		if(!newObject)
		{
			Debug.Log ("Object Not Found - "+marker.name);
			return;
		}
		
		//set position & rotation
		newObject.transform.position=marker.position;
		
		newObject.transform.rotation=marker.rotation;
	
		if(interrainTag){
			interrainTag.PushToAllElements(newObject.GetComponent<AbstractTag>());
		}
	}
	
	
	public GameObject AddTerrain()
	{
		GameObject newTerrain=null;
		if(currentRoadPos>=roadTerrainsNames.Length)
		{
			currentRoadPos=0;
		}
		newTerrain=terrainElementFactory.GetNewObjectWithName(roadTerrainsNames[currentRoadPos]);
		currentRoadPos++;
		return newTerrain;
	}
	
	public Vector3 GetCurTerrainEnd()
	{
		return terrainElementFactory.GetCurrentTerrainForZ().singleTransform.FindChild("EndOfTerrain").position;
	}
}
