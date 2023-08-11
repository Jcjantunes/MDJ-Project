using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger2 : MonoBehaviour
{
		
		public Dialogue dialogue;
		public int flag = 0;

		void OnEnable() {
			
			if (flag == 1){
				TriggerDialogue();
			}
			flag = 1;
		}

		public void TriggerDialogue(){
			FindObjectOfType<DialogueManager2>().StartSentence();
			FindObjectOfType<DialogueManager2>().StartDialogue(dialogue);
		}

}