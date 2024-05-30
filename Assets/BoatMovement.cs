using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{

    // "W" will apply force from the back
    // A,D will rotate the boat in directions
    // "S" will apply backwards force

    public float ForceForward;
    public float TurningSpeed;
    public float ForceBackward;

    private void Start(){
        ForceBackward = -ForceForward/2;
    }

     private void FixedUpdate() {
        
        




    }
}
