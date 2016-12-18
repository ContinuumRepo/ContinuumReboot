 #pragma strict
 
 // Attach this script to an orthographic camera.
 
 public var object : Transform;     // The object we will move.
 
 private var offSet : Vector3;       // The object's position relative to the mouse position.
 
 function Update () {
 
     var ray = GetComponent.<Camera>().ScreenPointToRay(Input.mousePosition);     // Gets the mouse position in the form of a ray.
 
     if (Input.GetAxis ("Mouse X") && Input.GetAxis ("Mouse Y")) {     // If we click the mouse...
 
         if (!object) {      // And we are not currently moving an object...
 
             var hit : RaycastHit;
             
             if (Physics.Raycast(ray, hit, Mathf.Infinity)) {        // Then see if an object is beneath us using raycasting.
     
                 object = hit.transform;     // If we hit an object then hold on to the object.
 
                 offSet = object.position-ray.origin;        // This is so when you click on an object its center does not align with mouse position.
 
             }
 
         }
 
     }
 /*
     else if (Input.GetMouseButtonUp(0)) {
 
         object = null;      // Let go of the object.
 
     }*/
 
     if (object) {
 
         //object.position = Vector3(ray.origin.x+offSet.x, object.position.y, ray.origin.z+offSet.z);     // Only move the object on a 2D plane.
 		object.position = Vector3(ray.origin.x+offSet.x, ray.origin.y+offSet.y, ray.origin.z+offSet.z);
     }
 
 }