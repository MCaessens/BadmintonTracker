using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imi.Project.Mobile.Core.Models.ErrorModels
{
    public class RacketErrorModel
    {
        public string BrandError { get; set; }  
        public string ModelError { get; set; }
    }
}
