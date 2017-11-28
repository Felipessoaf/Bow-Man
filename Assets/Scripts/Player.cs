using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Match match;
    public GameObject ArrowPrefab;
    public Transform ShootPos;
    public bool right;
    public bool first;
    public float Strength;

    public Text Angle;
    public Text Force;

    public GameObject Arms;

    private bool _endTurn;
    private bool _canPlay;
    [SerializeField]
    private float _health;

    private Vector3 _startPos;
    private Vector3 _endPos;
    private Vector2 _diff;
    private float _dist;

    private GameObject myLine;

    // Use this for initialization
    void Start ()
    {
        _endTurn = false;
        _canPlay = first;
        _health = 100;
	}

    private void Update()
    {
        if(_canPlay)
        {
            if(Input.GetMouseButtonDown(0))
            {
                _startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _startPos = new Vector3(_startPos.x, _startPos.y);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                GameObject arrow = Instantiate(ArrowPrefab);
                arrow.transform.position = ShootPos.position;
                arrow.GetComponent<Arrow>().player = this;
                if (right)
                {
                    arrow.GetComponent<Arrow>().Force = new Vector2(_diff.normalized.x * _dist * Strength, _diff.normalized.y * _dist * Strength);
                    arrow.GetComponent<Arrow>().Right = true;
                }
                else
                {
                    arrow.GetComponent<Arrow>().Force = new Vector2(_diff.x, _diff.y);
                    arrow.GetComponent<Arrow>().Right = false;
                }
                arrow.GetComponent<Arrow>().Shoot();
                _canPlay = false;

                Angle.gameObject.SetActive(false);
                Force.gameObject.SetActive(false);

                if (myLine)
                {
                    Destroy(myLine);
                }
            }
            else if(Input.GetMouseButton(0))
            {
                _endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _endPos = new Vector3(_endPos.x, _endPos.y);

                if(myLine)
                {
                    Destroy(myLine);
                }

                myLine = new GameObject();
                myLine.transform.position = _startPos;
                myLine.AddComponent<LineRenderer>();
                LineRenderer lr = myLine.GetComponent<LineRenderer>();
                lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
                lr.startColor = Color.black;
                lr.endColor = Color.black;
                lr.startWidth = 0.1f;
                lr.endWidth = 0.1f;
                lr.SetPosition(0, _startPos);
                lr.SetPosition(1, _endPos);
                lr.sortingLayerName = "player";
                lr.sortingOrder = 2;
                
                _diff = _startPos - _endPos;
                _dist = Vector3.Distance(_startPos, _endPos);
                var angle = Mathf.Atan2(_diff.y, _diff.x) * Mathf.Rad2Deg;
                Arms.transform.rotation = Quaternion.Euler(0, 0, angle);

                Angle.text = angle.ToString() + " °";
                Angle.gameObject.transform.position = _startPos + Vector3.up;
                Force.text = _dist.ToString();
                Force.gameObject.transform.position = _endPos + Vector3.down;

                Angle.gameObject.SetActive(true);
                Force.gameObject.SetActive(true);
            }
        }
    }

    public IEnumerator SetTurn()
    {
        _canPlay = true;
        yield return new WaitUntil(() => _endTurn == true);
        match.EndTurn();
        _endTurn = false;
    }

    public void EndTurn()
    {
        _endTurn = true;
    }

    public void Damage(float dmg)
    {
        _health -= dmg;
        if(_health <= 0)
        {
            StartCoroutine(match.EndGame());
        }
    }
}
