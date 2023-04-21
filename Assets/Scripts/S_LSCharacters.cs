using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;

public class S_LSCharacters : MonoBehaviour
{
    public Animator anim;
    public S_CanvasController controller;
    public GameObject TonyLS,BearLS,PDLS;
    public GameObject particles;
    public Image characterimage;
    public Color TonyC,BearC,PDC;
    public bool TonyE,BearE,PDE;
    public TextMeshProUGUI imageText;


    void Awake()
    {
        anim.SetBool("Entry", false);
        TonyLS.SetActive(false);
        BearLS.SetActive(false);
       // PDLS.SetActive(false);
        //imageText.text = "s";

    }
    
    void Update()
    {
        if (TonyE)
        {
            BearLS.SetActive(false);
        }
        if (BearE)
        {
            TonyLS.SetActive(false);
        }
        if (PDE)
        {
            BearLS.SetActive(false);
            TonyLS.SetActive(false);
        }
    }


    public void EnableTony()
    {
        anim.SetBool("Entry", true);
        TonyE = true;
        BearE = false;
        PDE = false;
        BearLS.SetActive(false);

        if (!TonyLS.activeSelf)
        {
        Instantiate(particles, TonyLS.transform.position + new Vector3(0,2,0), particles.transform.rotation);
        StartCoroutine(EnableTonyM(0.5f));
        characterimage.GetComponent<Image>().color = TonyC;
        }

       // PDLS.SetActive(false);
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
        TonyLS.SetActive(false);

        if (!BearLS.activeSelf){
        Instantiate(particles, BearLS.transform.position + new Vector3(0,2,0), particles.transform.rotation);        
        characterimage.GetComponent<Image>().color = BearC;

        StartCoroutine(EnableBearM(0.5f));

        }

      //  PDLS.SetActive(false);
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
