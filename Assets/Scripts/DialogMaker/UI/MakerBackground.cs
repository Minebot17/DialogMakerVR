using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace DialogMaker.UI
{
    public class MakerBackground : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        [SerializeField] private float _maxSize;
        [SerializeField] private float _minSize;
        [SerializeField] private float _scrollSencitivity;
        
        private DefaultInputActions _inputActions;
        private Camera _camera;
        private CanvasScaler _canvasScaler;
        private RectTransform _canvasTransform;
        
        private bool _isMouseDown;

        [Inject]
        public void Inject(DefaultInputActions inputActions, Camera camera, CanvasScaler canvasScaler)
        {
            _inputActions = inputActions;
            _camera = camera;
            _canvasScaler = canvasScaler;
            _canvasTransform = canvasScaler.GetComponent<RectTransform>();
        }

        private void Update()
        {
            var scrollValue = _inputActions.UI.ScrollWheel.ReadValue<Vector2>().y;

            if (scrollValue == 0)
            {
                return;
            }

            if (Mouse.current.position.x.ReadValue() > Screen.width - 400 * (Screen.width / _canvasTransform.sizeDelta.x))
            {
                return;
            }

            _camera.orthographicSize = Mathf.Clamp(
                _camera.orthographicSize + _scrollSencitivity * (scrollValue > 1 ? -1 : 1), _minSize, _maxSize);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right)
            {
                return;
            }

            _isMouseDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right)
            {
                return;
            }
            
            _isMouseDown = false;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!_isMouseDown)
            {
                return;
            }
            
            var worldDelta = _camera.ScreenToWorldPoint(eventData.delta) - _camera.ScreenToWorldPoint(Vector3.zero);
            _camera.transform.position -= new Vector3(worldDelta.x, worldDelta.y, 0);
        }
    }
}