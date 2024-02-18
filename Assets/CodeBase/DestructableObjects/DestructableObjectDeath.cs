using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.Audio;
using UnityEngine;

namespace CodeBase.DestructableObjects
{
    [RequireComponent(typeof(AudioSource))]
    public class DestructableObjectDeath : MonoBehaviour, IDeath
    //, IProgressReader
    {
        [SerializeField] private GameObject _solid;
        [SerializeField] private GameObject _broken;
        [SerializeField] private DestructableTypeId _typeId;

        private AudioSource _audioSource;
        private float _deathDelay = 50f;
        private bool _isBroken = false;
        private List<Rigidbody> _parts;
        private IAudioService _audioService;
        private WaitForSeconds _waitForSeconds;

        public event Action Died;

        private void Awake()
        {
            _solid.SetActive(true);
            _broken.SetActive(false);
            _parts = new List<Rigidbody>(_broken.transform.childCount);
            _audioSource = GetComponent<AudioSource>();
            _audioService = AllServices.Container.Single<IAudioService>();

            for (int i = 0; i < _broken.transform.childCount; i++)
                _parts.Add(_broken.transform.GetChild(i).GetComponent<Rigidbody>());

            _waitForSeconds = new WaitForSeconds(_deathDelay);
        }

        public void Die()
        {
            _solid.SetActive(false);
            _broken.SetActive(true);
            Destroy(GetComponent<BoxCollider>());
            Destroy(_solid.GetComponentInChildren<BoxCollider>());

            if (_isBroken == false)
                foreach (Rigidbody part in _parts)
                    part.AddForce(part.gameObject.transform.forward * 5f, ForceMode.Impulse);

            PlaySound();
            _isBroken = true;

            StartCoroutine(DestroyTimer());
        }

        private void PlaySound()
        {
            switch (_typeId)
            {
                case DestructableTypeId.WoodenBox:
                    _audioService.LaunchDestructionSound(DestructionSoundId.DestructionWoodenBox, transform,
                        _audioSource);
                    break;
                case DestructableTypeId.Concrete:
                    _audioService.LaunchDestructionSound(DestructionSoundId.DestructionConcrete, transform,
                        _audioSource);
                    break;
            }
        }

        private IEnumerator DestroyTimer()
        {
            yield return _waitForSeconds;
            Destroy(gameObject);
        }
    }
}