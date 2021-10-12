   using UnityEngine;
   using UnityEngine.AI;
    
    public class AIfollow : MonoBehaviour 
    {  
       //Assign in inspector the target that you want to follow
       public Transform target;
       
       void Update() 
       {
          //Move towards target every frame and stay in the NavMesh
          GetComponent<NavMeshAgent>().destination = target.position;
       }
       
    }