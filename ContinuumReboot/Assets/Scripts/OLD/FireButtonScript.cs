using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; // Requires when using Event data.

public class FireButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public PlayerShooting PlayerShootingScript;

	void Start () 
	{
	}

	void Update () 
	{
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		PlayerShootingScript.isFiring = true;
		//Debug.Log ("isFiring = true");
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		PlayerShootingScript.isFiring = false;
	}
}
