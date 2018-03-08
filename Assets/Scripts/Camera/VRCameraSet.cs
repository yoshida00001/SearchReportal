using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VRCameraSet : MonoBehaviour{

    void Start()
    {
        StartCoroutine(LoadCardBoard());
    }

    IEnumerator LoadCardBoard()
    {
        VRSettings.LoadDeviceByName("cardboard");
        yield return null;
        VRSettings.enabled = true;
    }

    public void Tangan() {

    }
}
