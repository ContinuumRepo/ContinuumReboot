using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragGameObjectCS : MonoBehaviour
{
	private Vector3 screenPoint;
	private Vector3 offset;
	public GameObject Player;
	
	public void OnMouseDown (PointerEventData eventData)
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	
	public void OnMouseDrag (PointerEventData eventData)
	{
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		Player.transform.position = cursorPosition;
	}
}
