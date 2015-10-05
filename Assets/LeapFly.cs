using UnityEngine;
using System.Collections;
using Leap;

public class LeapFly : MonoBehaviour {
  
  Controller m_leapController;
  
  // Use this for initialization
  void Start () {
    m_leapController = new Controller();
    if (transform.parent == null) {
			Debug.LogError ("LeapFly must have a parent object to control"); 
		} 
	else {
			Debug.LogError ("Leap works");
		}
  }
  
  Hand GetLeftMostHand(Frame f) {
    float smallestVal = float.MaxValue;
    Hand h = null;
    for(int i = 0; i < f.Hands.Count; ++i) {
      if (f.Hands[i].PalmPosition.ToUnity().x < smallestVal) {
        smallestVal = f.Hands[i].PalmPosition.ToUnity().x;
        h = f.Hands[i];
      }
    }
    return h;
  }

	void ProcessLook(Hand hand) {
		float handX = hand.PalmPosition.ToUnityScaled().x;
		if (Mathf.Abs(handX) > 1.0f) {
			transform.RotateAround(Vector3.up, handX * 0.03f);
		}
	}

	void MoveCharacter(Hand hand) {
		if (hand.PalmPosition.ToUnityScaled().z > .1f) {
			transform.position += transform.forward * 0.1f;
		}
		
		if (hand.PalmPosition.ToUnityScaled().z < -.1f) {
			transform.position -= transform.forward * 0.04f;
		}

		if (hand.PalmPosition.ToUnityScaled().x > .1f) {
			transform.position += transform.right * 0.1f;
		}
		
		if (hand.PalmPosition.ToUnityScaled().x < -.1f) {
			transform.position -= transform.right * 0.04f;
		}
	}
  
  Hand GetRightMostHand(Frame f) {
    float largestVal = -float.MaxValue;
    Hand h = null;
    for(int i = 0; i < f.Hands.Count; ++i) {
      if (f.Hands[i].PalmPosition.ToUnity().x > largestVal) {
        largestVal = f.Hands[i].PalmPosition.ToUnity().x;
        h = f.Hands[i];
      }
    }
    return h;
  }
  
  void FixedUpdate () {
    
    Frame frame = m_leapController.Frame();
	Hand leftHand = GetLeftMostHand(frame);
	Hand rightHand = GetRightMostHand(frame);

	if (leftHand != null) {
			MoveCharacter(leftHand);

	}

  }


  
}
