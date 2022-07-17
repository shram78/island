using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;



public class LevelComlete : MonoBehaviour
{
    [SerializeField] ParticleSystem _confettyParticle;
    [SerializeField] private Transform _arivvedPoint;
    [SerializeField] private GameObject _raftRoot;
    [SerializeField] private CinemachineVirtualCamera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _levelCompleteCamera;
    [SerializeField] private PlayerAnimator _animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            ArriveFromIsland(playrsBag);
        }
    }

    private void ArriveFromIsland(PlayrsBag playrsBag)
    {
        playrsBag.transform.SetParent(transform);
        _confettyParticle.Play();
        _raftRoot.transform.DOMove(_arivvedPoint.position, 30f);

        _mainCamera.Priority = 0;
        _levelCompleteCamera.Priority = 1;

        _animator.Dance();
    }
}
