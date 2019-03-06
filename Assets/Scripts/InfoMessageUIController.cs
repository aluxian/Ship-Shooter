using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMessageUIController : MonoBehaviour
{
    protected GlideController glideCtrl;
    protected Text textComp;

    // Start is called before the first frame update
    void Start()
    {
        glideCtrl = gameObject.GetComponentInChildren<GlideController>();
        textComp = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateShow(string text)
    {
        textComp.text = text;
        glideCtrl.SetDestination(glideCtrl.initialPosition);
    }

    public void Hide()
    {
        glideCtrl.SetPosition(glideCtrl.initialPosition + new Vector3(-350, 0, 0));
    }

    public void AnimateHide()
    {
        glideCtrl.SetDestination(glideCtrl.initialPosition + new Vector3(-350, 0, 0));
    }
}
