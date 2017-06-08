using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    [SerializeField]
    [Range(0.0f, 20.0f)]
    private float _interactionDistance = 4.0f;
    
    private Vector3 _camPos;
    private Vector3 _camForward;

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            _camPos = Camera.main.transform.position;
            _camForward = Camera.main.transform.forward;

            if (Physics.Raycast(_camPos, _camForward, out hit, _interactionDistance))
            {
                var hitComponents = hit.collider.gameObject.GetComponents(typeof(IInteractiveWithPlayer));

                if (hitComponents.Length >= 1)
                {
                    var interactive = hitComponents[0] as IInteractiveWithPlayer;
                    if (interactive != null) interactive.Interact();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(_camPos, _camForward * _interactionDistance);
    }
}
