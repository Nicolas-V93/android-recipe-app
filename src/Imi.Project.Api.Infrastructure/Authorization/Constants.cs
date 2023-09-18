using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Infrastructure.Authorization
{
    public class Constants
    {
        public const string CreateOperationName = "Create";
        public const string CreateReviewOperationName = "CreateReview";
        public const string ReadOperationName = "Read";
        public const string UpdateOperationName = "Update";
        public const string DeleteOperationName = "Delete";

        public const string AdministratorRole = "Admin";
        public const string UserRole = "User";

        // Policies
        public const string OnlyAdminsPolicy = "AdminsOnly";
    }
}
