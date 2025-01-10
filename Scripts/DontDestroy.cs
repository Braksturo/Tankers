using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    void Start()
    {
        // Mantener este objeto entre escenas
        DontDestroyOnLoad(this.gameObject);
    }

    

}

