using ObjectPool;
using UnityEngine;

namespace Gameplay
{
    public class Cross : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void DeSpawn()
        {
            PoolManager.Instance.Despawn(Pools.Types.Cross, gameObject);
        }
    }
}
