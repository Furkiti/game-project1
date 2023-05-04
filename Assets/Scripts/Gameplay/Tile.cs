using DG.Tweening;
using Gameplay.Interfaces;
using Managers;
using ObjectPool;
using UnityEngine;

namespace Gameplay
{
    public class Tile : MonoBehaviour, IClickable
    {
        [SerializeField]private SpriteRenderer tileSpriteRenderer;
        public int GridX { get; set; }
        public int GridY { get; set; }
        public bool IsEmpty { get; set; }

        private Cross _crossGO;

        private void Awake()
        {
            IsEmpty = true;
        }

        
        public void OnClick()
        {
            if (!IsEmpty)
            {
                return;
            }
            AddCross(); 
            TileManager.Instance.OnTileSelected(this);
        }
        
        
        private void AddCross()
        {
            _crossGO = PoolManager.Instance.Spawn(Pools.Types.Cross, transform.position, Quaternion.identity, transform).GetComponent<Cross>();
            _crossGO.transform.localScale = Vector3.one;
            IsEmpty = false;
        }

        public void RemoveCross()
        {
            if (_crossGO != null)
            {
                _crossGO.transform.DOScale(_crossGO.transform.localScale * 1.2f, .2f).OnComplete((() =>
                {
                    _crossGO.transform.DOScale(_crossGO.transform.localScale / 1.2f, .2f).OnComplete((() =>
                    {
                        _crossGO.DeSpawn();
                        IsEmpty = true;
                    }));
                }));
            }
            
            
        }
        
        public void DeSpawn()
        {
            RemoveCross();
            transform.localScale = Vector3.one;
        }
        
        
    }
}
