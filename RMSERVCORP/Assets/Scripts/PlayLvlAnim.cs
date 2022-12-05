using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLvlAnim : MonoBehaviour
{
    [SerializeField] public Animator myAnimationController;
    public EnemyController enemy;
    public Canvas myC;

    public void OnTriggerEnter(Collider other)   //If the tag "Player" enters the "Collider" then set the boolean for animation to true
    {
        if (other.CompareTag("Player")) 
        {
            myAnimationController.SetBool("Trigger", true);
            //enemy.FocusPlayer(other.transform);

            
        }

        
    }

}
