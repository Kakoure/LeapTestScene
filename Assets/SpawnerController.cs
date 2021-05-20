using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject objToSpawn;
    public Material[] matList;

    private MeshRenderer objMeshRend;
    private int matIndex;
    // Start is called before the first frame update
    void Start()
    {
        objMeshRend = objToSpawn.GetComponent<MeshRenderer>();
        matIndex = 0;
        objMeshRend.material = matList[matIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
        GameObject.Instantiate(objToSpawn, transform.position, Quaternion.identity);
    }

    public void NextMaterial()
    {
        matIndex += 1;
        matIndex %= matList.Length;
        objMeshRend.material = matList[matIndex];
    }

}
