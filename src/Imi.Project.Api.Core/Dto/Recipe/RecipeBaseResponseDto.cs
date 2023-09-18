using Imi.Project.Api.Core.Dto.User;
using Imi.Project.Api.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Dto.Recipe
{
    public class RecipeBaseResponseDto
    {        
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int Servings { get; set; }
        public string ImgURL { get; set; }
        public string Category { get; set; }
        public string Diet { get; set; }
        public UserResponseDto User { get; set; }
    }
}
