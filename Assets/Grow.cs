using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GrowRoutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GrowRoutine()
    {
        for(float i = .5f; i < 1f; i += .05f)
        {
            transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(.05f);
        }
        yield return new WaitForSeconds(3f);
        while(transform.localScale.x > .01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), .2f);
            yield return new WaitForSeconds(.05f);
        }
        gameObject.SetActive(false);
        yield return null;
    }
}
