using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Assets.Scripts.Data.StaticData;

namespace Assets.Scripts.GameEnvironment.Dice
{
    public class DiceLayout : MonoBehaviour
    {
        [SerializeField] private List<Image> _faceImages;
        [SerializeField] private AreaDice _playerDice;
        [SerializeField] private List<DiceFace> _faces;
        [SerializeField] private List<TMP_Text> _increaseTexts;
        [SerializeField] private List<TMP_Text> _decreaseTexts;        

        public void InitDice(AreaDice areaDice)
        {
            _playerDice = areaDice;
            _faces = _playerDice.Faces.ToList();

            foreach (var face in _faces)
            {
                face.InitDiceFace(face.FaceData);
            }
            SetFaces();
        }             

        public void UpdateFaces(AreaDice areaDice)
        {
            for (int i = 0; i < areaDice.Faces.Count; i++)
            {
                _faceImages[i].sprite = areaDice.Faces[i].FaceImage;

                if (areaDice.Faces[i].FaceType == AreaType.Attack || areaDice.Faces[i].FaceType == AreaType.Defence)
                {
                    _increaseTexts[i].gameObject.SetActive(true);
                    _increaseTexts[i].text = areaDice.Faces[i].FaceData.IncreaseValue.ToString();

                }
                else if (areaDice.Faces[i].FaceType == AreaType.StrongAttack || areaDice.Faces[i].FaceType == AreaType.StrongDefence)
                {
                    _increaseTexts[i].gameObject.SetActive(true);
                    _decreaseTexts[i].gameObject.SetActive(true);
                    _increaseTexts[i].text = areaDice.Faces[i].FaceData.IncreaseValue.ToString();
                    _decreaseTexts[i].text = areaDice.Faces[i].FaceData.DecreaseValue.ToString();
                }
            }
        }

        private void SetFaces()
        {
            
            for (int i = 0; i < _faces.Count; i++)
            {
                _faceImages[i].sprite = _faces[i].FaceImage;                

                if (_faces[i].FaceType == AreaType.Attack || _faces[i].FaceType == AreaType.Defence)
                {
                    _increaseTexts[i].gameObject.SetActive(true);
                    _increaseTexts[i].text = _faces[i].FaceData.IncreaseValue.ToString();
                    
                }
                else if (_faces[i].FaceType == AreaType.StrongAttack || _faces[i].FaceType == AreaType.StrongDefence)
                {
                    _increaseTexts[i].gameObject.SetActive(true);
                    _decreaseTexts[i].gameObject.SetActive(true);
                    _increaseTexts[i].text = _faces[i].FaceData.IncreaseValue.ToString();
                    _decreaseTexts[i].text = _faces[i].FaceData.DecreaseValue.ToString();
                }
            }
        }
    }
}
