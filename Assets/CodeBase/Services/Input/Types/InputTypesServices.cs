using System.Collections.Generic;

namespace CodeBase.Services.Input.Types
{
    public class InputTypesServices : IInputTypesServices
    {
        private List<IInputTypeService> _inputServices;

        public InputTypesServices()
        {
            _inputServices = new List<IInputTypeService>();
        }

        public void AddInputService(IInputTypeService inputTypeService) =>
            _inputServices.Add(inputTypeService);

        public List<IInputTypeService> GetInputServices() =>
            _inputServices;
    }
}