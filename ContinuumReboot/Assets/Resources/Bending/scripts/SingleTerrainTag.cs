using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingleTerrainTag : AbstractTag {
	
	public float sizeOfPlane;
	
	protected List<AbstractTag> AllElements = new List<AbstractTag>();
	
	public void PushToAllElements(AbstractTag inobj){
		AllElements.Add(inobj);
	}
	
	
	public List<AbstractTag> GetAllElements(){
		return AllElements;
	}
	
	public void RemakeAllElementsList()
	{
		AllElements = new List<AbstractTag>();
	}
}
