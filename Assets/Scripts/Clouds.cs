using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] bool isLeft;
    public List<Mesh> meshes;
    void Awake()
    {
        Invoke("Controlet", 1f);
        RandomStartup();
    }
    void RandomStartup()
    {
        int mx = Random.Range(1, meshes.Count + 1);
        GetComponent<MeshFilter>().mesh = meshes[mx-1];
        if (!isLeft)
        {
            int x = Random.Range(3, 9) * 10;
            int y = Random.Range(3, 9) * 10;
            int rotY = Random.Range(-18, 18) * 10;
            int scl = 4000 / (Random.Range(6, 15) * 5);
            this.transform.localPosition = new Vector3(x, y, this.transform.localPosition.z);
            this.transform.localEulerAngles = new Vector3(0, rotY, 0);
            this.transform.localScale = new Vector3(scl, scl, scl);
        }
        if (isLeft)
        {
            int x = Random.Range(-3, -30) * 10;
            int y = Random.Range(5, 30) * 10;
            int rotY = Random.Range(-18, 18) * 10;
            int scl = 6000 / (Random.Range(2, 5) * 5);
            this.transform.localPosition = new Vector3(x, y, this.transform.localPosition.z);
            this.transform.localEulerAngles = new Vector3(0, rotY, 0);
            this.transform.localScale = new Vector3(scl, scl, scl);
        }
    }
    public void Controlet()
    {
        if (this.gameObject.transform.localEulerAngles.y == 0) 
        {
            RandomStartup();
        }
    }
}
