//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using HC.Debug;

//[DisallowMultipleComponent]
//public class CollisionViwer : MonoBehaviour
//{
//    // Start is called before the first frame update

//    #region �t�B�[���h / �v���p�e�B

//    [SerializeField, Tooltip("���R���C�_�[�̐F")]
//    private ColliderVisualizer.VisualizerColorType _visualizerColor;

//    [SerializeField, Tooltip("���b�Z�[�W")]
//    private string _message;

//    [SerializeField, Tooltip("�t�H���g�T�C�Y")]
//    private int _fontSize;

//    [SerializeField, Tooltip("�v���C���[")]
//    private GameObject _player;

//    #endregion


//    private void Awake()
//    {
//        _player.AddComponent<ColliderVisualizer>().Initialize(_visualizerColor, _message, _fontSize);
//    }


//}
