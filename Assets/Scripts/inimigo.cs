using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigo : MonoBehaviour {
    
    public GameObject           hitPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ANIMÇÃO HIT QUANDO ENCOSTA NO INIMIGO
    void OnTriggerEnter2D(Collider2D col) 
    {
        switch(col.gameObject.tag)
        {
        case "hitBox":
            GameObject temp = Instantiate(hitPrefab, transform.position, transform.localRotation);
            Destroy(temp.gameObject, 0.05f);
            Destroy(this.gameObject);
            break;    
        }
    }
}
