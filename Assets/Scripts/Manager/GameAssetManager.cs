using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class GameAssetManager : MonoBehaviour
    {

        public static GameAssetManager Instance;

        [Header("Effect Gameobject")]
        public GameObject slashFx;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(Instance);
            }
        }
    }
}