using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace DialogMaker.UI
{
    public class MakerBackground : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        private Camera _camera;
        
        private bool _isMouseDown;

        [Inject]
        public void Inject(Camera camera)
        {
            _camera = camera;
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