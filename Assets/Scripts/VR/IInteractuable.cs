public interface IInteractuable
{
    /// <summary>
    /// Se llama cuando el usuario ha mirado el objeto el tiempo suficiente.
    /// </summary>
    void Accion();
    void Mirando(float progreso);
}