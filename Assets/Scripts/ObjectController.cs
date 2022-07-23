using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [Header("Character Material")]
    [SerializeField] private Material characterMaterial;
    [Space]
    [SerializeField] private List<Material> cubeMaterials;
    
    private int _randomMaterial;
    private AudioSource enabledAudio;
    private AudioSource disabledAudio;
    
    void Start()
    {
        enabledAudio = gameObject.GetComponent<AudioSource>();
        
        _rigidbody = GetComponent<Rigidbody>();
        _randomMaterial = Random.Range(0, 8);
        gameObject.GetComponent<MeshRenderer>().material = cubeMaterials[_randomMaterial];
    }

    private void OnCollisionEnter(Collision other)
    {
        #region Object Materials and Rigidbody Settings

        if (other.gameObject.CompareTag("SmallCircle") ||
            other.gameObject.CompareTag("MidCircle") ||
            other.gameObject.CompareTag("BigCircle"))
        {
            gameObject.GetComponent<MeshRenderer>().material = characterMaterial;        
        }
        
        if (other.gameObject.CompareTag("Player"))
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        #endregion

        if (other.gameObject.CompareTag("SmallCircle"))
        {
            GameController.Instance.smallCircleCount.Add(1);
            PlayEnabledAudio();
        }

        if (other.gameObject.CompareTag("MidCircle"))
        {
            GameController.Instance.midCircleCount.Add(1);
            PlayEnabledAudio();
        }

        if (other.gameObject.CompareTag("BigCircle"))
        {
            GameController.Instance.bigCircleCount.Add(1);
            PlayEnabledAudio();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        #region Object Materials and Rigidbody Settings
        
        if (other.gameObject.CompareTag("SmallCircle") ||
            other.gameObject.CompareTag("MidCircle") ||
            other.gameObject.CompareTag("BigCircle"))
            gameObject.GetComponent<MeshRenderer>().material = cubeMaterials[_randomMaterial];
        
        if (other.gameObject.CompareTag("Player"))
            _rigidbody.constraints = RigidbodyConstraints.None;

        #endregion
        
        if (other.gameObject.CompareTag("SmallCircle"))
            GameController.Instance.smallCircleCount.Remove(1);
        
        if (other.gameObject.CompareTag("MidCircle"))
            GameController.Instance.midCircleCount.Remove(1);
        
        if (other.gameObject.CompareTag("BigCircle"))
            GameController.Instance.bigCircleCount.Remove(1);
    }

    private void PlayEnabledAudio()
    {
        enabledAudio.Play();
    }
}
