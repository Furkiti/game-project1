using System;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay;
using ObjectPool;
using Singleton;
using UnityEngine;

namespace Managers
{
    public class TileManager : Singleton<TileManager>
    {
        [SerializeField]private int initialBoardSize;
        [SerializeField]private Vector3 originPosition;
        private float _initialTileSize = .32f;
        private int _boardSize;
        private List<Tile> _tiles;
        private List<Tile> _selectedTiles = new List<Tile>();
        
        private void OnEnable()
        {
            EventManager.onGameStarted += CreateTiles;
            EventManager.onRegenerateButtonClicked += RegenerateTiles;
        }
        
        private void OnDisable()
        {
            EventManager.onGameStarted -= CreateTiles;
            EventManager.onRegenerateButtonClicked -= RegenerateTiles;
        }
        
        private void CreateTiles()
        {
           CreateTiles(initialBoardSize);
        }
     
        private void CreateTiles(int newBoardSize)
        {
            float scaleFactor =  initialBoardSize / (float)newBoardSize;
            float scaledTileSize = _initialTileSize * scaleFactor;

            float totalWidth = newBoardSize * scaledTileSize;
            float totalHeight = newBoardSize * scaledTileSize;

            float startX = originPosition.x - totalWidth / 2f + scaledTileSize / 2f;
            float startY = originPosition.y - totalHeight / 2f + scaledTileSize / 2f;
            
            _tiles = new List<Tile>();

            for (int x = 0; x < newBoardSize; x++)
            {
                for (int y = 0; y < newBoardSize; y++)
                {
                    Vector3 tilePosition = new Vector3(startX + (x * scaledTileSize), startY + (y * scaledTileSize), originPosition.z);
                    GameObject tileGO = PoolManager.Instance.Spawn(Pools.Types.Tile, tilePosition, Quaternion.identity, transform);
                    Tile tile = tileGO.GetComponent<Tile>();
                    tile.GridX = x;
                    tile.GridY = y;

                    // Scale the tile if needed
                    if (Math.Abs(scaleFactor - 1f) > .1f)
                    {
                        tile.transform.localScale *= (scaleFactor);
                    }

                    _tiles.Add(tile);
                }
            }

            _boardSize = newBoardSize;
            
            EventManager.onBoardCreated?.Invoke(newBoardSize);
        }
        
        private void RegenerateTiles(int newBoardSize)
        {
            if (newBoardSize != _boardSize)
            {
                // Destroy the existing tiles
                foreach (var tile in _tiles)
                {
                    tile.DeSpawn();
                    PoolManager.Instance.Despawn(Pools.Types.Tile, tile.gameObject);
                }
            
                // Create the new tiles
                CreateTiles(newBoardSize);
            }
        }
        
        
        public void OnTileSelected(Tile tile)
        {
            if (!_selectedTiles.Contains(tile))
            {
                _selectedTiles.Add(tile);
            }
            else
            {
                _selectedTiles.Remove(tile);
            }

            // Check if any matches exist
            CheckMatches();
        }
        
        private void RemoveMatches(List<Tile> matches)
        {
            foreach (Tile tile in matches)
            {
                tile.RemoveCross();
                _selectedTiles.Remove(tile);
            }
        }
        
        private void CheckMatches()
        {
            List<Tile> matches = new List<Tile>();

            foreach (Tile tile in _selectedTiles)
            {
                if (!matches.Contains(tile))
                {
                    matches.Clear();
                    CheckMatchesRecursive(tile, ref matches);
            
                    if (matches.Count >= 3)
                    {
                        EventManager.onMatchFound?.Invoke();
                        RemoveMatches(matches);
                        return;
                    }
                }
            }
        }

        private void CheckMatchesRecursive(Tile tile, ref List<Tile> matches)
        {
            if (!_selectedTiles.Contains(tile) || matches.Contains(tile))
            {
                return;
            }
    
            matches.Add(tile);
    
            int x = tile.GridX;
            int y = tile.GridY;

            // Check adjacent tiles
            if (x > 0)
            {
                CheckMatchesRecursive(_tiles.Find(t => t.GridX == x - 1 && t.GridY == y), ref matches);
            }
            if (x < _boardSize - 1)
            {
                CheckMatchesRecursive(_tiles.Find(t => t.GridX == x + 1 && t.GridY == y), ref matches);
            }
            if (y > 0)
            {
                CheckMatchesRecursive(_tiles.Find(t => t.GridX == x && t.GridY == y - 1), ref matches);
            }
            if (y < _boardSize - 1)
            {
                CheckMatchesRecursive(_tiles.Find(t => t.GridX == x && t.GridY == y + 1), ref matches);
            }
        }


        
       

    }
    
    
}

   