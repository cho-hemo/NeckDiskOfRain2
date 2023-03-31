using RiskOfRain2.Manager;
using UnityEngine;

namespace BeetleQueenSkills
{
    public class BeetleWard : MonoBehaviour
    {
        private GameObject _player;
        private Rigidbody _rigidbody;
        private float SPEED = 1600f;

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

				//lookAtYZ
				Quaternion lookRot = Quaternion.LookRotation(toPlayer);
				transform.rotation = Quaternion.Euler(0f, lookRot.eulerAngles.y, lookRot.eulerAngles.z);
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