using UnityEngine;

namespace AutoBindCode
{
    public class Bind : MonoBehaviour
    {
        public EnumBindType Enum_Type;
        public Component Com_Type;
    }

    public enum EnumBindType
    {
        Element,
        Class,
    }
}