using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    

	public Text dialogueText;

    private Queue<string> sentences;

   	private GameObject continueButton, skipButton;

    public float textTime = 0.1f;
    public float sentenceTime = 0.3f;
    public float skipTime = 0.9f;

    public int skipFlag = 0;

    public AudioSource audioSource;
    
    public AudioClip typingSound;


    void Start()
    {
     	sentences = new Queue<string>(); 
     	continueButton = GameObject.Find("ContinueButton");
     	skipButton = GameObject.Find("SkipButton"); 
     	continueButton.SetActive(false);
     	skipButton.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue){

	 	foreach (string sentence in dialogue.sentences){	
			sentences.Enqueue(sentence);
			DisplayNextSentence();
		}
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
    	audioSource.PlayOneShot(typingSound);
    	StartCoroutine(skipButtonFunction());
    	foreach (char letter in sentence.ToCharArray()){
    		dialogueText.text += letter;
    		yield return new WaitForSeconds(textTime);
    		if(letter == '.' || letter == '!' || letter == ',' || letter == ':' || letter == 'U') {
    			audioSource.Stop();
    			yield return new WaitForSeconds(sentenceTime);
    			audioSource.PlayOneShot(typingSound);
    		}
    	}
    	
    	EndDialogue();

    }

    public IEnumerator skipButtonFunction() {
    	yield return new WaitForSeconds(skipTime);
    	skipFlag = 1;
    	skipButton.SetActive(true);
    }

    public void EndDialogue(){
    	audioSource.Stop();
    	skipButton.SetActive(false);
    	continueButton.SetActive(true);
        GameObject.FindObjectOfType<CutSceneManager>().canContinue = true;
   }

   void Update() {
   		if(Input.GetKeyDown(KeyCode.Return)) {
   			if(skipFlag == 1) {
	   			textTime = 0;
	   			sentenceTime = 0;
   			}
   		}
   }

   public void SkipButtonClick()
   {
        if (skipFlag == 1)
        {
            textTime = 0;
            sentenceTime = 0;
        }
   }


}
