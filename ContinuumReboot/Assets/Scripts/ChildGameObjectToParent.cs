using UnityEngine;
using System.Collections;

public class ChildGameObjectToParent : MonoBehaviour 
{
	public float delay = 0;
	public GameObject Parent;
	private GameObject Child;

	void Start () 
	{
		if (Parent == null) 
		{
			Parent = GameObject.Find ("Player");
		}

		StartCoroutine (ChildGameObject());
	}

	IEnumerator ChildGameObject ()
	{
		yield return new WaitForSeconds (delay);	
		gameObject.transform.parent = Parent.transform; 
		//gameObject.transform.position = new Vector3 (0, 0, 0, Space.Self);
	}
}
