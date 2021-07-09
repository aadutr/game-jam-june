using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Radius of interaction
    public float radius = 3f;

    // This method is meant to be overwritten
	public virtual void Interact ()
	{
		
	}
    
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
