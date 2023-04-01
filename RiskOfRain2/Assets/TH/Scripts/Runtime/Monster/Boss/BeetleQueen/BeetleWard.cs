using RiskOfRain2.Manager;
using RiskOfRain2.Player;
using UnityEngine;

namespace BeetleQueenSkills
{
    public class BeetleWard : MonoBehaviour
    {
        private const float SPEED = 1600f;

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

            if (_player != null)
            {
                _rigidbody.velocity = Vector3.zero;
                Vector3 toPlayer = (_player.transform.position - transform.position).normalized;
                _rigidbody.AddForce(toPlayer * SPEED);

                //lookAtYZ
                Quaternion lookRot = Quaternion.LookRotation(toPlayer);
                transform.rotation = Quaternion.Euler(0f, lookRot.eulerAngles.y, lookRot.eulerAngles.z);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Player"))
            {
				other.GetComponent<PlayerBase>().TakeDamage(_damage);
				ObjectPoolManager.Instance.ObjectPoolPush(gameObject);
			}
        }
    }
}