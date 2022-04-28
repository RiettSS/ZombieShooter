using UnityEngine;
using Zenject;
using ZombieShooter.Service;
using ZombieShooter.Service.Impl;

namespace Infrastructure
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Camera _camera;
        [SerializeField] private InputService _inputService; 
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            Container.Bind<IInputService>().FromInstance(_inputService);
            
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            var player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _startPoint.position, Quaternion.identity, null);
            Container.Bind<Player>().FromInstance(player).AsSingle();
        }
    }
}
