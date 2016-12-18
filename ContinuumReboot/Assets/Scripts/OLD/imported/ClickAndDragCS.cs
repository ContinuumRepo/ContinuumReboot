// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class ClickAndDragCS : MonoBehaviour {
	
	
	// Attach this script to an orthographic camera.
	
	public Transform Obj;     // The object we will move.
	
	private Vector3 offSet;       // The object's position relative to the mouse position.
	
	void  Update (){
		
		Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);     // Gets the mouse position in the form of a ray.
		
		//if (Input.GetAxis ("Mouse X") || Input.GetAxis ("Mouse Y")) {     // If we click the mouse...
			
			if (Obj == null) {      // And we are not currently moving an object...
				
				RaycastHit hit;
				
				if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {        // Then see if an object is beneath us using raycasting.
					
					Obj = hit.transform;     // If we hit an object then hold on to the object.
					
					offSet = Obj.position - ray.origin;        // This is so when you click on an object its center does not align with mouse position.
					
				}
				
			//}
			
		}
		/*
     else if (Input.GetMouseButtonUp(0)) {
 
         object = null;      // Let go of the object.
 
     }*/
		
		if (Obj != null) 
		{
			
			//object.position = Vector3(ray.origin.x+offSet.x, object.position.y, ray.origin.z+offSet.z);     // Only move the object on a 2D plane.
			Obj.transform.position = new Vector3 
			(
				ray.origin.x + offSet.x, 
				ray.origin.y + offSet.y, 
				ray.origin.z + offSet.z
			);
		}
		
	}
}
