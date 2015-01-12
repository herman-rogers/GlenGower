using UnityEngine;
using System.Collections;

public class RotatingCube : MonoBehaviour
{
    GameObject _rotatingCube;

    // Use this for initialization
    void Start()
    {
        _rotatingCube = GameObject.Find("RotatingCube");
    }

    // Update is called once per frame
    void Update()
    {
        _rotatingCube.transform.Rotate(0.0f, 8.0f * Time.deltaTime, 0.0f);
    }
}
