using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCalVolume : MonoBehaviour
{
    protected GameObject[] Geometries;
    protected Material volumeMaterial;
    // Start is called before the first frame update
    void Start()
    {
        if (Geometries == null)
        {
            Geometries = GameObject.FindGameObjectsWithTag("Geometry");
        }
        var VolumeLabel = new GameObject("Label");
        TextMeshPro textRenderer = VolumeLabel.AddComponent<TextMeshPro>();
        textRenderer.text = "Volume";
        textRenderer.color = Color.black;

        var buttonVolume = GameObject.Find("ButtonVolume");
        volumeMaterial = buttonVolume.GetComponent<Renderer>().material;
        VolumeLabel.transform.parent = buttonVolume.transform;
        VolumeLabel.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        VolumeLabel.transform.localPosition = new Vector3(0.8f, -0.5f, 0.32f);
        VolumeLabel.transform.localRotation = Quaternion.Euler(90, -90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        for (int i = 0; i < Geometries.Length; i++)
        {
            Geometries[i].GetComponent<Renderer>().material = volumeMaterial;
        }
    }
}
