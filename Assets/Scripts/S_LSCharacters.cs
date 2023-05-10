using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;

public class S_LSCharacters : MonoBehaviour
{
    public Animator anim;
    public S_CanvasController controller;
    public GameObject TonyLS,BearLS,PDLS,WCLS,TurLS;
    public GameObject particles;
    public Image characterimage;
    public Color TonyC,BearC,PDC,WKC,TurC;
    public bool TonyE,BearE,PDE, WKE,TurE;
    public TextMeshProUGUI imageText;


    void Awake()
    {
        anim.SetBool("Entry", false);
        TonyLS.SetActive(false);
        BearLS.SetActive(false);
        PDLS.SetActive(false);
        WCLS.SetActive(false);
        TurLS.SetActive(false);
        //imageText.text = "s";

    }
    
    void Update()
    {
        
        if (TonyE)
        {
            BearLS.SetActive(false);
            WCLS.SetActive(false);
            PDLS.SetActive(false);
            TurLS.SetActive(false);
        }
        if (BearE)
        {
            TonyLS.SetActive(false);
            WCLS.SetActive(false);
            PDLS.SetActive(false);
            TurLS.SetActive(false);
        }
        if (PDE)
        {
            BearLS.SetActive(false);
            TonyLS.SetActive(false);
            WCLS.SetActive(false);
            TurLS.SetActive(false);
        }
        if (WKE)
        {
            BearLS.SetActive(false);
            TonyLS.SetActive(false);
            PDLS.SetActive(false);
            TurLS.SetActive(false);
        }
        if (TurE)
        {
            BearLS.SetActive(false);
            TonyLS.SetActive(false);
            WCLS.SetActive(false);
            PDLS.SetActive(false);
        }
        
    }


    public void EnableTony()
    {
        anim.SetBool("Entry", true);
        TonyE = true;
        BearE = false;
        PDE = false;
        WKE = false;
        TurE = false;
        BearLS.SetActive(false);
        PDLS.SetActive(false);
        WCLS.SetActive(false);
        TurLS.SetActive(false);
        if (!TonyLS.activeSelf)
        {
        Instantiate(particles, TonyLS.transform.position + new Vector3(0,2,0), particles.transform.rotation);
        StartCoroutine(EnableTonyM(0.5f));
        characterimage.GetComponent<Image>().color = TonyC;
        }

    }
    IEnumerator EnableTonyM(float delay)
    {
        yield return new WaitForSeconds(delay);
        TonyLS.SetActive(true);
    }
    public void EnableBear()
    {
        anim.SetBool("Entry", true);
        TonyE = false;
        BearE = true;
        PDE = false;
        WKE = false;
        TurE = false;
        TonyLS.SetActive(false);
        PDLS.SetActive(false);
        WCLS.SetActive(false);
        TurLS.SetActive(false);
        if (!BearLS.activeSelf){
        Instantiate(particles, BearLS.transform.position + new Vector3(0,2,0), particles.transform.rotation);        
        characterimage.GetComponent<Image>().color = BearC;

        StartCoroutine(EnableBearM(0.5f));

        }

    }
    
    IEnumerator EnableBearM(float delay)
    {
        yield return new WaitForSeconds(delay);
        BearLS.SetActive(true);
    }

    public void EnablePD()
    {
        anim.SetBool("Entry", true);
        TonyE = false;
        BearE = false;
        PDE = true;
        WKE = false;
        TurE = false;
        if (!PDLS.activeSelf)
        {
        Instantiate(particles, PDLS.transform.position + new Vector3(0,2,0), particles.transform.rotation); 
        }
        TonyLS.SetActive(false);
        BearLS.SetActive(false);
        WCLS.SetActive(false);
        TurLS.SetActive(false);
        characterimage.GetComponent<Image>().color = PDC;
        StartCoroutine(EnablePDM(0.5f));
    }
    
    IEnumerator EnablePDM(float delay)
    {
        yield return new WaitForSeconds(delay);
        PDLS.SetActive(true);
    }


    public void EnableWC()
    {
        anim.SetBool("Entry", true);
        TonyE = false;
        BearE = false;
        PDE = false;
        WKE = true;
        TurE = false;
        if (!WCLS.activeSelf)
        {
        Instantiate(particles, WCLS.transform.position + new Vector3(0,2,0), particles.transform.rotation); 
        }
        TonyLS.SetActive(false);
        BearLS.SetActive(false);
        PDLS.SetActive(false);
        TurLS.SetActive(false);
        characterimage.GetComponent<Image>().color = WKC;
        StartCoroutine(EnableWCM(0.5f));
    }
    
    IEnumerator EnableWCM(float delay)
    {
        yield return new WaitForSeconds(delay);
        WCLS.SetActive(true);
    }

    public void EnableTurtle()
    {
        anim.SetBool("Entry", true);
        TonyE = false;
        BearE = false;
        PDE = false;
        WKE = false;
        TurE = true;
        if (!TurLS.activeSelf)
        {
        Instantiate(particles, TurLS.transform.position + new Vector3(0,2,0), particles.transform.rotation); 
        }
        TonyLS.SetActive(false);
        BearLS.SetActive(false);
        PDLS.SetActive(false);
        WCLS.SetActive(false);
        characterimage.GetComponent<Image>().color = TurC;
        StartCoroutine(EnableTurM(0.5f));
    }
    
    IEnumerator EnableTurM(float delay)
    {
        yield return new WaitForSeconds(delay);
        TurLS.SetActive(true);
    }
}
