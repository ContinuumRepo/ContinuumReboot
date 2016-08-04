#pragma strict

 var scaleFactor = 0.5;
 var object : Transform;
 
function Update()
{
	var gameOver = PlayerPrefs.GetString ("GameOver");
	var dist = 0;
    if(object && gameOver == "false")
    {
    	dist = Vector3.Distance(object.position, transform.position);
    }
	GetComponent.<AudioSource>().pitch = dist * scaleFactor + 0.1;
}