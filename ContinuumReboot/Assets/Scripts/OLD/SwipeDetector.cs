using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour 
{
	
	public float minSwipeDistY;
	
	public float minSwipeDistX;
	
	public Vector2 startPos;

	//private BasicPlayerMovement PlayerMove;

	void start ()
	{
		//PlayerMove = gameObject.GetComponent<BasicPlayerMovement>();
	}
	
	void Update()
	{
		//#if UNITY_ANDROID
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.touches[0];
			switch (touch.phase) 	
			{	
			case TouchPhase.Began:

				startPos = touch.position;

				break;

			case TouchPhase.Ended:	

				float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

				if (swipeDistVertical > minSwipeDistY) 	
				{
					float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
	
					if (swipeValue > 0)//up swipe
					{
						//Jump ();
						Debug.Log ("Up swipe");
					}

					if (swipeValue < 0)//down swipe
					{
							//Shrink ();
						Debug.Log ("Down swipe");
					}	
				}
				
				float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
				
				if (swipeDistHorizontal > minSwipeDistX) 	
				{	
					float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
					
					if (swipeValue > 0)//right swipe
					{
						//MoveRight ();
						Debug.Log ("Right swipe");
					}

					if (swipeValue < 0)//left swipe 
					{
							
							//MoveLeft ();
						Debug.Log ("Left swipe");
					}		
				}
				break;
			}
		}
	}
}