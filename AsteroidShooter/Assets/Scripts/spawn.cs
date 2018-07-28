using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

	public GameObject m_myObject;
 
     void Start()
     {
        
         // StartCoroutine( InitOverNineThousandObject( 9001 ) );

            int  i = 0;
          while( i < 16384 )
		{
             GameObject.Instantiate( m_myObject );
             i++;
             Debug.Log(i);
             
         }
     }
     
	 
     IEnumerator InitOverNineThousandObject( int nObjectToSpawn )
     {
        int i = 0;
        while( i < nObjectToSpawn )
		{
             GameObject.Instantiate( m_myObject );
             i++;
             Debug.Log(i);
             yield return null;
         }
     }
}
