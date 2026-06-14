using UnityEngine;

namespace UniKuroKit.Serialization
{
    public class SerializerFactory : MonoBehaviour
    {
        public static ISerializer Create(bool humanReadable)
            => humanReadable ? new JsonSerializer()
                             : new BinarySerializer();
    }
}
