using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOCollsion : MonoBehaviour
{
	private GameObject popup, popupYellow, popupRed, popupBlue, popupBlack, enterImage, enterText, lock0, lock1, lock2, lock3, lock4;

    // Start is called before the first frame update
    void Start()
    {
        enterImage = GameObject.Find("EnterImage");
        enterText = GameObject.Find("EnterText");

        popup = GameObject.Find("Popup");
		popupYellow = GameObject.Find("PopupYellow");
		popupRed = GameObject.Find("PopupRed");
		popupBlue = GameObject.Find("PopupBlue");
		popupBlack = GameObject.Find("PopupBlack");

		lock0 = GameObject.Find("Lock");
		lock1 = GameObject.Find("Lock1");
		lock2 = GameObject.Find("Lock2");
		lock3 = GameObject.Find("Lock3");
		lock4 = GameObject.Find("Lock4");


        popup.SetActive(false);
        popupYellow.SetActive(false);
        popupRed.SetActive(false);
        popupBlue.SetActive(false);
        popupBlack.SetActive(false);

        enterImage.SetActive(false);
	    enterText.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
    	if(collision.gameObject.tag == "level1") {
    		popup.SetActive(true);
    		if (GameMode.IsUnlocked("level_1")){
    			lock0.SetActive(false);

    		}
    		else{
    			lock0.SetActive(true);
    		}
		}

		else if(collision.gameObject.tag == "level2") {
			popupYellow.SetActive(true);
			if (GameMode.IsUnlocked("level_2")){
    			lock1.SetActive(false);

    		}
    		else{
    			lock1.SetActive(true);
    		}


		}

		else if(collision.gameObject.tag == "level3") {
			popupBlue.SetActive(true);
			if (GameMode.IsUnlocked("level_3")){
    			lock2.SetActive(false);

    		}
    		else{
    			lock2.SetActive(true);
    		}
		}

		else if(collision.gameObject.tag == "level4") {
			popupRed.SetActive(true);
			if (GameMode.IsUnlocked("level_4")){
    			lock3.SetActive(false);

    		}
    		else{
    			lock3.SetActive(true);
    		}
		}

		else if(collision.gameObject.tag == "level5") {
			popupBlack.SetActive(true);
			if (GameMode.IsUnlocked("level_5")){
    			lock4.SetActive(false);

    		}
    		else{
    			lock4.SetActive(true);
    		}
		}

		enterImage.SetActive(true);
	    enterText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
    	if(collision.gameObject.tag == "level1") {
    		popup.SetActive(false);
		}

		else if(collision.gameObject.tag == "level2") {
			popupYellow.SetActive(false);
		}

		else if(collision.gameObject.tag == "level3") {
			popupBlue.SetActive(false);
		}

		else if(collision.gameObject.tag == "level4") {
			popupRed.SetActive(false);
		}

		else if(collision.gameObject.tag == "level5") {
			popupBlack.SetActive(false);
		}
	    enterImage.SetActive(false);
	    enterText.SetActive(false);
    }
}
