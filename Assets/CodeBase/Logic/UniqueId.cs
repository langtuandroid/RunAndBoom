using System;
using System.Linq;
using UnityEngine;

namespace CodeBase.Logic
{
    public class UniqueId : MonoBehaviour
    {
        public string Id;

        private void Start()
        {
            GenerateId();
        }

        private void GenerateId()
        {
            UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();

            if (uniqueIds.Any(other => other != this && other.Id == Id))
                Id = $"{GetComponent<UniqueId>().gameObject.scene.name}_{Guid.NewGuid().ToString()}";
        }
    }
}