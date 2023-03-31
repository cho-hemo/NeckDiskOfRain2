using RiskOfRain2.Manager;
using UnityEngine;

namespace VagrantSkill
{
    public class Orb : MonoBehaviour
    {
        private GameObject _player;
        private Rigidbody _rigidbody;
        private float SPEED = 3200f;

        private void Start()
        {
            _player = GameManager.Instance.Player.gameObject;
            _rigidbody = GetComponent<Rigidbody>();
        }

		private void OnEnable()
		{
			if (_player != null)
			{
				Vector3 toPlayer = (_player.transform.position - transform.position).normalized;
				_rigidbody.AddForce(toPlayer * SPEED);
			}
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