using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LSCharacters : MonoBehaviour
{
    public GameObject TonyLS,BearLS,PDLS;
    public GameObject particles;

    void Awake()
    {
        TonyLS.SetActive(false);
        BearLS.SetActive(false);
       // PDLS.SetActive(false);
    }

    public void EnableTony()
    {
        Instantiate(particles, TonyLS.transform.position + new Vector3(0,2,0), particles.transform.rotation);
        BearLS.SetActive(false);
        StartCoroutine(EnableTonyM(0.5f));
       // PDLS.SetActive(false);
    }
    IEnumerator EnableTonyM(float delay)
    {
        yield return new WaitForSeconds(delay);
        TonyLS.SetActive(true);
    }
    public void EnableBear()
    {
        Instantiate(particles, BearLS.transform.position + new Vector3(0,2,0), particles.transform.rotation);        
        TonyLS.SetActive(false);
      //  PDLS.SetActive(false);
        StartCoroutine(EnableBearM(0.5f));
    }
    
    IEnumerator EnableBearM(float delay)
    {
        yield return new WaitForSeconds(delay);
        BearLS.SetActive(true);
    }

    public void EnablePD()
    {
        Instantiate(particles, PDLS.transform.position + new Vector3(0,2,0), particles.transform.rotation); 
        TonyLS.SetActive(false);
        BearLS.SetActive(false);
        StartCoroutine(EnablePDM(0.5f));
    }
    
    IEnumerator EnablePDM(float delay)
    {
        yield return new WaitForSeconds(delay);
        //PDLS.SetActive(true);
    }
}
