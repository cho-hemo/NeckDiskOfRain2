using RiskOfRain2.Manager;
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
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            if (_player == null && GameManager.Instance.Player != null)
            {
                _player = GameManager.Instance.Player.gameObject;
            }

            if (_player != null)
            {
                _rigidbody.velocity = Vector3.zero;
                Vector3 toPlayer = (_player.transform.position - transform.position).normalized;
                _rigidbody.AddForce(toPlayer * SPEED);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}