using UnityEngine;

namespace Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _speed;

        private void LateUpdate()
        {
            var vector3 = transform.position;
            vector3.x = Mathf.Lerp(transform.position.x, _target.position.x, _speed * Time.deltaTime);
            transform.position = vector3;
        }
    }
}
