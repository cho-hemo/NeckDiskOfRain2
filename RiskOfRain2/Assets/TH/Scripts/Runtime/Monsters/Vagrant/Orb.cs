using UnityEngine;

namespace VagrantSkill
{
    public class Orb : MonoBehaviour
    {
        private GameObject _player;
        private Rigidbody _rigidbody;
        private float SPEED = 3200f;

        private void Awake()
        {
            _player = GioleFunc.GetRootObj("Player");
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            Vector3 toPlayer = (_player.transform.position - transform.position).normalized;
            _rigidbody.AddForce(toPlayer * SPEED);
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