using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class S_Effect : MonoBehaviour
{
    public GameObject EdsLasereffect;
    public GameObject IcePatch;
    public Collider itemEffectCollider;
    // Start is called before the first frame update
    private void Update()
    {
        if(itemEffectCollider!=null)
        {
            gameObject.GetComponent<Collider>().transform.position = itemEffectCollider.transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="RedFlag")
        {
            other.gameObject.SetActive(false);
        }
    }
    public void activateEffect(GameObject incomingItem, GameObject itemUsed)
    {
        Debug.Log(itemUsed.name);
        if(itemUsed.name == "The EDS")
        {
            EdsLaser(incomingItem);
        }
    }
   public void EdsLaser(GameObject itemToDestroy)
    {

        GameObject activeEdsLaserEffect = Instantiate(EdsLasereffect, transform.position, transform.rotation) as GameObject;
        itemEffectCollider = activeEdsLaserEffect.GetComponent<Collider>();

    }

}
