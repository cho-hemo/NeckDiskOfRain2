using RiskOfRain2.Manager;
using RiskOfRain2.Player;
using UnityEngine;

namespace VagrantSkill
{
    public class Orb : MonoBehaviour
    {
        private const float SPEED = 3200f;

        private GameObject _player;
        private Rigidbody _rigidbody;
		private int _damage = 3;

		public void SetStats(int power)
		{
			_damage *= power;
		}

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
            if (other.CompareTag("Ground"))
            {
				ObjectPoolManager.Instance.ObjectPoolPush(gameObject);
			}
			else if (other.CompareTag("Player"))
			{
				other.GetComponent<PlayerBase>().TakeDamage(_damage);
				ObjectPoolManager.Instance.ObjectPoolPush(gameObject);
			}
        }
    }
}