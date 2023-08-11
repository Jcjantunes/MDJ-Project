using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBeamCollider : BeamCollider
{
    // Start is called before the first frame update
    public override void Start()
    {
        beam = GameObject.Find("Beam");
        beam.SetActive(false);

        outOfRangeBeam = GameObject.Find("OutOfRangeBeam");
        outOfRangeBeam.SetActive(false);
    }

    // Update is called once per frame
    public override void Update()
    {
       if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)
            || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space)) {
            altUpFlag = 1;
            altFlag = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            altFlag = 1;
        }

        if(altUpFlag == 1) {
            if(beam.activeSelf != false) {
                beam.SetActive(false);
                audioSource.Stop();
            }

            if(outOfRangeBeam != false) {
                outOfRangeBeam.SetActive(false);
            }
            altUpFlag = 0;
        }

        if(altFlag == 1) {
            if(collisionFlag == 0) {
                beam.SetActive(false);
                audioSource.Stop();
                outOfRangeBeam.SetActive(true);              
            }
            else if(beam.activeSelf != true) {
                beam.SetActive(true);
                outOfRangeBeam.SetActive(false);
                audioSource.PlayOneShot(cowAbductingSound);
            }
        }
        
        if(abducteeVar != null) {
            abducteeVar.SetTargeyPosition(transform.parent.position);
        } 
    }
}
