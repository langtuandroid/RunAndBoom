using System.Collections.Generic;

namespace CodeBase.Services.Input.Types
{
    public interface IInputTypesServices : IService
    {
        void AddInputService(IInputTypeService inputTypeService);
        List<IInputTypeService> GetInputServices();
    }
}