using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World
{
    /// <summary>
    /// This is a struct used to link a Decoration prefab to a Transform in the Biome
    /// </summary>
    [Serializable]
    public struct BiomeDecorationEntry
    {
        public Transform decorationTransform;
        public Decoration decorationPrefab;
    }
    
    public class Decoration : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private List<Sprite> spriteVariations;
        
        // Components
        private SpriteRenderer _spriteRenderer;

        
        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        

        public int SetSprite(int _spriteToRemoveFromList)
        {
            int decorationListPos = Random.Range(0, spriteVariations.Count);
            
            while(decorationListPos == _spriteToRemoveFromList)
                decorationListPos = Random.Range(0, spriteVariations.Count);
            
            _spriteRenderer.sprite = spriteVariations[decorationListPos];

            return decorationListPos;
        }


        public float DecorationWidth()
        {
            return _spriteRenderer.bounds.extents.x * 2;
        }
    }
}
