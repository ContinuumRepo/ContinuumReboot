#pragma strict

var slowMoIn : AudioClip;
var slowMoOut : AudioClip;   
   
function Update(){ 
 
 if (Input.GetKeyDown (KeyCode.S)) {
  
  if (Time.timeScale == 1.0){ 
   Time.timeScale = 0.3;
  }else{
   Time.timeScale = 1.0;
   Time.fixedDeltaTime = 0.02 * Time.timeScale;
  }
  
  var aSources = FindObjectsOfType(AudioSource);
  for (aSource in aSources)
  aSource.pitch = Time.timeScale;
  
  if (Time.timeScale == 1.0) PlayAudioClip(slowMoOut, transform.position, 1.0);
  else PlayAudioClip(slowMoIn, transform.position, 1.0);
    }
 }

function PlayAudioClip (clip : AudioClip, position : Vector3, volume : float) {
    var go = new GameObject ("One shot audio");
    go.transform.position = position;
    var source : AudioSource = go.AddComponent (AudioSource);
    source.clip = clip;
    source.volume = volume;
    source.pitch = 1.0;
    source.Play ();
    Destroy (go, clip.length);
    return source;
}