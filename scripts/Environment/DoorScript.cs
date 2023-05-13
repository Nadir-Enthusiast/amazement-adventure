using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject theDoor;
    public AudioSource doorFX;

    void OnTriggerEnter(Collider other)
    {
        doorFX.Play();
        this.GetComponent<BoxCollider>().enabled = false;
        theDoor.GetComponent<Animator>().Play("door_2_open");
        StartCoroutine(CloseDoor());
    }
    
    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(4);
        theDoor.GetComponent<Animator>().Play("door_2_close");
        this.GetComponent<BoxCollider>().enabled = true;
        doorFX.Play();
    }
}
