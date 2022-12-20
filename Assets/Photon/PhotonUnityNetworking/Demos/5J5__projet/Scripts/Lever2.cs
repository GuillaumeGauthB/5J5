using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever2 : Puzzle1
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            puzzle2Active = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            puzzle2Active = false;
        }
    }
}
