using UnityEngine;

namespace Kino
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Kino Image Effects/Fringe")]
    public class Fringe : MonoBehaviour
    {
        #region Public Properties

        // Shift amount for lateral CA
        [SerializeField, Range(0, 1)]
        float _lateralShift = 0.3f;

        public float lateralShift {
            get { return _lateralShift; }
            set { _lateralShift = value; }
        }

        // Axial CA strength
        [SerializeField, Range(0, 1)]
        float _axialStrength = 0.8f;

        public float axialStrength {
            get { return _axialStrength; }
            set { _axialStrength = value; }
        }

        // Shift amount for axial CA
        [SerializeField, Range(0, 1)]
        float _axialShift = 0.3f;

        public float axialShift {
            get { return _axialShift; }
            set { _axialShift = value; }
        }

        // Quality level for axial CA
        public enum QualityLevel { Low, High }

        [SerializeField]
        QualityLevel _axialQuality = QualityLevel.Low;

        public QualityLevel axialQuality {
            get { return _axialQuality; }
            set { _axialQuality = value; }
        }

        #endregion

        #region Private Properties

        [SerializeField] Shader _shader;

        Material _material;

        #endregion

        #region MonoBehaviour Functions

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (_material == null)
            {
                _material = new Material(_shader);
                _material.hideFlags = HideFlags.DontSave;
            }

            var cam = GetComponent<Camera>();
            var aspect = new Vector4(cam.aspect, 1.0f / cam.aspect, 1, 0);

            _material.SetVector("_CameraAspect", aspect);
            _material.SetFloat("_LateralShift", _lateralShift);
            _material.SetFloat("_AxialStrength", _axialStrength);
            _material.SetFloat("_AxialShift", _axialShift);

            if (_axialStrength == 0)
            {
                _material.DisableKeyword("AXIAL_SAMPLE_LOW");
                _material.DisableKeyword("AXIAL_SAMPLE_HIGH");
            }
            else if (_axialQuality == QualityLevel.Low)
            {
                _material.EnableKeyword("AXIAL_SAMPLE_LOW");
                _material.DisableKeyword("AXIAL_SAMPLE_HIGH");
            }
            else
            {
                _material.DisableKeyword("AXIAL_SAMPLE_LOW");
                _material.EnableKeyword("AXIAL_SAMPLE_HIGH");
            }

            Graphics.Blit(source, destination, _material, 0);
        }

        #endregion
    }
}
