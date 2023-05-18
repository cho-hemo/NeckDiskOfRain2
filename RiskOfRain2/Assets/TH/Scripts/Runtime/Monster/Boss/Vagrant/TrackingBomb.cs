using UnityEngine;
using RiskOfRain2.Manager;
using RiskOfRain2.Player;

namespace VagrantSkill
{
    public class TrackingBomb : MonoBehaviour
    {
        private const float SPEED = 1000f;

        private GameObject _player;
        private Rigidbody _rigidbody;

		private int _damage = 5;

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
		}

        private void Update()
        {
            transform.LookAt(_player.transform.GetChild(2).transform);
            _rigidbody.velocity = transform.forward * SPEED * Time.deltaTime;
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