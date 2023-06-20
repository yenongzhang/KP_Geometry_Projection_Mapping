using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpButton : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Material materialVolume;
    public Material materialSurface;

    // Start is called before the first frame update
    void Start()
    {
        string geometryName = this.gameObject.name;
        GameObject buttonVolume = Instantiate(buttonPrefab, transform.position + Vector3.forward * 2f, Quaternion.identity);
        Vector3 currentPositionVolume = buttonVolume.transform.position;
        buttonVolume.transform.position = new Vector3(currentPositionVolume.x, 0.2f, currentPositionVolume.z);
        buttonVolume.name = geometryName + " volume";
        buttonVolume.GetComponent<Renderer>().material = materialVolume;
        buttonVolume.AddComponent<Switch>();

        GameObject buttonSurface = Instantiate(buttonPrefab, transform.position + Vector3.forward * 3f, Quaternion.identity);
        Vector3 currentPositionSurface = buttonSurface.transform.position;
        buttonSurface.transform.position = new Vector3(currentPositionSurface.x, 0.2f, currentPositionSurface.z);
        buttonSurface.name = geometryName + " surface";
        buttonSurface.GetComponent<Renderer>().material = materialSurface;
        buttonSurface.AddComponent<Switch>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
