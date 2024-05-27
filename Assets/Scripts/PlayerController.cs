using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;

    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    private bool _canRun;
    private Vector3 _pos;

    public GameObject endScreen;

    // Update is called once per frame
    void Update()
    {

        if(!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(speed * Time.deltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagToCheckEnemy)
        {
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCheckEndLine))
            EndGame();
    }

    public void StartToRun()
    {
        _canRun = true;
    }

    private void EndGame()
    {

        _canRun = false;
        endScreen.SetActive(true);
    }



}
