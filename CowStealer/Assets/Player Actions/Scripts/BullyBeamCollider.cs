using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyBeamCollider : BeamCollider
{

    // Start is called before the first frame update
    public override void Start()
    {
        beam = GameObject.Find("BullyBeam");
        beam.SetActive(false);

        outOfRangeBeam = GameObject.Find("BullyOutOfRangeBeam");
        outOfRangeBeam.SetActive(false);
    }

    // Update is called once per frame
    public override void Update()
    {
        if(bullyAbductFlag == 0) {
            if(beam.activeSelf != false) {
                beam.SetActive(false);
                audioSource.Stop();
            }

            if(outOfRangeBeam != false) {
                outOfRangeBeam.SetActive(false);
            }
        }

        else if(bullyAbductFlag == 1) {
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
