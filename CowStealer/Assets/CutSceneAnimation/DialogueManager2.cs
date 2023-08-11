using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager2 : MonoBehaviour
{
    

	public Text dialogueText;

    private Queue<string> sentences;

   	private GameObject continueButton, skipButton;

    public float textTime = 0.1f;
    public float sentenceTime = 0.3f;
    public float skipTime = 0.9f;

    public int skipFlag = 0;
    public int dialogueFlag = 0;

    public AudioSource audioSource;
    
    public AudioClip alienSound;

    void Start(){
        continueButton = GameObject.Find("ContinueButton");
    }

    public void StartSentence()
    {
     	sentences = new Queue<string>(); 
    }

    public void StartDialogue(Dialogue dialogue){

        sentences.Clear();
	 	foreach (string sentence in dialogue.sentences){
			sentences.Enqueue(sentence);
			
		}
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){

    	if (sentences.Count == 0){
    		return;
    	}

    	string sentence = sentences.Dequeue();
    	StopAllCoroutines();
    	StartCoroutine(TypeSentence(sentence));
    }

    public IEnumerator TypeSentence (string sentence){

    	dialogueText.text = "";
    	audioSource.PlayOneShot(alienSound);
    	foreach (char letter in sentence.ToCharArray()){
    		dialogueText.text += letter;
    		yield return new WaitForSeconds(textTime);
    		if(letter == '.' || letter == '!' || letter == ',' || letter == ':' || letter == 'U') {
    			yield return new WaitForSeconds(sentenceTime);
    		}
    	}
    	
    	EndDialogue();

    }

    public IEnumerator skipButtonFunction() {
    	yield return new WaitForSeconds(skipTime);
    	skipButton.SetActive(true);
    }

    public void EndDialogue(){
    	audioSource.Stop();
        if (dialogueFlag == 0){
            DisplayNextSentence();
            dialogueFlag = 1;
        }
   }

   void Update() {
   		if(Input.GetKeyDown(KeyCode.Return)) {
   			if(skipFlag == 1) {
	   			textTime = 0;
	   			sentenceTime = 0;
   			}
   		}

        if(continueButton.active)
            GameObject.FindObjectOfType<CutSceneManager>().canContinue = true;
   }

   public void SkipButtonClick()
   {
        if (skipFlag == 1)
        {
            
        }
   }


}
