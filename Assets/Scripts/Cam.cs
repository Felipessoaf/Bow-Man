using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Material mat;

    private Vector3 _startPos;
    private Vector3 _endPos;

    private bool _draw;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DrawLine(Vector3 start, Vector3 end)
    {
        _startPos = start;
        _endPos = end;
        _draw = true;
    }

    public void StopDraw()
    {
        _draw = false;
    }
    
    void OnPostRender()
    {
        if (_draw)
        {
            Debug.Log(_startPos);
            Debug.Log(_endPos);
            GL.PushMatrix();
            mat.SetPass(0);
            GL.LoadOrtho();
            GL.Begin(GL.LINES);
            GL.Color(Color.black);
            GL.Vertex(_startPos);
            GL.Vertex(_endPos);
            GL.End();
            GL.PopMatrix();
        }
    }
}
