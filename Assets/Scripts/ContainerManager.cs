using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public GameObject containerToInstantiate;
    public Basket[] activeBaskets;

    void Start()
    {
        activeBaskets = new Basket[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
