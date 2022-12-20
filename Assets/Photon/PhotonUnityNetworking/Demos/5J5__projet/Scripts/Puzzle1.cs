using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    public static bool puzzle1Active = false;
    public static bool puzzle2Active = false;
    public GameObject anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate() {
        if (puzzle1Active && puzzle2Active) {
            anim.SetActive(true);
        }
    }
}
