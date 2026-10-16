using Binus.WS.Pattern.Output;
using System.Collections.Generic;
using Git.Training.Model.Response;

namespace Git.Training.Output
{
    public class TemplateOutput : OutputBase
    {
        public List<TemplateResponseDTO> ListTemplate{ get; set; }


        public TemplateOutput()
        {
            this.ListTemplate = new List<TemplateResponseDTO>();
        }
    }
}
