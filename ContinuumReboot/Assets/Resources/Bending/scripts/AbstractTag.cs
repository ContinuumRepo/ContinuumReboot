using UnityEngine;
using System.Collections;
using  System.Globalization;

public class AbstractTag : Abstract{	
	protected AbstractElementFactory abstractElementFactory;
	
	
	public void addFactory(AbstractElementFactory inabstractElementFactory){
		abstractElementFactory=inabstractElementFactory;
	}
	
	public virtual void DeleteFromUsed(){
		abstractElementFactory.DeleteCurrent(this);
	}
	
	public virtual void ReStart()
	{
		
	}
}