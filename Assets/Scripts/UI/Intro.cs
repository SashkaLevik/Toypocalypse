using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.States;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Scripts.UI
{
    public class Intro : MonoBehaviour
    {
        private const string MenuScene = "Menu";

        [SerializeField] private VideoPlayer _cattPlayer;
        [SerializeField] private VideoPlayer _enIntroPlayer;
        [SerializeField] private VideoPlayer _ruIntroPlayer;
        [SerializeField] private RawImage _cattImage;
        [SerializeField] private RawImage _introImage;
        [SerializeField] private Button _skip;

        private IGameStateMachine _stateMachine;

        private void Awake()
            => _stateMachine = AllServices.Container.Single<IGameStateMachine>();        

        private void OnEnable()
        {
            _cattPlayer.loopPointReached += PlayIntro;
            _enIntroPlayer.loopPointReached += EndIntro;
            _ruIntroPlayer.loopPointReached += EndIntro;
            _skip.onClick.AddListener(LoadMenu);
        }        

        private void OnDestroy()
        {
            _cattPlayer.loopPointReached -= PlayIntro;
            _enIntroPlayer.loopPointReached -= EndIntro;
            _ruIntroPlayer.loopPointReached -= EndIntro;
            _skip.onClick.RemoveListener(LoadMenu);
        }

        private void PlayIntro(VideoPlayer player)
        {
            _cattPlayer.Stop();            
            _cattImage.gameObject.SetActive(false);
            _cattPlayer.gameObject.SetActive(false);
            _introImage.gameObject.SetActive(true);

            if (Application.systemLanguage == SystemLanguage.Russian)
                _ruIntroPlayer.gameObject.SetActive(true);
            else
                _enIntroPlayer.gameObject.SetActive(true);
        }

        private void EndIntro(VideoPlayer source)
        {
            _enIntroPlayer.Stop();
            _ruIntroPlayer.Stop();
            LoadMenu();
        }              

        private void LoadMenu()
        {
            _stateMachine.Enter<MenuState, string>(MenuScene);
        }
    }
}
