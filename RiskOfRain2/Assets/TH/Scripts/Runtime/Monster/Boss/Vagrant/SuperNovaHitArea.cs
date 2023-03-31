using System.Collections;
using UnityEngine;

namespace VagrantSkill
{
    public class SuperNovaHitArea : MonoBehaviour
    {
        private const int TOTAL_FRAME = 300;
        private const float TOTAL_TIME = 3f;

        [SerializeField] private float _radius;
        private Vagrant _vagrant;


        private void Awake()
        {
            _vagrant = transform.root.GetComponent<Vagrant>();
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            StartCoroutine(Grow(TOTAL_TIME));
        }

        private IEnumerator Grow(float totalTime)
        {
            float deltaTime = totalTime / TOTAL_FRAME;
            float maxScale = 0.5f;
            float deltaScale = maxScale / TOTAL_FRAME;
            float timer = 0f;

            while (timer < totalTime)
            {
                transform.localScale += new Vector3(deltaScale, deltaScale, deltaScale);
                timer += deltaTime;
                Debug.Log(timer);
                yield return new WaitForSeconds(deltaTime);
            }

            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _radius = transform.localScale.x * transform.lossyScale.x / 2f;
            _vagrant.OnSuperNova(_radius);
        }
    }
}