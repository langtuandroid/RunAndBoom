using System.Collections.Generic;

namespace CodeBase.Services.Input.Types
{
    public interface IInputTypeServicesService : IService
    {
        void AddInputService(IInputTypeService inputTypeService);
        List<IInputTypeService> GetInputServices();
    }
}