using UnityEngine;
using UnityEngine.VFX;

public class ThrusterVisuals : MonoBehaviour
{
    [SerializeField] private VisualEffect _rightThrusterVFX;
    [SerializeField] private VisualEffect _leftThrusterVFX;
    [SerializeField] private VisualEffect _mainThrusterVFX;
    private void Start()
    {
        StopAllThrusters();
        DroneController.onForceNone += StopAllThrusters;
        DroneController.onForceUp += LaunchAllThrusters;
        DroneController.onForceRight += LaunchLeftThruster;
        DroneController.onForceLeft += LaunchRightThruster;
    }
    public void LaunchAllThrusters()
    {
        _mainThrusterVFX.Play();
        _leftThrusterVFX.Play();
        _rightThrusterVFX.Play();
    }
    public void LaunchLeftThruster()
    {
        _leftThrusterVFX.Play();
    }
    public void LaunchRightThruster()
    {
        _rightThrusterVFX.Play();
    }
    public void StopAllThrusters()
    {
        _mainThrusterVFX.Stop();
        _leftThrusterVFX.Stop();
        _rightThrusterVFX.Stop();
    }
    private void OnDestroy()
    {
        DroneController.onForceNone -= StopAllThrusters;
        DroneController.onForceUp -= LaunchAllThrusters;
        DroneController.onForceRight -= LaunchLeftThruster;
        DroneController.onForceLeft -= LaunchRightThruster;
    }
}
