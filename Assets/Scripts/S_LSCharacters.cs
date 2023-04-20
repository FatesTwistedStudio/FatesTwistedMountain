using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_LSCharacters : MonoBehaviour
{
    public GameObject TonyLS,BearLS,PDLS;
    public GameObject particles;
    public Image characterimage;
    public Color TonyC,BearC,PDC;

    void Awake()
    {
        TonyLS.SetActive(false);
        BearLS.SetActive(false);
       // PDLS.SetActive(false);
    }

    public void EnableTony()
    {
        if (!TonyLS.activeSelf)
        {
        Instantiate(particles, TonyLS.transform.position + new Vector3(0,2,0), particles.transform.rotation);
        }
        BearLS.SetActive(false);
        StartCoroutine(EnableTonyM(0.5f));
        characterimage.GetComponent<Image>().color = TonyC;
       // PDLS.SetActive(false);
    }
    IEnumerator EnableTonyM(float delay)
    {
        yield return new WaitForSeconds(delay);
        TonyLS.SetActive(true);
    }
    public void EnableBear()
    {
        if (!BearLS.activeSelf){
        Instantiate(particles, BearLS.transform.position + new Vector3(0,2,0), particles.transform.rotation);        
        }
        TonyLS.SetActive(false);
        characterimage.GetComponent<Image>().color = BearC;

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
        if (!PDLS.activeSelf)
        {
        Instantiate(particles, PDLS.transform.position + new Vector3(0,2,0), particles.transform.rotation); 
        }
        TonyLS.SetActive(false);
        BearLS.SetActive(false);
        characterimage.GetComponent<Image>().color = PDC;
        StartCoroutine(EnablePDM(0.5f));
    }
    
    IEnumerator EnablePDM(float delay)
    {
        yield return new WaitForSeconds(delay);
        //PDLS.SetActive(true);
    }
}
