using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using TMPro;
using DG.Tweening;
using static AnimatorManager;

public class PlayerController : Singleton<PlayerController>
{
    //Publics
    public float speed = 1f;

    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    [Header("Lerp")]
    public TextMeshPro uiTextPowerUp;

    [Header("Coins Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endScreen;

    public bool invencible = false;

    //Privates
    private bool _canRun;
    private Vector3 _pos;

    private float _currentSpeed;
    private Vector3 _startPosition;

    private float _baseSpeedToAnimation = 7f;

    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }

    // Update is called once per frame
    void Update()
    {

        if(!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(_currentSpeed * Time.deltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagToCheckEnemy)
        {
            if(!invencible)
            {
                MoveBack();
                EndGame(AnimatorManager.AnimationType.DEAD);
            }
        }
    }

    private void MoveBack()
    {
        transform.DOMoveZ(-1f, .3f).SetRelative();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCheckEndLine))
            if(!invencible) EndGame();
    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {

        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
    }

    #region Power Ups

    public void setPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        /*var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);

    }

    public void ResetHeight()
    {
        transform.DOMoveY(_startPosition.y, .1f);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
        Debug.Log("Entrrou");
    }

    #endregion
    


}
