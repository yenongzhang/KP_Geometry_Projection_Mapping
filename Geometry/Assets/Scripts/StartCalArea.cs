using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCalArea : MonoBehaviour
{
    protected GameObject[] Geometries;
    protected Material areaMaterial;
    // Start is called before the first frame update
    void Start()
    {
        if (Geometries == null)
        {
            Geometries = GameObject.FindGameObjectsWithTag("Geometry");
        }
        var AreaLabel = new GameObject("Label");
        TextMeshPro textRenderer = AreaLabel.AddComponent<TextMeshPro>();
        textRenderer.text = "Surface Area";
        textRenderer.color = Color.black;

        var buttonArea = GameObject.Find("ButtonSurfaceArea");
        areaMaterial = buttonArea.GetComponent<Renderer>().material;
        AreaLabel.transform.parent = buttonArea.transform;
        AreaLabel.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        AreaLabel.transform.localPosition = new Vector3(0.8f, -0.5f, 0.32f);
        AreaLabel.transform.localRotation = Quaternion.Euler(90, -90, 0);
    }

    private void OnMouseEnter()
    {
        for(int i = 0; i < Geometries.Length; i++)
        {
            Geometries[i].GetComponent<Renderer>().material = areaMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
