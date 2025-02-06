using System.Linq;
using Oculus.Interaction;
using UnityEngine;

public class SocketBehaviour : MonoBehaviour
{
    [SerializeField] private Material _renderMeshMaterial;

    private SnapInteractable _snapInteractable;

    private bool _isHovering = false;
    private bool _isSelected = false;

    private MeshFilter _hoveredMeshFilter;
    private RenderParams _renderParams;

    private void Start()
    {
        _renderParams = new RenderParams(_renderMeshMaterial);
        _snapInteractable = GetComponent<SnapInteractable>();
    }

    public void OnHover()
    {
        _hoveredMeshFilter = _snapInteractable.Interactors.First().Rigidbody.GetComponentInParent<MeshFilter>();
        _isHovering = true;
    }

    public void OnUnhover()
    {
        _isHovering = false;
        _hoveredMeshFilter = null;
    }

    public void OnSelect()
    {
        _isSelected = true;
    }

    public void OnUnselect()
    {
        _isSelected = false;
    }

    private void FixedUpdate()
    {
        if(!_isSelected && _isHovering && _hoveredMeshFilter != null)
        {
            DrawMesh();
        }
    }

    private void DrawMesh()
    {
        Matrix4x4 matrix = Matrix4x4.TRS(transform.position, transform.rotation, _hoveredMeshFilter.transform.lossyScale);
        Graphics.RenderMesh(_renderParams, _hoveredMeshFilter.mesh, 0, matrix);
    }
}
