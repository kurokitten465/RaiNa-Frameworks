using System;

namespace RaiNa.Services.Dependency
{
    internal sealed class RegistrationEntry
    {
        public Guid OwnerContainerId { get; }
        public string OwnerContainerName { get; }
        public ServiceDescriptor Descriptor { get; }
        public DateTime CreationTime { get; }

        public RegistrationEntry(Guid ownerContainerId, string ownerContainerName, ServiceDescriptor descriptor)
        {
            OwnerContainerId = ownerContainerId;
            OwnerContainerName = ownerContainerName;
            Descriptor = descriptor;
            CreationTime = DateTime.UtcNow;
        }
    }
}
