using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpBar : MonoBehaviour
{
    Transform bar;

    // Start is called before the first frame update
    void Start()
    {

        bar = transform.Find("Bar");

    }

    public void setXp(float xp)
    {
        bar.localScale = new Vector3(xp, 1f);
    }
    
}


