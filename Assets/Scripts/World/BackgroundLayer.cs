using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World
{
    public class BackgroundLayer : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private GameObject backgroundImagePrefab;
        [SerializeField] private List<Sprite> backgroundSprites;
        
        // Private Variables
        private Biome _biome;
        private float _currentBGImgPos;
        private float _biomeEndPos;

        private List<GameObject> _activeBgImages;


        private void Awake()
        {
            Init();
        }
        

        private void Init()
        {
            _currentBGImgPos = 0;
            _activeBgImages = new List<GameObject>();
            
            _biome = GetComponentInParent<Biome>();
        }


        public void UpdateBiomeEndPos(float _biomeNewEndPos)
        {
            _biomeEndPos = _biomeNewEndPos;

            SpawnBgImages();
        }


        private void SpawnBgImages()
        {
            Vector3 newBgImgPos = new Vector3(_currentBGImgPos, transform.position.y, 0);
            GameObject newBgImage = Instantiate(backgroundImagePrefab,
                newBgImgPos,
                quaternion.identity,
                transform);

            SpriteRenderer sr = newBgImage.GetComponent<SpriteRenderer>();
            sr.sprite = backgroundSprites[Random.Range(0, backgroundSprites.Count)];

            BoxCollider2D bC2D = newBgImage.GetComponent<BoxCollider2D>();
            bC2D.size = sr.bounds.size;

            _currentBGImgPos += bC2D.bounds.size.x;
            
            if (_currentBGImgPos < _biomeEndPos)
                SpawnBgImages();
        }
    } 
}
