using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    public float 子彈速度 = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * 子彈速度 * Time.deltaTime);
    }
}
