using UnityEngine;
using System.Collections;

//Base Class Function
public abstract class ButtonEvents : MonoBehaviour
{
	//Main Method. Don't need return bool state
	public abstract void OnClick();

	public abstract void OnEnter();

	public abstract void OnExit();
}
