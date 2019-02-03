using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadToEnter : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
       
    }
        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
