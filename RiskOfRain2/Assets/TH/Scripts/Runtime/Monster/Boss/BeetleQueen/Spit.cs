using UnityEngine;
using RiskOfRain2.Manager;
using RiskOfRain2.Player;

namespace BeetleQueenSkills
{
    public class Spit : MonoBehaviour
    {
        private const float SPEED = 400f;

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
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(transform.forward * SPEED);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Player"))
            {
                other.GetComponent<PlayerBase>().TakeDamage(_damage);
                ObjectPoolManager.Instance.ObjectPoolPush(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100))
            {
                Gizmos.DrawRay(transform.position, Vector3.down * hit.distance);
            }
        }
    }
}