namespace EllinghamScript.Variables
{
    public interface IVarTypeWrapper<out T>
    {
        T Unwrap();
    }
}