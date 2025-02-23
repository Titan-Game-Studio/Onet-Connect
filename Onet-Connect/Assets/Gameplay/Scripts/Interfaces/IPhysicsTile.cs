namespace TGS.OnetConnect.Gameplay.Scripts.Interfaces
{
    public interface IPhysicsTile
    {
        void AddForce(UnityEngine.Vector3 force);
        void AddTorque(float value);
    }
}