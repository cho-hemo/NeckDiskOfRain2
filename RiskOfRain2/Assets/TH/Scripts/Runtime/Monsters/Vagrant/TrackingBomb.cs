using UnityEngine;

namespace VagrantSkill
{
    public class TrackingBomb : MonoBehaviour
    {
        private GameObject _player;
        private Rigidbody _rigidbody;
        private float SPEED = 1000f;

        private void Awake()
        {
            //_player = FindRootObject("Player");
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            transform.LookAt(_player.transform);
        }

        private void Update()
        {
            transform.LookAt(_player.transform);
            _rigidbody.velocity = transform.forward * SPEED * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Ground" || other.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
    }
}