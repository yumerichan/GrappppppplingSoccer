//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using HC.Debug;

//[DisallowMultipleComponent]
//public class CollisionViwer : MonoBehaviour
//{
//    // Start is called before the first frame update

//    #region フィールド / プロパティ

//    [SerializeField, Tooltip("可視コライダーの色")]
//    private ColliderVisualizer.VisualizerColorType _visualizerColor;

//    [SerializeField, Tooltip("メッセージ")]
//    private string _message;

//    [SerializeField, Tooltip("フォントサイズ")]
//    private int _fontSize;

//    [SerializeField, Tooltip("プレイヤー")]
//    private GameObject _player;

//    #endregion


//    private void Awake()
//    {
//        _player.AddComponent<ColliderVisualizer>().Initialize(_visualizerColor, _message, _fontSize);
//    }


//}
