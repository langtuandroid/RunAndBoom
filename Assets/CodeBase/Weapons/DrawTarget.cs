using UnityEngine;

namespace CodeBase.Weapons
{
    public class DrawTarget : MonoBehaviour
    {
        [SerializeField] private GameObject _decal;
        [SerializeField] private GameObject _hero;

        public void Draw(Vector3 target)
        {
            _decal.transform.position = target;
            _decal.transform.forward = _hero.transform.forward;
        }
    }
}